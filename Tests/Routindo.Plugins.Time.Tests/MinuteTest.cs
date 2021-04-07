using System;
using System.ComponentModel.Design;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract;
using Routindo.Contract.Services;
using Routindo.Plugins.Time.Components;

namespace Routindo.Plugins.Time.Tests
{
    [TestClass]
    public class MinuteTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            IWatcher watcher = new MinuteWatcher()
            {
                Id = Guid.NewGuid().ToString("D").ToUpper(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(null),
                Times = 1
            };

            var result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);

            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Result);

            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
        }
    }
}