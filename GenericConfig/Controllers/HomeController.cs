using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GenericConfig.Models;
using GenericConfigCore;
using GenericConfigMsSql;
using System.IO;

namespace GenericConfig.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //// TODO buraya ayz
            //string connectionString = "asdasd"; // todo read from app config
            int refreshTime = 1000 * 15; // todo read from app config

            //var provider = new MsSqlConfigProvider(connectionString);
            var provider = new JsonFileConfigProvider(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            var configReader = new GenericConfigReader("SERVICE-A", provider, refreshTime);

            var configValue = configReader.GetValue<string>("SiteName");
       

            return View();

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
