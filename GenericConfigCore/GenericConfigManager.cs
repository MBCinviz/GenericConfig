using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericConfigCore
{
    public class GenericConfigManager : GenericConfigReader
    {

        //MODEL ADD,UPDATE AND DELETE METHODS
        protected IConfigEditor _configEditor;
        public GenericConfigManager(string applicationName, IConfigEditor configEditor, int refreshTimerIntervalInMs) : base(applicationName, configEditor, refreshTimerIntervalInMs)
        {
            this._configEditor = configEditor;
        }

        public ConfigModel getConfigModelByKey(string key)
        {
            return _configDictionary.GetValueOrDefault(key, null);
        }

        public void AddConfig(ConfigModel configModel)
        {
            if (IsConfigExist(configModel.Name) || !_configEditor.IsAccessible())
            {
                return;
            }

            _configEditor.AddNewConfig(configModel);
            FetchConfig();
        }

        public void UpdateConfig(ConfigModel configModel)
        {
            if (!IsConfigExist(configModel.Name) || !_configEditor.IsAccessible())
            {
                return;
            }

            _configEditor.UpdateConfig(configModel);
            FetchConfig();
        }

        public void DeleteConfig(string key, string applicationName)
        {
            if (!IsConfigExist(key) || !_configEditor.IsAccessible())
            {
                return;
            }

            _configEditor.DeleteConfig(key, applicationName);
            FetchConfig();
        }


    }
}
