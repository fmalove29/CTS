using CrudAsp.Repository;
using CrudAsp.Models.app;

namespace CrudAsp.Services.Genres;

public class GenreService
{
    private readonly IRepository<Genre> repository;

    public GenreService(IRepository<Genre> repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await this.repository.GetAllAsync();
    }
    public async Task<Genre> GetByIdAsync(Guid Id)
    {
        return await this.repository.GetByIdAsync(Id);
    }
    public async Task<Genre> AddAsync(Genre genre)
    {
        try
        {
            var newGenre = new Genre
            {
                GenreName = genre.GenreName
            };

            return await this.repository.AddAsync(newGenre);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
