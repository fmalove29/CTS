
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
        return await this.repository.GetByIdAsync(Id);
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
                Location = cinema.Location
            };

            await this.repository.AddAsync(newCinema);

            return cinema;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}