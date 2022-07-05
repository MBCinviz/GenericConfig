using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore
{
    public interface IConfigProvider
    {//MODEL PRODUCT
        List<ConfigModel> Provide(string applicationName);

        bool IsAccessible();
    }
}
