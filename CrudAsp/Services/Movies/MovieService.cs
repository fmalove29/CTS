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
                Title       = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                EndDate     = movie.EndDate,
                Description = movie.Description,
                Genres      = new List<Genre>(),
                MovieImages = new List<MovieImage>()
            };

            foreach (var genre in movie.Genres)
            {
                var existingGenre = await this.genreRepo.GetByIdAsync(genre.Id); 

                if (existingGenre != null)
                {
                    newMovie.Genres.Add(existingGenre);
                }
            }

            foreach(var mi in movie.MovieImages)
            {
                newMovie.MovieImages.Add(mi);
            }

            await this.repository.AddAsync(newMovie);

            return movie;
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }
    }

    public async Task<Movie> GetById(Guid Id)
    {

        var movieDb = await this.repository.GetDbSet();

        var movie = await movieDb
            .Include(e => e.Genres)   
            .Include(e => e.MovieImages)
            .Where(e => e.Id == Id)
            .FirstOrDefaultAsync();


        return movie;
    }

    public async Task Update(Movie movie)
    {
        await this.repository.UpdateAsync(movie);
    }


    public async Task<DbSet<Movie>> GetDbSet()
        => await this.repository.GetDbSet();




}