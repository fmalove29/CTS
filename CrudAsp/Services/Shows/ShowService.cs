using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Repository;
using CrudAsp.Models.app;

namespace CrudAsp.Services.Shows;

public class ShowService
{
    private readonly IRepository<Show> repository;
    public ShowService(IRepository<Show> repository)
    {
        this.repository = repository;
    }

    public async Task<Show> GetById(Guid Id)
    {
        var response = await this.repository.GetByIdAsync(Id);
        return response;
    }
    
    public async Task<DbSet<Show>> GetDbSet()
    => await this.repository.GetDbSet();
    public async Task<Show> AddAsync(Show show)
    => await this.repository.AddAsync(show);

    public async Task<Show> UpdateAsync(Show show)
    => await this.repository.UpdateAsync(show);

    public async Task<Show?> RemoveAsync(Show show)
    {
        try
        {
            return await this.repository.DeleteAsync(show);
        }
        catch (Exception ex)
        {
            return null; // Return null if the deletion fails
        }
    }

}
