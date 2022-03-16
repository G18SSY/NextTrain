using NextTrain.Client;

namespace NextTrain.Router
{
    /// <summary>
    ///     A possible route including changes that can be planned against.
    /// </summary>
    public class Route
    {
        public Route(Crs origin, Crs destination, IReadOnlyList<Crs> changes)
        {
            Origin = origin;
            Destination = destination;
            Changes = changes;
        }

        public Crs Origin { get; }

        public Crs Destination { get; }

        public IReadOnlyList<Crs> Changes { get; }

        public Route Reverse() => new(Destination, Origin, Changes.Reverse().ToList());
    }
}