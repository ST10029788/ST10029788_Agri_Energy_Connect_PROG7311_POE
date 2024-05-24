using AgriEnergyConnectApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AgriEnergyConnectApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Constructor to initialize the controller with a logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Action method for the Index view
        public IActionResult Index()
        {
            return View();
        }
        // Action method for the New view
        public IActionResult New()
        {
            return View();
        }
        // Action method for the Emp view
        public IActionResult Emp()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // Action method for handling errors
        public IActionResult Error()
        {            // Handling errors and returning the Error view with the error details

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
