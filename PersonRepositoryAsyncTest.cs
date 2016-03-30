using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using CodeFirstEntityFramework.Repository;
using CodeFirstEntityFramework.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirstEntityFramework.Test
{    
    [TestClass]
    public class PersonRepositoryAsyncTest
    {
        private readonly ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["CodeFirstEntityFramework"];
        private PersonRepositoryAsync repo;

        [TestInitialize]
        public void Initialize()
        {
            repo = new PersonRepositoryAsync(css);
        }

        [TestMethod]
        public async Task GetAll_OK()
        {
            List<Person> persons = await repo.GetAll();

            Assert.IsNotNull(persons);
            Assert.IsTrue(persons.Count > 0);
        }

        [TestMethod]
        public async Task GetById_OK()
        {
            Person person = await repo.GetById(1);            

            Assert.IsNotNull(person);
        }

        [TestMethod]
        public async Task Update_OK()
        {
            bool status = await repo.Update(new Person { PersonId = 1, FirstName = "Peter2", LastName = "Gabriel" });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task Insert_OK()
        {
            bool status = await repo.Save(new Person { FirstName = "Peter", LastName = "Gabriel" });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task Delete_OK()
        {
            List<Person> people = await repo.GetAll();
            Person lastPerson = people.Last();
            bool status = await repo.Delete(new object[] {lastPerson.PersonId });

            Assert.IsTrue(status);
        }
    }
}
