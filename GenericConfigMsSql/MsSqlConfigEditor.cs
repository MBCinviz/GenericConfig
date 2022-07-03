using GenericConfigCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GenericConfigMsSql
{

    public class MsSqlConfigEditor : MsSqlConfigProvider, IConfigEditor
    {
        public MsSqlConfigEditor(string connectionString) : base(connectionString)
        {

        }
        public void AddNewConfig(ConfigModel configModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteConfig(string key, string applicationName)
        {
            var sql = "DELETE FROM dbo.config WHERE Name = '" + key + "'";
            throw new NotImplementedException();
        }

        public void UpdateConfig(ConfigModel configModel)
        {
            throw new NotImplementedException();
        }
    }
}