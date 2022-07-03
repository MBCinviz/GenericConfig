using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GenericConfigCore
{
    public class ConfigModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ConfigTypeEnum Type { get; set; }

        public string Value { get; set; }

        public Boolean IsActive { get; set; }

        public string ApplicationName { get; set; }
    }
}
