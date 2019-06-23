using CarDealer.Infrastructure.Bundles.Image.Model;
using CarDealer.Infrastructure.Bundles.Advert.Model;
using CarDealer.Infrastructure.Bundles.Car.Model;
using CarDealer.Infrastructure.Bundles.User.Model;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Infrastructure.Context
{
    public class CarDealerContext:DbContext
    {
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Image> Images { get; set; }
        
        public CarDealerContext(DbContextOptions<CarDealerContext> options):base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=dbo.CarDealerApi.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            modelBuilder.Entity<Advert>()
                .HasOne(x => x.Car)
                .WithOne(y => y.Advert)
                .HasForeignKey<Car>(x => x.Id);

            modelBuilder.Entity<Advert>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Advert)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}