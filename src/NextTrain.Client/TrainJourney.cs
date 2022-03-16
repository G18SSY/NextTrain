namespace NextTrain.Client
{
    public record TrainJourney(IReadOnlyList<JourneyPoint> Points, TimeSpan OriginDelay);
}