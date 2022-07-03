using GenericConfigCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Linq;

namespace GenericConfigMsSql
{

    public class JsonFileConfigEditor : JsonFileConfigProvider, IConfigEditor
    {
        public JsonFileConfigEditor(string filename) : base(filename)
        {

        }

        public void AddNewConfig(ConfigModel configModel)
        {
            var configList = this.Provide(configModel.ApplicationName);
            configList.Add(configModel);
            WriteJsonFile(this._filename, configList);
        }

        public void DeleteConfig(string key, string applicationName)
        {
            var configList = this.Provide(applicationName);
            configList.RemoveAll(c => c.Name.Equals(key) && c.ApplicationName.Equals(applicationName));
            WriteJsonFile(this._filename, configList);
        }

        public void UpdateConfig(ConfigModel configModel)
        {
            var configList = this.Provide(configModel.ApplicationName);
            var index = configList.FindIndex(c => c.Name.Equals(configModel.Name) && c.ApplicationName.Equals(configModel.ApplicationName));
            if (index < 0)
            {
                return;
            }
            configList[index] = configModel;
            WriteJsonFile(this._filename, configList);
        }

        protected void WriteJsonFile(string path, List<ConfigModel> configList)
        {
            File.WriteAllText(path, Serialize(configList));
        }

        protected void WriteJsonFile(string path, string jsonString)
        {
            File.WriteAllText(path, jsonString);
        }

        protected string Serialize(List<ConfigModel> configModels)
        {
            return JsonConvert.SerializeObject(configModels, Formatting.Indented);
        }
    }
}