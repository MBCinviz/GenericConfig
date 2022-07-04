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
        private string _configType;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            setConfigManagerAsJsonFileReader();
        }

        private void setConfigManagerAsDB()
        {
            //// TODO buraya ayz
            string connectionString = "Server=DESKTOP-APHGH7T\\SQLEXPRESS;Database=dbGenericConfig;User ID=gConfig;Password=gConfig"; // todo read from app config
            int refreshTime = 1000 * 15; // todo read from app config
            //connectionString
            var provider = new MsSqlConfigProvider(connectionString);
            //var provider = new JsonFileConfigProvider(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            var configReader = new GenericConfigReader("SERVICE-A", provider, refreshTime);
            var editor = new MsSqlConfigEditor(connectionString);

            // var editor = new JsonFileConfigEditor(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            this.configManager = new GenericConfigManager("SERVICE-A", editor, refreshTime);
            this._configType = "DB";
        }

        private void setConfigManagerAsJsonFileReader()
        {
            int refreshTime = 1000 * 15; // todo read from app config
            var editor = new JsonFileConfigEditor(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Files/json.json");
            this.configManager = new GenericConfigManager("SERVICE-A", editor, refreshTime);
            this._configType = "JSON";
        }

        public IActionResult Index()
        {

            var model = new HomeViewModel()
            {
                ConfigList = configManager.GetConfigList(),
                IsProviderAccessible = configManager.ConfigProvider.IsAccessible(),
                ConfigType = this._configType
            };

            return View(model);

        }

        public IActionResult Form(string name)
        {
            return View(name == null || name.Length == 0 ? new ConfigModel() : configManager.getConfigModelByKey(name));
        }

        public void Change(string name)
        {
            switch (name)
            {
                case "JSON":
                    setConfigManagerAsJsonFileReader();
                    break;
                case "DB":
                    setConfigManagerAsDB();
                    break;
                default:
                    setConfigManagerAsJsonFileReader();
                    break;
            }
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
