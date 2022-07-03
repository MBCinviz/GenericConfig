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
            //var provider = new JsonFileConfigProvider(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            //var configReader = new GenericConfigReader("SERVICE-A", provider, refreshTime);

            var editor = new JsonFileConfigEditor(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            var configManager = new GenericConfigManager("SERVICE-A", editor, refreshTime);


            var cfg = new ConfigModel()
            {
                Id = "13",
                Name = "TestKey4",
                Value = "TestValue4",
                IsActive = true,
                Type = ConfigTypeEnum.STRING
            };
            configManager.AddConfig(cfg);
            var configValue = configManager.GetValue<string>("TestKey4");
            var a = configManager.GetConfigDictionary();

            cfg.Value = "TestValue updated4";
            configManager.UpdateConfig(cfg);
            var b = configManager.GetConfigDictionary();

            configManager.DeleteConfig("TestKey4");
            var c = configManager.GetConfigDictionary();

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
