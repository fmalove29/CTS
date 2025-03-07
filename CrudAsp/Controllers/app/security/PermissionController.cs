using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using CrudAsp.Services.Movies;
using CrudAsp.Models.app;
using CrudAsp.Repository;

namespace CrudAsp.Controllers.app.security;

public class PermissionController : Controller
{
    public PermissionController()
    {

    }

}
