namespace NextTrain.Console
{
    using NextTrain.Client;

    public static class Program
    {
        public static async Task Main()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENRAILAPIKEY") ??
                         throw new ArgumentNullException();

            DeparturesClient client = new(apiKey);

            var journeys = await client.FindJourneys(Crs.Sheffield, Crs.Derby);

            foreach (var journey in journeys)
            {
                System.Console.WriteLine($@"Departure: {journey.Points[0].Departure:g} ({journey.Points[0].Platform})");
            }
        }
    }
}