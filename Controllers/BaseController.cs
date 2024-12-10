using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            base.OnActionExecuting(context);
        }
    }
}

