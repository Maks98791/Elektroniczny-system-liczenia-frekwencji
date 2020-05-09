using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CounterWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CounterWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GuestCounterContext _context;
        private readonly DbContextOptions<GuestCounterContext> _dbContextOptions;

        public HomeController(ILogger<HomeController> logger, GuestCounterContext context, DbContextOptions<GuestCounterContext> dbContextOptions)
        {
            _logger = logger;
            _context = context;
            _dbContextOptions = dbContextOptions;
        }

        public IActionResult Index()
        {
            using var db = new GuestCounterContext(_dbContextOptions);
            var raport = db.Visitors.FirstOrDefault(i => i.RaportId == 1);
            return View(raport);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
