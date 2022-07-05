using GenericConfigCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GenericConfigMsSql
{
    //MSSQL ADD,UPDATE AND DELETE METHODS
    public class MsSqlConfigEditor : MsSqlConfigProvider, IConfigEditor
    {
        public MsSqlConfigEditor(string connectionString) : base(connectionString)
        {

        }
        public void AddNewConfig(ConfigModel configModel)
        {

            configModel.Id = Guid.NewGuid().ToString();
            var queryString = string.Format("INSERT INTO dbo.config (Id,Name,Type,Value,IsActive,ApplicationName) VALUES ('{0}','{1}','{2}','{3}',{4},'{5}')",
               configModel.Id, configModel.Name, configModel.Type, configModel.Value, configModel.IsActive == true ? 1 : 0, configModel.ApplicationName);
            //ID yi setle 
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
             
            }
        }

        public void DeleteConfig(string key, string applicationName)
        {
          
            var queryString = "DELETE FROM dbo.config WHERE Name = '" + key + "'";

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }

            }
        }
        public void UpdateConfig(ConfigModel configModel)
        {
        
            var queryString = string.Format("UPDATE dbo.config SET " +
                "Name = '{0}'," +
                "Type = '{1}'," +
                "Value = '{2}'," +
                "IsActive = '{3}'," +
                "ApplicationName = '{4}' WHERE Id = '{5}'",
              configModel.Name, configModel.Type, configModel.Value, configModel.IsActive == true ? 1 : 0, configModel.ApplicationName,configModel.Id);
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }

            }
            
        }
    }
}