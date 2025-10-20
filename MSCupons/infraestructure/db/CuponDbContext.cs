using Microsoft.EntityFrameworkCore;
using MSCupons.domain.Entity;

namespace MSCupons.infrastructure.db
{
    public class CuponDbContext : DbContext
    {
        public CuponDbContext(DbContextOptions<CuponDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cupon> Cupones { get; set; }
    }
}