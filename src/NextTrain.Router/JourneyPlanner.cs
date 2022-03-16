using NextTrain.Client;

namespace NextTrain.Router
{
    public class JourneyPlanner
    {
        private static readonly TimeSpan defaultMinimumChange = TimeSpan.FromMinutes(5);
        
        private readonly IDeparturesClient departuresClient;
        private readonly IReadOnlyList<Route> routes;

        public JourneyPlanner(IDeparturesClient departuresClient, IEnumerable<Route> routes)
        {
            this.departuresClient = departuresClient;
            this.routes = routes.ToList();
        }

        public async IAsyncEnumerable<JourneyPlan> PlanJourneysAsync(Crs origin, Crs destination, TimeSpan? minimumChange = null)
        {
            foreach (var matchedRoute in FindRoutes(origin, destination))
            {
                IEnumerable<JourneyPlan> journeys = await PlanJourneysForRouteAsync(matchedRoute, minimumChange ?? defaultMinimumChange);

                foreach (var journey in journeys)
                {
                    yield return journey;
                }
            }
        }

        private IEnumerable<Route> FindRoutes(Crs origin, Crs destination)
            => routes.Where(r => r.Origin == origin && r.Destination == destination);

        private Task<IEnumerable<JourneyPlan>> PlanJourneysForRouteAsync(Route route, TimeSpan minimumChange)
        {
            throw new NotImplementedException();
        }
    }
}