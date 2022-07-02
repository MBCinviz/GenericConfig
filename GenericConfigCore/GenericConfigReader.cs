﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace GenericConfigCore
{
    public class GenericConfigReader
    {
        protected string _applicationName;
        protected int _refreshTimerIntervalInMs;
        protected IConfigProvider _configProvider;
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
            _configDictionary = ConvertListToDictionary(this._configProvider.Provide(this._applicationName));
        }


        protected Dictionary<string, ConfigModel> ConvertListToDictionary(List<ConfigModel> configs)
        {
            return configs.ToDictionary(x => x.Name);
        }

        public T GetValue<T>(string key) where T : class
        {
            if (!_configDictionary.ContainsKey(key))
            {
                return null;
            }

            var model = this._configDictionary[key];

            if (model == null)
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
                valueList.Add(ParseValue(item.Value));
            }
            return valueList;
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
