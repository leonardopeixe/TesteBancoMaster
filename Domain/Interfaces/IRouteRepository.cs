using Domain.Models;

namespace Domain.Interfaces
{
    public interface IRouteRepository
    {
        Task AddRoute(Route route);
        Task<IEnumerable<Route>> GetAllRoutes();
    }
}
