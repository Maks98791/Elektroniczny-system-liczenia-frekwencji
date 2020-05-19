﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CounterWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using CounterWebApp.ViewModels;

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

        [HttpGet]
        public IActionResult Index()
        {
            using var db = new GuestCounterContext(_dbContextOptions);

            GuestsInfoViewModel model = new GuestsInfoViewModel
            {
                CurrentDate = DateTime.Now,
                GuestsInside = 0,
                GuestsIn = 0,
                GuestsOut = 0
            };

            foreach(var raport in db.Visitors)
            {
                if (raport.RaportDate.Date == model.CurrentDate.Date)
                {
                    model.GuestsInside += raport.GuestsIn - raport.GuestsOut;
                    model.GuestsIn += raport.GuestsIn;
                    model.GuestsOut += raport.GuestsOut;
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Statistics(StatParamsViewModel model)
        {
            using var db = new GuestCounterContext(_dbContextOptions);

            var statList = new List<StatisticsViewModel>();

            foreach (var raport in db.Visitors)
            {
                if (raport.RaportDate.Date >= model.BeginDate && raport.RaportDate.Date <= model.EndDate)
                {
                    StatisticsViewModel stat = new StatisticsViewModel
                    {
                        RaportDate = raport.RaportDate,
                        GuestsIn = raport.GuestsIn,
                        GuestsOut = raport.GuestsOut
                    };

                    statList.Add(stat);
                }
            }

            return RedirectToAction("DisplayStatistics", statList);
        }

        public IActionResult DisplayStatistics(List<StatisticsViewModel> statisticsViewModel)
        {
            return View(statisticsViewModel);
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
