﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract.Services;
using Routindo.Plugins.Time.Tests.Mock;

namespace Routindo.Plugins.Time.Tests
{
    [TestClass]
    public class TestAssemblyInit
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        { 
            ServicesContainer.SetServicesProvider(new FakeServicesProvider());
        }
    }
}
