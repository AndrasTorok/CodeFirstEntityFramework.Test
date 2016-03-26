using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using CodeFirstEntityFramework.Repository;
using CodeFirstEntityFramework.Model;

namespace CodeFirstEntityFramework.Test
{    
    [TestClass]
    public class PersonRepositoryTest
    {
        private ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["CodeFirstEntityFramework"];
        PersonRepository repo;

        [TestInitialize]
        public void Initialize()
        {
            repo = new PersonRepository(css);
        }

        [TestMethod]
        public void GetById_OK()
        {
            Person person = repo.GetById(1);

            Assert.IsNotNull(person);
        }

        [TestMethod]
        public void Update_OK()
        {
            bool status = repo.Update(new Person { PersonId = 1, FirstName = "Peter2", LastName = "Gabriel" });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Insert_OK()
        {
            bool status = repo.Save(new Person { FirstName = "Peter", LastName = "Gabriel" });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Delete_OK()
        {
            bool status = repo.Delete(new object[] { 2 });

            Assert.IsTrue(status);
        }
    }
}
