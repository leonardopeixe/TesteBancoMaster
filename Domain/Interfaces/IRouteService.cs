using Domain.Models;

namespace Domain.Interfaces
{
    public interface IRouteService
    {
        Task AddRoute(RouteInput routeInput);
        Task<BestRouteResponse> GetBestRoute(string origin, string destination);
    }
}
