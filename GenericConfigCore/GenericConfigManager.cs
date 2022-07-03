using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericConfigCore
{
    public class GenericConfigManager : GenericConfigReader
    {
        protected IConfigEditor _configEditor;
        public GenericConfigManager(string applicationName, IConfigEditor configEditor, int refreshTimerIntervalInMs) : base(applicationName, configEditor, refreshTimerIntervalInMs)
        {
            this._configEditor = configEditor;
        }

        public void AddConfig(ConfigModel configModel)
        {
            if (IsConfigExist(configModel.Name))
            {
                return;
            }
            _configEditor.AddNewConfig(configModel);
            FetchConfig();
        }

        public void UpdateConfig(ConfigModel configModel)
        {
            if (!IsConfigExist(configModel.Name))
            {
                return;
            }
            _configEditor.UpdateConfig(configModel);
            FetchConfig();
        }

        public void DeleteConfig(string key, string applicationName)
        {
            if (!IsConfigExist(key))
            {
                return;
            }
            _configEditor.DeleteConfig(key, applicationName);
            FetchConfig();
        }


    }
}
