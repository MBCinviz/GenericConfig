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
        private string _filename;

        public JsonFileConfigProvider(string filename)
        {
            this._filename = filename;
        }

        public List<ConfigModel> Provide(string applicationName)
        {
            string jsonString = ReadJsonFile(_filename);

            var configList = JsonConvert.DeserializeObject<List<ConfigModel>>(jsonString);

            return configList.Where(c => c.IsActive).ToList();
        }

        private string ReadJsonFile(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            {
                return reader.ReadToEnd();

            }
        }
    }
}