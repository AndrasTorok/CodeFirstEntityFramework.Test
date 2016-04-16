using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using CodeFirstEntityFramework.Repository;
using CodeFirstEntityFramework.Model;

namespace CodeFirstEntityFramework.Test
{
    [TestClass]
    public class MisecllaneousRepositoryTest
    {
        private readonly ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["CodeFirstEntityFramework"];
        private MisecllaneousRepository repo;

        [TestInitialize]
        public void Initialize()
        {
            repo = new MisecllaneousRepository(css);
        }

        [TestMethod]
        public void Get_OK()
        {
            Misecllaneous mis = repo.Get();

            Assert.IsNotNull(mis);
        }

        [TestMethod]
        public void Save_OK()
        {
            Misecllaneous mis = new Misecllaneous { Rate= 4.57, Income=2500.34M, Expense=2000.23M };

            bool status = repo.Save(mis);
            Assert.IsTrue(status);
        }
    }
}
