using Microsoft.EntityFrameworkCore;
using try5000rpg.Models;

namespace try5000rpg.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Characters> Characters {get;set;}
        public DbSet<User> Users { get; set; }
    }
}