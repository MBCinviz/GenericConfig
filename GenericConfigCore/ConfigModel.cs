using System;
using System.Collections.Generic;
using System.Text;

namespace GenericConfigCore
{
    public class ConfigModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ConfigTypeEnum Type { get; set; }

        public string Value { get; set; }

        public Boolean IsActive { get; set; }

        public string ApplicationName { get; set; }
    }
}
