using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Context : DbContext
    {
        public DbSet<Route> Routes { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>().HasData(
                new Route { Id = 1, Origin = "GRU", Destination = "BRC", Cost = 10 },
                new Route { Id = 2, Origin = "BRC", Destination = "SCL", Cost = 5 },
                new Route { Id = 3, Origin = "GRU", Destination = "CDG", Cost = 75 },
                new Route { Id = 4, Origin = "GRU", Destination = "SCL", Cost = 20 },
                new Route { Id = 5, Origin = "GRU", Destination = "ORL", Cost = 56 },
                new Route { Id = 6, Origin = "ORL", Destination = "CDG", Cost = 5 },
                new Route { Id = 7, Origin = "SCL", Destination = "ORL", Cost = 20 }
            );
        }
    }
}
