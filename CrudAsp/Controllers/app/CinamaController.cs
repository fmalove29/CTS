using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CrudAsp.Controllers.app;

[Authorize]
public class CinemaController : Controller
{
    public CinemaController()
    {

    }
}
