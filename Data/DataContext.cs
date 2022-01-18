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

        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skills> Skills { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skills>().HasData(
                new Skills { Id = 1, Name = "Fireball", Damage = 20},
                new Skills { Id = 2, Name = "BigBalls", Damage = 30 },
                new Skills { Id = 3, Name = "BiggerBalls", Damage = 50 },
                new Skills { Id = 4, Name = "BiggestBalls", Damage = 70 },
                new Skills { Id = 5, Name = "BlackHoleBalls", Damage = 100 }
                );
        }
    }
}