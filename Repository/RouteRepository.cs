using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly Context _context;

        public RouteRepository(Context context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task AddRoute(Route route)
        {
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Route>> GetAllRoutes()
        {
            return await _context.Routes.AsNoTracking().ToListAsync();
        }
    }
}
