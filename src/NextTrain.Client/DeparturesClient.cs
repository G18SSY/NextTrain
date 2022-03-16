using System.Globalization;
using ServiceReference;

namespace NextTrain.Client
{
    public class DeparturesClient : IDeparturesClient
    {
        private readonly LDBServiceSoapClient client = new(LDBServiceSoapClient.EndpointConfiguration.LDBServiceSoap);
        private readonly AccessToken token;

        public DeparturesClient(string apiKey)
        {
            token = new AccessToken { TokenValue = apiKey };
        }

        public async Task<IEnumerable<TrainJourney>> FindJourneys(Crs from, Crs to)
        {
            var response = await client.GetDepBoardWithDetailsAsync(token,
                100,
                from,
                to,
                FilterType.to,
                0,
                120);

            return ExtractJourneys(response.GetStationBoardResult, to);
        }

        private static IEnumerable<TrainJourney> ExtractJourneys(StationBoardWithDetails1 board,
            Crs destinationLocation)
        {
            if (board.trainServices is not { } trainServices)
                yield break;


            var originLocation = new Crs(board.crs, board.locationName);
            foreach (var service in trainServices.Where(s => !s.isCancelled))
                yield return ConvertJourney(service, originLocation, destinationLocation);
        }

        private static TrainJourney ConvertJourney(ServiceItemWithCallingPoints1 service, Crs originLocation,
            Crs destinationLocation)
        {
            var origin = new JourneyPoint(originLocation,
                service.platform,
                ParseTime(service.sta),
                ParseTime(service.std));

            List<JourneyPoint> points = new()
            {
                origin
            };

            foreach (var callingPoint in service.subsequentCallingPoints.Select(p => p.callingPoint[0]))
            {
                Crs crs = new(callingPoint.crs, callingPoint.locationName);

                JourneyPoint point = new(crs,
                    null,
                    ParseTime(service.sta),
                    ParseTime(service.std));

                points.Add(point);
                
                if (crs == destinationLocation)
                    break;
            }

            return new TrainJourney(points, ParseDelay(service.etd));
        }

        private static TimeSpan ParseDelay(string serviceEtd)
            => TimeSpan.Zero; // TODO

        private static DateTimeOffset? ParseTime(string? raw)
        {
            if (raw is null)
                return null;

            return DateTimeOffset.Parse(raw, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
        }
    }
}