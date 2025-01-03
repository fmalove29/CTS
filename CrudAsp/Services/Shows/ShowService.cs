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
}
