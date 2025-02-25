using CrudAsp.Repository;
using CrudAsp.Models.app;


namespace CrudAsp.Services.Genres;

public class GenreService
{
    private readonly IRepository<Genre> repository;
    private readonly IRepository<MovieGenre> _mgRepository;

    public GenreService(IRepository<Genre> repository, IRepository<MovieGenre> mgRepository)
    {
        this.repository = repository;
        _mgRepository = mgRepository;
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
    public async Task<IEnumerable<MovieGenre>> GetAllGenrePerMovies(Guid Id)
    {
        var genDb = await _mgRepository.GetDbSet();

        var movieGenres = genDb
        .Where(mg => mg.MoviesId == Id).ToList();  // Filter by movieId


        return movieGenres;
    }
}
