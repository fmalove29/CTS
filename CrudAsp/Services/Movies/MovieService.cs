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
using CrudAsp.resource.response;



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
        var employees = await this.repository.GetAllAsync();
        return employees;
    
    }

    public async Task<IEnumerable<Genre>> GetAllGenre()
    {
        return await this.genreRepo.GetAllAsync();
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        try
        {
            var newMovie = new Movie
            {
                Id          = Guid.NewGuid(), // Assign new Guid to avoid `Guid.Empty` issue
                Title       = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                EndDate     = movie.EndDate,
                Description = movie.Description,
                MovieGenres = new List<MovieGenre>(),
                MovieImages = new List<MovieImage>()
            };

            // Ensure movie.MovieGenres is not null before iterating
            if (movie.MovieGenres != null)
            {
                foreach (var movieGenre in movie.MovieGenres)
                {
                    var existingGenre = await this.genreRepo.GetByIdAsync(movieGenre.GenresId);
                    if (existingGenre != null)
                    {
                        newMovie.MovieGenres.Add(new MovieGenre
                        {
                            MoviesId = newMovie.Id, // Ensure it has the correct MovieId
                            GenresId = existingGenre.Id
                        });
                    }
                }
            }

            // Ensure movie.MovieImages is not null before iterating
            if (movie.MovieImages != null)
            {
                foreach (var mi in movie.MovieImages)
                {
                    newMovie.MovieImages.Add(new MovieImage
                    {
                        MovieId = newMovie.Id,
                        Name    = mi.Name,
                        Size    = mi.Size,
                        Type    = mi.Type,
                        Path    = mi.Path
                    });
                }
            }

            await this.repository.AddAsync(newMovie);
            return newMovie;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding movie: {ex.Message}", ex);
        }
    }


    public async Task<Movie> GetById(Guid Id)
    {
        var movieDb = await this.repository.GetDbSet();

        var movie = await movieDb
            .Include(e => e.MovieGenres) 
                .ThenInclude(mg => mg.Genres)  
            .Include(e => e.MovieImages)
            .FirstOrDefaultAsync(e => e.Id == Id);

        return movie;
    }


    public async Task Update(Movie movie)
    {
        await this.repository.UpdateAsync(movie);
    }


    public async Task<DbSet<Movie>> GetDbSet()
        => await this.repository.GetDbSet();




}