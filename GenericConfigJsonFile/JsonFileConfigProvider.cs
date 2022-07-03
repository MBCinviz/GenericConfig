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

    public class JsonFileConfigProvider : IConfigProvider
    {
        protected string _filename;

        public JsonFileConfigProvider(string filename)
        {
            this._filename = filename;
        }

        public bool IsAccessible()
        {
            try
            {
                string jsonString = ReadJsonFile(_filename);
                return jsonString != null && jsonString.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ConfigModel> Provide(string applicationName)
        {
            string jsonString = ReadJsonFile(_filename);

            var configList = JsonConvert.DeserializeObject<List<ConfigModel>>(jsonString)
                .Where(c => c.ApplicationName.Equals(applicationName))
                .ToList();

            return configList;
        }

        protected string ReadJsonFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}