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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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


            // modelBuilder.Entity<Genre>().HasData(
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Sci-Fi" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Thriller" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Action" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Romance" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Comedy" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Crime" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "War" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Biographical" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Drama" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Mystery" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Animation" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Western" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Horror" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Fantasy" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Documentary" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Superhero" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Family" },
            //     new Genre { Id = Guid.NewGuid(), GenreName = "Musical" }
            // );
            base.OnModelCreating(modelBuilder);
        }

    }
}
