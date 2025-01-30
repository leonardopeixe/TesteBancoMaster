using Domain.Interfaces;
using Domain.Models;

namespace App.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task AddRoute(RouteInput routeInput)
        {
            var route = new Route
            {
                Origin = routeInput.Origin.ToUpper(),
                Destination = routeInput.Destination.ToUpper(),
                Cost = routeInput.Cost
            };
            await _routeRepository.AddRoute(route);
        }

        public async Task<BestRouteResponse> GetBestRoute(string origin, string destination)
        {
            var routes = await _routeRepository.GetAllRoutes();
            var graph = BuildGraph(routes);
            return FindCheapestPath(origin.ToUpper(), destination.ToUpper(), graph);
        }

        private static Dictionary<string, Dictionary<string, decimal>> BuildGraph(IEnumerable<Route> routes)
        {
            var graph = new Dictionary<string, Dictionary<string, decimal>>();
            foreach (var route in routes)
            {
                if (!graph.ContainsKey(route.Origin))
                    graph[route.Origin] = new Dictionary<string, decimal>();

                graph[route.Origin][route.Destination] = route.Cost;
            }
            return graph;
        }

        private static BestRouteResponse? FindCheapestPath(string origin, string destination,
            Dictionary<string, Dictionary<string, decimal>> graph)
        {
            var visited = new Dictionary<string, (decimal Cost, string Path)>();
            var priorityQueue = new PriorityQueue<string, decimal>();

            priorityQueue.Enqueue(origin, 0);
            visited[origin] = (0, origin);

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();
                if (current == destination) break;

                if (!graph.ContainsKey(current)) continue;

                foreach (var neighbor in graph[current])
                {
                    var newCost = visited[current].Cost + neighbor.Value;
                    var newPath = $"{visited[current].Path} - {neighbor.Key}";

                    if (!visited.ContainsKey(neighbor.Key) || newCost < visited[neighbor.Key].Cost)
                    {
                        visited[neighbor.Key] = (newCost, newPath);
                        priorityQueue.Enqueue(neighbor.Key, newCost);
                    }
                }
            }
            var message = visited[destination].Path + " ao custo de $" + visited[destination].Cost;
            return visited.ContainsKey(destination)
                ? new BestRouteResponse(
                    visited[destination].Path, 
                    visited[destination].Cost, 
                    message
                )
                : null;
        }
    }
}
