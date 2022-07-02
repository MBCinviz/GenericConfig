using GenericConfigCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace GenericConfigMsSql
{

    public class MsSqlConfigProvider : IConfigProvider
    {
        private string _connetionString;

        public MsSqlConfigProvider(string connectionString)
        {
            this._connetionString = connectionString;
        }

        public List<ConfigModel> Provide(string applicationName)
        {
            var configList = new List<ConfigModel>();
            string queryString = string.Format("SELECT * FROM dbo.Config WHERE ApplicationName = '{0}' AND IsActive = 1",applicationName);

            using (SqlConnection connection = new SqlConnection(this._connetionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var config = new ConfigModel()
                            {
                                Id = reader.GetString(reader.GetOrdinal("Id")),
                                Type = (ConfigTypeEnum)Enum.Parse(typeof(ConfigTypeEnum), reader.GetString(reader.GetOrdinal("Type")), true),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Value = reader.GetString(reader.GetOrdinal("Value")),
                                IsActive = reader.GetInt32(reader.GetOrdinal("IsActive")) == 1,
                                ApplicationName = reader.GetString(reader.GetOrdinal("ApplicationName")),
                            };
                            configList.Add(config);
                        }
                    }
                }
            }

            return configList;
        }
    }
}