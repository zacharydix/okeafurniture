using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using okeafurniture.DAL.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.Tests
{
    public class EFCartItemRepositoryTests
    {
        private ICartItemRepository repository;
        Cart cart1;
        Cart cart2;
        Item item1;
        Item item2;
        Account account;
        CartItem cartitem1_1;
        CartItem cartitem1_2;
        CartItem cartitem2_1;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
            .UseInMemoryDatabase(databaseName: "okea").Options;
            var context = new OkeaFurnitureContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            account = new Account()
            {
                FirstName = "first1",
                LastName = "last1",
                Email = "test@email.com",
                Password = "password",
                DateOfBirth = DateTime.Now.AddYears(-20),
                IsAdmin = false
            };
            context.Account.Add(account);
            cart1 = new Cart()
            {
                AccountId = 1,
                OrderTotal = 2M,
                CheckOutDate = null
            };
            cart2 = new Cart()
            {
                AccountId = 1,
                OrderTotal = 2M,
                CheckOutDate = DateTime.Now
            };
            context.Cart.Add(cart1);
            context.Cart.Add(cart2);
            item1 = new Item()
            {
                ItemName = "test item 1",
                ItemDescription = "item 1 desc",
                UnitPrice = 2M
            };
            item2 = new Item()
            {
                ItemName = "test item 2",
                ItemDescription = "item 2 desc",
                UnitPrice = 2M
            };
            context.Item.Add(item1);
            context.Item.Add(item2);
            context.SaveChanges();
            cartitem1_1 = new CartItem()
            {
                CartId = 1,
                ItemId = 1,
                Quantity = 1
            };
            cartitem1_2 = new CartItem()
            {
                CartId = 1,
                ItemId = 2,
                Quantity = 1
            };
            cartitem2_1 = new CartItem()
            {
                CartId = 2,
                ItemId = 1,
                Quantity = 1
            };
            context.CartItem.Add(cartitem1_1);
            context.CartItem.Add(cartitem1_2);
            context.CartItem.Add(cartitem2_1);
            context.SaveChanges();
            repository = new EFCartItemRepository(context);
        }

        [Test]
        public void ShouldGet()
        {

            var response = repository.Get(1, 1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.CartId);
            Assert.AreEqual(1, response.Data.ItemId);
            Assert.AreEqual(1, response.Data.Quantity);
        }

        [Test]
        public void ShouldGetByCart()
        {
            var response = repository.GetByCart(1);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(2, response.Data.Count);
            Assert.AreEqual(2, response.Data[0].ItemId);
            Assert.AreEqual(1, response.Data[1].ItemId);
        }

        [Test]
        public void ShouldUpdate()
        {
            CartItem cartItem = repository.Get(1, 1).Data;
            cartItem.Quantity = 2;

            var result = repository.Update(cartItem);
            CartItem updatedCartItem = repository.Get(1, 1).Data;

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, updatedCartItem.Quantity);
        }

        [Test]
        public void ShouldAdd()
        {
            var result = repository.Add(new CartItem() { CartId = 2, ItemId = 2, Quantity = 1 });

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.CartId);
            Assert.AreEqual(2, result.Data.ItemId);
            Assert.AreEqual(1, result.Data.Quantity);
        }

        [Test]
        public void ShouldDelete()
        {
            var deleteResult = repository.Delete(1, 1);
            var getResult = repository.Get(1, 1);

            Assert.IsTrue(deleteResult.Success);
            Assert.IsFalse(getResult.Success);
        }
    }
}
