using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore
{
    public interface IConfigProvider
    {
        List<ConfigModel> Provide(string applicationName);
    }
}
