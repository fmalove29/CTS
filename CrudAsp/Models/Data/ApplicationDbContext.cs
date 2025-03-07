using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CrudAsp.Models.app;
using CrudAsp.Models;

namespace CrudAsp.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users> // Use your custom user class
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        // Remove the Users DbSet if you are using IdentityUser
        // If you want to keep custom DbSet properties for your custom entity classes, define them here.
        public DbSet<Movie> Movies {get; set;}

        public DbSet<Booking> Bookings {get; set;}

        public DbSet<Cinema> Cinemas {get; set;}

        public DbSet<Genre> Genres {get; set;}

        public DbSet<Hall> Halls {get; set;}

        public DbSet<Seat> Seat {get; set;}
        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<MovieImage> MovieImages {get; set;}
        public DbSet<CinemaFormat> CinemaFormats {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hall>()
            .HasOne(h => h.CinemaFormat)
            .WithMany(cf => cf.Halls)
            .HasForeignKey(h => h.CinemaFormatId)
            .OnDelete(DeleteBehavior.Cascade);
            // base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Movie>()
                    .HasMany(x => x.Genres)
                    .WithMany(y => y.Movies)
                    .UsingEntity(j => j.ToTable("MovieGenre"));

                modelBuilder.Entity<Show>()
                .Property(s => s.TicketPrice)
                .HasPrecision(18, 2);

                modelBuilder.Entity<Booking>()
                .Property(b => b.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.Property(c => c.CinemaName)
                    .IsRequired()   // Makes it NOT NULL
                    .HasMaxLength(255); // Optional: setting max length

                entity.Property(c => c.Location)
                    .IsRequired()   // Makes it NOT NULL
                    .HasMaxLength(255); // Optional: setting max length
            });



            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = Guid.NewGuid(), GenreName = "Action" },
                new Genre { Id = Guid.NewGuid(), GenreName = "Drama" },
                new Genre { Id = Guid.NewGuid(), GenreName = "Sci-Fi" },
                new Genre { Id = Guid.NewGuid(), GenreName = "Comedy" },
                new Genre { Id = Guid.NewGuid(), GenreName = "Horror" }
            );
        }

    }
}
