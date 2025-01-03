
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Repository;
using CrudAsp.Models.app;

namespace CrudAsp.Services.Cinemas;

public class CinemaService
{
    private readonly IRepository<Cinema> repository;
    public CinemaService(IRepository<Cinema> repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Cinema>> GetAllAsync()
    {
        return await this.repository.GetAllAsync();
    }
    public async Task<Cinema> GetByIdAsync(Guid Id)
    {
        var existCinema = await this.repository.GetDbSet();

        var cinema = await existCinema
                    .Include(e => e.Halls)
                        .ThenInclude(h => h.Shows) 
                    .Where(e => e.Id == Id)
                    .FirstOrDefaultAsync();

        return cinema;
    }
    public async Task<DbSet<Cinema>> GetDbSet()
        => await this.repository.GetDbSet();

    public async Task<Cinema> AddAsync(Cinema cinema)
    {
        try
        {
            var newCinema = new Cinema
            {
                CinemaName = cinema.CinemaName,
                Location = cinema.Location,
                Halls = new List<Hall>()
            };
            foreach(var hall in cinema.Halls)
            {
                var h = new Hall
                {
                    CinemaId = cinema.Id,
                    HallName = hall.HallName,
                    SeatCapacity = hall.SeatCapacity
                };
                newCinema.Halls.Add(h);
            }


            await this.repository.AddAsync(newCinema);

            return cinema;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}