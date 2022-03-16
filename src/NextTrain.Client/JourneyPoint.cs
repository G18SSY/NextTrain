namespace NextTrain.Client
{
    public record JourneyPoint(Crs Station, string? Platform, DateTimeOffset? Arrival, DateTimeOffset? Departure);
}