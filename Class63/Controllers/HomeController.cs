using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Class63.Models;
using LakewoodScoopScraping;

namespace Class63.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Story> stories = Api.ScrapeTLS();
            return View(stories);
        }
    }
}
