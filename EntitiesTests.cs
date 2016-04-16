using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CodeFirstEntityFramework.Repository;
using System.Linq;

namespace CodeFirstEntityFramework.Test
{
    [TestClass]
    public class EntitiesTests
    {
        private List<OrderDetail> orderDetails;
        private List<OrderDetail> clonedOrderDetails;
        private Entities<OrderDetail> entities;

        [TestInitialize]
        public void Initialize()
        {
            entities = new Entities<OrderDetail>(new List<string> { "OrderId", "ProductId" });

            orderDetails = new List<OrderDetail>
            {
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 1,
                    Price = 20
                },
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 2,
                    Price = 11
                },
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 3,
                    Price = 12
                },
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 4,
                    Price = 10
                },
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 5,
                    Price = 18
                },
            };

            clonedOrderDetails = orderDetails.Select(od => od.Clone() as OrderDetail).ToList();
        }

        [TestMethod]
        public void Changes_Update_OK()
        {
            orderDetails.First().Price += 2;

            EntityChanges<OrderDetail> changes = entities.EntityChanges(orderDetails, clonedOrderDetails);

            Assert.AreEqual(changes.Added.Count, 0);
            Assert.AreEqual(changes.Deleted.Count, 0);
            Assert.AreEqual(changes.Updated.Count, 1);
        }

        [TestMethod]
        public void Changes_Add_OK()
        {
            OrderDetail addedOrderDetail = new OrderDetail { OrderId = 1, ProductId = 6, Price = 9 };

            orderDetails.Add(addedOrderDetail);

            EntityChanges<OrderDetail> changes = entities.EntityChanges(orderDetails, clonedOrderDetails);

            Assert.AreEqual(changes.Added.Count, 1);
            Assert.AreEqual(changes.Deleted.Count, 0);
            Assert.AreEqual(changes.Updated.Count, 0);
        }

        [TestMethod]
        public void Changes_Delete_OK()
        {
            orderDetails.Remove(orderDetails.Last());

            EntityChanges<OrderDetail> changes = entities.EntityChanges(orderDetails, clonedOrderDetails);

            Assert.AreEqual(changes.Added.Count, 0);
            Assert.AreEqual(changes.Deleted.Count, 1);
            Assert.AreEqual(changes.Updated.Count, 0);
        }

        [TestMethod]
        public void Changes_Modify_PK_OK()
        {
            orderDetails.First().ProductId = 10;

            EntityChanges<OrderDetail> changes = entities.EntityChanges(orderDetails, clonedOrderDetails);

            Assert.AreEqual(changes.Added.Count, 1);
            Assert.AreEqual(changes.Deleted.Count, 1);
            Assert.AreEqual(changes.Updated.Count, 0);
        }
    }

    #region Helper classes

    internal class OrderDetail : ICloneable
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            OrderDetail that = obj as OrderDetail;
            return that != null && OrderId == that.OrderId && ProductId == that.ProductId && Price == that.Price;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", OrderId, ProductId, Price);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    #endregion
}
