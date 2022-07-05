using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore.Tests
{
    
    class MockConfigEditor : MockConfigProvider, IConfigEditor
    {
        public void AddNewConfig(ConfigModel configModel)
        {
            _configModels.Add(configModel);
        }

        public void DeleteConfig(string key, string applicationName)
        {
            _configModels.RemoveAll(c => c.Name.Equals(key) && c.ApplicationName.Equals(applicationName));
        }

        public void UpdateConfig(ConfigModel configModel)
        {
            var index = _configModels.FindIndex(c => c.Name.Equals(configModel.Name) && c.ApplicationName.Equals(configModel.ApplicationName));
            if (index < 0)
            {
                return;
            }
            _configModels[index] = configModel;
        }
    }
}
