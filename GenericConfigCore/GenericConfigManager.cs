using System;
using System.Collections.Generic;
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
            if (IsExist(configModel.Name))
            {
                return;
            }
            configModel.ApplicationName = this._applicationName;
            _configEditor.AddNewConfig(configModel);
            FetchConfig();
        }

        public void UpdateConfig(ConfigModel configModel)
        {
            configModel.ApplicationName = this._applicationName;
            _configEditor.UpdateConfig(configModel);
            FetchConfig();
        }
        public void DeleteConfig(string key)
        {
            _configEditor.DeleteConfig(key, this._applicationName);
            FetchConfig();
        }

    }
}
