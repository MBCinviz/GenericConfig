using GenericConfigCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericConfig.Models
{
    public class HomeViewModel
    {
        public List<ConfigModel> ConfigList { get; set; }

        public bool IsProviderAccessible { get; set; }

        public string ConfigType { get; set; }
    }
}
