using NextTrain.Client;

namespace NextTrain.Router
{
    public class JourneyPlan
    {
        public JourneyPlan(IReadOnlyList<JourneyPoint> points)
        {
            if (points.Count < 2)
                throw new ArgumentException("A journey requires two or more points");

            Points = points;
            Changes = CalculateChanges(points).ToList();
            TotalDuration = CalculateTotalDuration(points);
            MidJourneyWaitTime = Changes.Aggregate(TimeSpan.Zero, (t, change) => t + change.WaitTime);
        }

        public IReadOnlyList<JourneyPoint> Points { get; }
        
        public IReadOnlyList<Change> Changes { get; }

        public TimeSpan MidJourneyWaitTime { get; }

        public TimeSpan TotalDuration { get; }

        private static TimeSpan CalculateTotalDuration(IReadOnlyList<JourneyPoint> points)
        {
            var departure = points[0].Departure ??
                            throw new ArgumentException("The first point must have a departure time");
            var arrival = points[^1].Arrival ?? throw new ArgumentException("The final point must have a arrival time");

            return arrival - departure;
        }

        private static IEnumerable<Change> CalculateChanges(IReadOnlyList<JourneyPoint> points)
        {
            using IEnumerator<JourneyPoint> enumerator = points.GetEnumerator();

            if (!enumerator.MoveNext())
                yield break;

            var previous = enumerator.Current;

            while (enumerator.MoveNext())
            {
                if (previous.Station == enumerator.Current.Station)
                {
                    Change change = new(previous, enumerator.Current);

                    yield return change;
                }
            }
        }
    }
}