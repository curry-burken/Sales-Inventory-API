using Microsoft.EntityFrameworkCore;
using SalesDetails.Models;

namespace SalesDetails.Data
{
    public class DbAttribute : DbContext
    {
        public DbAttribute(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Details> Item { get; set; }
    }
}
