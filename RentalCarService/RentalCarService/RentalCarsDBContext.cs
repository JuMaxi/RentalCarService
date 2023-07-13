using Microsoft.EntityFrameworkCore;
using RentalCarService.Models;

namespace RentalCarService
{
    public class RentalCarsDBContext : DbContext
    {
        public RentalCarsDBContext(DbContextOptions<RentalCarsDBContext> options) : base(options)
        {
        }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Brands> Brands { get; set; }

      
    }
}
