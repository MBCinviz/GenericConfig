using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericConfigCore.Tests
{
    [TestClass]
    public class GenericConfigManagerTest
    {
        private readonly GenericConfigManager _genericConfigManager;
        public GenericConfigManagerTest()
        {
            var mockEditor = new MockConfigEditor();
            _genericConfigManager = new GenericConfigManager("Test1", mockEditor, 5000);
        }


        [TestMethod]
        public void TestAllReaderCore()
        {
            var savingObject = getObject();
            Assert.ThrowsException<ConfigNotFoundException>(() => _genericConfigManager.GetValue<int>(savingObject.Name));

            _genericConfigManager.AddConfig(savingObject);
            Assert.AreEqual(_genericConfigManager.GetValue<int>(savingObject.Name), 75);

            savingObject.Value = "100";
            _genericConfigManager.UpdateConfig(savingObject);
            Assert.AreEqual(_genericConfigManager.GetValue<int>(savingObject.Name), 100);

            _genericConfigManager.DeleteConfig(savingObject.Name, savingObject.ApplicationName);
            Assert.ThrowsException<ConfigNotFoundException>(() => _genericConfigManager.GetValue<int>(savingObject.Name));
        }

        private ConfigModel getObject()
        {
            var conf1 = new ConfigModel();
            conf1.ApplicationName = "Test1";
            conf1.Id = "5";
            conf1.IsActive = true;
            conf1.Type = ConfigTypeEnum.INTEGER;
            conf1.Name = "Weigth";
            conf1.Value = "75";
            return conf1;
        }
    }
}
