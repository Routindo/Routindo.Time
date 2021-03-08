using Microsoft.VisualStudio.TestTools.UnitTesting;
using Umator.Contract.Services;
using Umator.Plugins.Email.Tests.Mock;

namespace Umator.Plugins.Email.Tests
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
