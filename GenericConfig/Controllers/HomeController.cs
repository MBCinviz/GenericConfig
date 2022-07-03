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
        private GenericConfigManager configManager;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            //// TODO buraya ayz
            //string connectionString = "asdasd"; // todo read from app config
            int refreshTime = 1000 * 15; // todo read from app config

            //var provider = new MsSqlConfigProvider(connectionString);
            //var provider = new JsonFileConfigProvider(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            //var configReader = new GenericConfigReader("SERVICE-A", provider, refreshTime);

            var editor = new JsonFileConfigEditor(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            this.configManager = new GenericConfigManager("SERVICE-A", editor, refreshTime);
        }

        public IActionResult Index()
        {

            var model = new HomeViewModel()
            {
                ConfigList = configManager.GetConfigList(),
                IsProviderAccessible = configManager.ConfigProvider.IsAccessible()
            };

            return View(model);

        }

        public IActionResult Form(string name)
        {
            return View(name == null || name.Length == 0 ? new ConfigModel() : configManager.getConfigModelByKey(name));
        }

        public IActionResult Save([FromForm] ConfigModel configModel)
        {
            configModel.ApplicationName = configManager.ApplicationName;

            if (configManager.IsConfigExist(configModel.Name))
            {
                var currentConfig = configManager.getConfigModelByKey(configModel.Name);
                configModel.Id = currentConfig.Id;
                configManager.UpdateConfig(configModel);
            }
            else
            {
                configManager.AddConfig(configModel);
            }


            return RedirectToAction("index");
        }

        public IActionResult Remove(string name)
        {
            configManager.DeleteConfig(name, configManager.ApplicationName);
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
