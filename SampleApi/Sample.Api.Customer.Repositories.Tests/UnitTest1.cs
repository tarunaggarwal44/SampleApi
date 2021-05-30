using Microsoft.SqlServer.Dac;
using NUnit.Framework;
using System;
using System.IO;

namespace Sample.Api.Customer.Repositories.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        public void Initialise(string dacpacRelativePath)
        {
            var _databaseName = $"UnitTestDB_{Guid.NewGuid().ToString("N").ToUpper()}";
            var connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;";

            var instance = new DacServices(connectionString);
            var dacpacPath = Path.GetFullPath(
                Path.Combine(
                    TestContext.CurrentContext.TestDirectory,
                    dacpacRelativePath
                ));

            using (var dacpac = DacPackage.Load(dacpacPath))
            {
                instance.Deploy(dacpac, _databaseName, upgradeExisting: true, options: new DacDeployOptions() { AllowIncompatiblePlatform = true });
            }

            _connectionString = string.Concat(connectionString, $"Initial Catalog={_databaseName}");
            _initialised = true;
        }
    }
}