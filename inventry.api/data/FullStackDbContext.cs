using inventry.api.Models;
using Microsoft.EntityFrameworkCore;

namespace inventry.api.data
{
    public class FullStackDbContext : DbContext
    {
        public FullStackDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet <phone> phones { get; set; }
    }
}
