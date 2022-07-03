using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace GenericConfigCore
{
    public class GenericConfigReader
    {
        public string ApplicationName
        {
            get => _applicationName;
            private set { }
        }

        public IConfigProvider ConfigProvider { get => _configProvider; private set { } }

        protected string _applicationName;
        protected int _refreshTimerIntervalInMs;
        private IConfigProvider _configProvider;
        protected Dictionary<string, ConfigModel> _configDictionary;

        public GenericConfigReader(string applicationName, IConfigProvider configProvider, int refreshTimerIntervalInMs)
        {
            this._applicationName = applicationName;
            this._configProvider = configProvider;
            this._refreshTimerIntervalInMs = refreshTimerIntervalInMs;

            //FetchConfig();
            ScheduleForFetch();
        }

        protected void FetchConfig()
        {
            try
            {
                _configDictionary = ConvertListToDictionary(this._configProvider.Provide(this._applicationName));
            }
            catch (Exception)
            {

            }
        }

        protected Dictionary<string, ConfigModel> ConvertListToDictionary(List<ConfigModel> configs)
        {
            return configs.ToDictionary(x => x.Name);
        }

        public T GetValue<T>(string key) where T : class
        {
            if (!IsConfigExist(key))
            {
                return null;
            }

            var model = this._configDictionary[key];

            if (model == null || !model.IsActive || !model.ApplicationName.Equals(this._applicationName))
            {
                return null;
            }

            return (T)Convert.ChangeType(model.Value, typeof(T));
        }

        public List<Object> GetAllValues()
        {
            var valueList = new List<Object>();
            foreach (var item in _configDictionary)
            {
                if (!item.Value.IsActive)
                {
                    continue;
                }

                valueList.Add(ParseValue(item.Value));
            }
            return valueList;
        }

        public bool IsConfigExist(string key)
        {
            return key != null && _configDictionary.ContainsKey(key);
        }

        public Dictionary<string, ConfigModel> GetConfigDictionary()
        {
            return this._configDictionary;
        }

        public List<ConfigModel> GetConfigList()
        {
            return this._configDictionary.Select(a => a.Value).ToList();
        }

        private object ParseValue(ConfigModel configModel)
        {
            return configModel.Type switch
            {
                ConfigTypeEnum.STRING => configModel.Value,
                ConfigTypeEnum.INTEGER => int.Parse(configModel.Value),
                ConfigTypeEnum.LONG => long.Parse(configModel.Value),
                ConfigTypeEnum.DOUBLE => double.Parse(configModel.Value),
                ConfigTypeEnum.BOOLEAN => bool.Parse(configModel.Value),
                ConfigTypeEnum.DATETIME => DateTime.Parse(configModel.Value),
                _ => null,
            };
        }

        protected async Task ScheduleForFetch()
        {
            while (true)
            {
                var delayTask = Task.Delay(this._refreshTimerIntervalInMs);
                FetchConfig();
                await delayTask;
            }
        }

    }
}
