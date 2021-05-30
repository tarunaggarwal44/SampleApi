using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Api.Customer.Repositories.Tests
{
    [SetUpFixture]
    public class DatabaseTestConfig
    {
        public static TestDatabase TestDatabase { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            TestDatabase = new TestDatabase();
            TestDatabase.Initialise(@"..\..\..\NUnitSQLIntegrationTesting.Database\bin\debug\NUnitSQLIntegrationTesting.Database.dacpac");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (TestDatabase != null)
                TestDatabase.Dispose();
        }
    }
}
