using CrudAsp.Models.app;
using CrudAsp.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrudAsp.resource.request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Services.Genres;
using CrudAsp.resource.response;
using CrudAsp.Models.Data;
namespace CrudAsp.Services.Menu;

public class AccessMenuService
{
    private readonly ApplicationDbContext _context;
    public AccessMenuService(ApplicationDbContext context)
    {   
        _context = context;
    }

}
