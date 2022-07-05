using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore
{//MODEL CONFIG METHOD
    public interface IConfigEditor : IConfigProvider
    {
        void AddNewConfig(ConfigModel configModel);

        void UpdateConfig(ConfigModel configModel);

        void DeleteConfig(string key, string applicationName);

    }
}
