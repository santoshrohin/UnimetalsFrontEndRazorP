using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnimetalWeb.Models;
using UnimetalWeb.Helpers;
namespace UnimetalWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var loginDetails = HttpContext.Session.GetObjectFromJson<AuthResponse>("loginDetails");
            return View();
        }
    }
}
