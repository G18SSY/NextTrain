using NextTrain.Client;

namespace NextTrain.Router
{
    public class Change
    {
        public Change(JourneyPoint arrival, JourneyPoint departure)
        {
            if (departure.Arrival is null)
                throw new ArgumentException("Arrival time not set", nameof(arrival));

            if (departure.Departure is null)
                throw new ArgumentException("Departure time not set", nameof(departure));
            Arrival = arrival;
            Departure = departure;

            WaitTime = departure.Departure.Value - arrival.Arrival!.Value;
        }

        public JourneyPoint Arrival { get; }

        public JourneyPoint Departure { get; }

        public TimeSpan WaitTime { get; }
    }
}