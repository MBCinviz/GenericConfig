using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore.Tests
{
    class MockConfigProvider : IConfigProvider
    {

        protected List<ConfigModel> _configModels = new List<ConfigModel>();

        public MockConfigProvider()
        {
            var conf1 = new ConfigModel();
            conf1.ApplicationName = "Test1";
            conf1.Id = "1";
            conf1.IsActive = true;
            conf1.Type = ConfigTypeEnum.INTEGER;
            conf1.Name = "Age";
            conf1.Value = "25";


            var conf2 = new ConfigModel();
            conf2.ApplicationName = "Test1";
            conf2.Id = "2";
            conf2.IsActive = true;
            conf2.Type = ConfigTypeEnum.STRING;
            conf2.Name = "Name";
            conf2.Value = "Bilal";


            var conf3 = new ConfigModel();
            conf3.ApplicationName = "Test1";
            conf3.Id = "3";
            conf3.IsActive = false;
            conf3.Type = ConfigTypeEnum.DOUBLE;
            conf3.Name = "Rate";
            conf3.Value = "5.4";


            var conf4 = new ConfigModel();
            conf4.ApplicationName = "Test2";
            conf4.Id = "4";
            conf4.IsActive = true;
            conf4.Type = ConfigTypeEnum.INTEGER;
            conf4.Name = "Surname";
            conf4.Value = "Cinviz";

            _configModels.Add(conf1);
            _configModels.Add(conf2);
            _configModels.Add(conf3);
            _configModels.Add(conf4);
        }

        public bool IsAccessible()
        {
            return true;
        }

        public List<ConfigModel> Provide(string applicationName)
        {
            return _configModels;
        }
    }
}
