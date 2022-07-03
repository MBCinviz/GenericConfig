using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore
{
    public interface IConfigEditor : IConfigProvider
    {
        void AddNewConfig(ConfigModel configModel);

        void UpdateConfig(ConfigModel configModel);

        void DeleteConfig(string key, string applicationName);

    }
}
