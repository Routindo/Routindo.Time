using System;
using System.ComponentModel.Design;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract;
using Routindo.Contract.Services;
using Routindo.Contract.Watchers;
using Routindo.Plugins.Time.Components;
using Routindo.Plugins.Time.Components.Minutely;
using Routindo.Plugins.Time.Tests.Mock;

namespace Routindo.Plugins.Time.Tests
{
    [TestClass]
    public class MinuteTest
    {
        [TestMethod]
        [TestCategory("Unit Test")]
        public void OneMinuteIntervalTest()
        {

            MinuteWatcher watcher = new MinuteWatcher()
            {
                Id = Guid.NewGuid().ToString("D").ToUpper(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(null),
                Times = 1, 
                DataManager = new PluginDataManagerMock()
            };

            var result = watcher.Watch();
            Console.WriteLine("[{0:G}] Planned Time : ({1:G})", DateTime.Now, watcher.DataManager.GetPlannedExecutionTime());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Console.WriteLine("[{0:G}] Planned Time : ({1:G})", DateTime.Now, watcher.DataManager.GetPlannedExecutionTime());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Console.WriteLine("[{0:G}] Planned Time : ({1:G})", DateTime.Now, watcher.DataManager.GetPlannedExecutionTime());
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Result);
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Console.WriteLine("[{0:G}] Planned Time : ({1:G})", DateTime.Now, watcher.DataManager.GetPlannedExecutionTime());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        [TestCategory("Unit Test")]
        public void TwoMinutesIntervalTest()
        {

            MinuteWatcher watcher = new MinuteWatcher()
            {
                Id = Guid.NewGuid().ToString("D").ToUpper(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(null),
                Times = 2,
                DataManager = new PluginDataManagerMock()
            };

            var result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
            Console.WriteLine(watcher.DataManager.GetPlannedExecutionTime().ToString("G"));
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Result);
            Console.WriteLine(watcher.DataManager.GetPlannedExecutionTime().ToString("G"));
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Result);
            Console.WriteLine(watcher.DataManager.GetPlannedExecutionTime().ToString("G"));
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Result);
            Console.WriteLine(watcher.DataManager.GetPlannedExecutionTime().ToString("G"));
            Thread.Sleep(TimeSpan.FromSeconds(30));

            result = watcher.Watch();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Result);
            Console.WriteLine(watcher.DataManager.GetPlannedExecutionTime().ToString("G"));
        }
    }
}
