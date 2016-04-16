using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using CodeFirstEntityFramework.Repository;
using CodeFirstEntityFramework.Model;
using System.Collections.Generic;
using System.Linq;

namespace CodeFirstEntityFramework.Test
{    
    [TestClass]
    public class AddressRepositoryTest
    {
        private readonly ConnectionStringSettings css = ConfigurationManager.ConnectionStrings["CodeFirstEntityFramework"];
        private AddressRepository repo;

        [TestInitialize]
        public void Initialize()
        {
            repo = new AddressRepository(css);
        }

        [TestMethod]
        public void GetById_OK()
        {
            Address address = repo.GetById("Romania", "Brasov", "Orastiei", "10");

            Assert.IsNotNull(address);
        }

        [TestMethod]
        public void Update_OK()
        {
            bool status = repo.Update(new Address { Country = "Romania", Locality = "Brasov", Street = "Orastiei", Number = "10", Coordinate="Not provided" });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Insert_OK()
        {
            bool status = repo.Save(new Address { Country = "Romania", Locality = "Brasov", Street = "Orastiei", Number = "10" });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public void Delete_OK()
        {
            List<Address> addresses = repo.GetAll().ToList();
            Address lastAddress = addresses.Last();
            bool status = repo.Delete(new object[] { lastAddress.Country, lastAddress.Locality, lastAddress.Street, lastAddress.Number });

            Assert.IsTrue(status);
        }

        [TestMethod]
        public void CanFindOutPropertiesWithValues()
        {
            Address address = new Address { Country = "Spain" };
            Type addressType = typeof(Address);

            Dictionary<string, object> propetiesWithValues =
                (from p in addressType.GetProperties()
                 let val = p.GetValue(address)
                 let defaultValue = addressType.IsValueType ? Activator.CreateInstance(addressType) : null
                 where val != defaultValue
                 select p).ToDictionary(p => p.Name, p => p.GetValue(address));

        }
    }
}
