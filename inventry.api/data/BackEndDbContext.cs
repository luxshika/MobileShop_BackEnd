using inventry.api.Models;
using Microsoft.EntityFrameworkCore;

namespace inventry.api.data
{
    public class BackEndDbContext : DbContext
    {
        public BackEndDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet <Phone> Phones { get; set; }
    }
}
