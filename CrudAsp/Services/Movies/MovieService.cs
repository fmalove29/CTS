using CrudAsp.Models.app;
using CrudAsp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrudAsp.resource.request;
using CrudAsp.Models.app;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Services.Genres;



namespace CrudAsp.Services.Movies;

public class MovieService 
{
    private readonly IRepository<Movie> repository;
    private readonly IRepository<Genre> genreRepo;

    public MovieService(IRepository<Movie> repository, IRepository<Genre> genreRepo)
    {
        this.repository = repository;
        this.genreRepo = genreRepo;
    }

    public async Task<IEnumerable<Movie>> GetResource()
            => await this.repository.GetAllAsync();

    public async Task<IEnumerable<Movie>> GetAll()
    {
        var employeeDb = await this.repository.GetDbSet(); // No Include statements
        return await employeeDb.ToListAsync(); // Lazy loading enabled at navigation property access
    }

    public async Task<IEnumerable<Genre>> GetAllGenre()
    {
        return await this.genreRepo.GetAllAsync();
    }
    // public async Task<Movie> GetByIdAsync(Guid Id)
    // {
    //     var employeeDb = await repository.GetDbSet();
    //     var employee = await employeeDb.Include(e => e.MovieGenres)
    //         .Where(e => e.Id == Id).FirstOrDefaultAsync();

    //         return employee;
    // }

    public async Task<Movie> AddAsync(Movie movie)
    {
        try
        {
            // Create a new movie entity and map the properties from the incoming movie object
            var newMovie = new Movie
            {
                Title       = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                EndDate     = movie.EndDate,
                Description = movie.Description,
                Genres      = new List<Genre>(),
                MovieImages = new List<MovieImage>()  // Initialize the Genres list
            };

            // Associate genres with the movie (movie.Genres should contain the genre entities)
            foreach (var genre in movie.Genres)
            {
                // Assuming the genre is already an entity, we can directly associate it
                var existingGenre = await this.genreRepo.GetByIdAsync(genre.Id); // Fetch genre from database

                if (existingGenre != null)
                {
                    newMovie.Genres.Add(existingGenre); // Add existing genre to the newMovie.Genres collection
                }
            }

            foreach(var mi in movie.MovieImages)
            {
                newMovie.MovieImages.Add(mi);
            }

            // Add the new movie to the repository, which will also handle inserting into the join table
            await this.repository.AddAsync(newMovie);

            return movie;  // Return the newly created movie object
        }
        catch (Exception ex)
        {
            // Return or throw the exception with a meaningful message
            throw new Exception(ex.Message);
        }
    }

    public async Task<Movie> GetById(Guid Id)
    {
        // Get the DbSet directly, no need to await
        var movieDb = await this.repository.GetDbSet(); // This should return DbSet<Movie>

        // Perform the async query directly on the DbSet
        var movie = await movieDb
            .Include(e => e.Genres)         // Include related genres
            .Include(e => e.MovieImages)
            .Where(e => e.Id == Id)    // Include related movie images
            .FirstOrDefaultAsync();


        return movie; // Return the found movie or null if not found
    }

    public async Task Update(Movie movie)
    {
        await this.repository.UpdateAsync(movie);
    }


    public async Task<DbSet<Movie>> GetDbSet()
        => await this.repository.GetDbSet();




}