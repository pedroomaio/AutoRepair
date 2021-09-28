using AutoRepair.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AutoRepair.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<SpecialistType> SpecialistTypes { get; set; }
        public DbSet<Repair> Repairs{ get; set; }
        public DbSet<Invoicing> Invoicings{ get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<AutoPiece> AutoPieces { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
