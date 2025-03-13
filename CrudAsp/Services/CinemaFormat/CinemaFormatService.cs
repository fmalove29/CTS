using Microsoft.EntityFrameworkCore;
using CrudAsp.Models.app;
using CrudAsp.Repository;
namespace CrudAsp.Services.CinemaFormat;

public class CinemaFormatService
{
    private readonly IRepository<CrudAsp.Models.app.CinemaFormat> _repository;
    public CinemaFormatService(IRepository<CrudAsp.Models.app.CinemaFormat> repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<CrudAsp.Models.app.CinemaFormat>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<CrudAsp.Models.app.CinemaFormat> AddAsync(CrudAsp.Models.app.CinemaFormat cinemaFormat)
    => await _repository.AddAsync(cinemaFormat);

    public async Task<CrudAsp.Models.app.CinemaFormat> GetByIdAsync(Guid Id)
    => await _repository.GetByIdAsync(Id);

    public async Task<DbSet<CrudAsp.Models.app.CinemaFormat>> GetDbSet()
            => await _repository.GetDbSet();

    public async Task<CrudAsp.Models.app.CinemaFormat> UpdateAsync(CrudAsp.Models.app.CinemaFormat cinemaFormat)
    => await _repository.UpdateAsync(cinemaFormat);

}
