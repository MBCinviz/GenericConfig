using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericConfigCore.Tests
{
    [TestClass]
    public class GenericConfigReaderTest
    {
        private readonly GenericConfigReader _genericConfigReader;

        public GenericConfigReaderTest()
        {
            var mockProvider = new MockConfigProvider();
            _genericConfigReader = new GenericConfigReader("Test1", mockProvider, 15000);
        }

        [TestMethod]
        public void TestAllReaderCore()
        {
            Assert.AreEqual(_genericConfigReader.GetValue<int>("Age"), 25);
            Assert.AreEqual(_genericConfigReader.GetValue<string>("Name"), "Bilal");
            Assert.ThrowsException<ConfigNotFoundException>(() => _genericConfigReader.GetValue<int>("Height")); // not found
            Assert.ThrowsException<ConfigNotFoundException>(() => _genericConfigReader.GetValue<int>("Surname")); // another app
            Assert.ThrowsException<ConfigNotFoundException>(() => _genericConfigReader.GetValue<int>("Rate")); // deactive
        }
    }
}
