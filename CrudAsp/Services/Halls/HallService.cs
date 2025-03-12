using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Repository;
using CrudAsp.Models.app;

namespace CrudAsp.Services.Halls;

public class HallService
{
    private readonly IRepository<Hall> repository;

    public HallService(IRepository<Hall> repository)
    {
        this.repository = repository;
    }

    public async Task<DbSet<Hall>> GetDbSet()
        => await this.repository.GetDbSet();

    public async Task<Hall> GetHallByCinemaId(Guid Id)
    {
        return await this.repository.GetByIdAsync(Id);
    }
    public async Task<Hall> AddAsync(Hall hall)
    => await this.repository.AddAsync(hall);
    public async Task<Hall> UpdateAsync(Hall hall)
    => await this.repository.UpdateAsync(hall);
}
