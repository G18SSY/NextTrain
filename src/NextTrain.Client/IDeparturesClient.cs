namespace NextTrain.Client
{
    public interface IDeparturesClient
    {
        Task<IEnumerable<TrainJourney>> FindJourneys(Crs from, Crs to);
    }
}