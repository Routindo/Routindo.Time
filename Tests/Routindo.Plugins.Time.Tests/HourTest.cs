using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract.Services;
using Routindo.Plugins.Time.Components;
using Routindo.Plugins.Time.Components.Hourly;
using Routindo.Plugins.Time.Tests.Mock;

namespace Routindo.Plugins.Time.Tests
{
    [TestClass]
    public class HourTest
    {
        [TestMethod]
        [TestCategory("Unit Test")]
        public void OneHourIntervalAfter3MinutesTest() 
        {

            HourWatcher watcher = new HourWatcher()
            {
                Id = Guid.NewGuid().ToString("D").ToUpper(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(HourWatcher), typeof(HourWatcher)),
                Times = 1, Minute = DateTime.Now.Minute + 2,
                DataManager = new PluginDataManagerMock()
            };

            Console.WriteLine("Starting at Minute: {0}", watcher.Minute);
            var firstExec = DateTime.Now;
            var result = watcher.Watch();
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
            Assert.IsFalse(result.Result);
            Thread.Sleep(TimeSpan.FromSeconds(30));

            // Additional wait
            if (firstExec.Second < 30)
            {
                result = watcher.Watch();
                Console.WriteLine("[{0:G}] Planned Time : ({1:G})", DateTime.Now, watcher.DataManager.GetPlannedExecutionTime());
                Assert.IsNotNull(result);
                Assert.IsFalse(result.Result);
                Thread.Sleep(TimeSpan.FromSeconds(30));
            }

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
        }
    }
}
