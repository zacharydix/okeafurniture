using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.DAL.EFRepositories;
using okeafurniture.CORE.Entites;

namespace okeafurniture.DAL.Tests
{
    public class EFCartRepositoryTests
    {
        private ICartRepository repository;
        Cart cart1;
        Cart cart2;
        Item item1;
        Item item2;
        Account account;
        CartItem cartitem1;
        CartItem cartitem2;
        PaymentMethod payment1;

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
            payment1 = new PaymentMethod()
            {
                CardHolderFirstName = "John",
                CardHolderLastName = "Doe",
                CardNumber = "1234123412341234",
                CardExpiration = new DateTime(2050, 01, 01),
                CardCVV = "1234",
                BillingAddress = "1234 Number Drive, Milwaukee, WI 53214",
                Account = account
            };
            context.PaymentMethod.Add(payment1);
            cart1 = new Cart()
            {
                AccountId = 1,
                PaymentMethodId = 1,
                OrderTotal = 2M,
                CheckOutDate = null
            };
            cart2 = new Cart()
            {
                AccountId = 1,
                PaymentMethodId = 1,
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
            cartitem1 = new CartItem()
            {
                CartId = 1,
                ItemId = 1,
                Quantity = 1
            };
            cartitem2 = new CartItem()
            {
                CartId = 2,
                ItemId = 1,
                Quantity = 1
            };
            context.CartItem.Add(cartitem1);
            context.CartItem.Add(cartitem2);
            context.SaveChanges();
            repository = new EFCartRepository(context);
        }

        [Test]
        public void ShouldGetAll()
        {
            var result = repository.GetAll();

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.Count);
        }

        [Test]
        public void ShouldGetByCartId()
        {
            var result = repository.Get(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.CartId);
        }

        [Test]
        public void ShouldGetActiveByAccount()
        {
            var result = repository.GetActive(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.CartId);
        }

        [Test]
        public void ShouldGetByAccount()
        {
            var result = repository.GetAllByAccount(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.Count);
        }

        [Test]
        public void ShouldGetByStatus()
        {
            var result1 = repository.GetAllByStatus(false);
            var result2 = repository.GetAllByStatus(true);

            Assert.AreEqual(1, result1.Data[0].CartId);
            Assert.AreEqual(2, result2.Data[0].CartId);
        }

        [Test]
        public void ShouldUpdate()
        {
            Cart cart = repository.Get(1).Data;
            cart.CheckOutDate = DateTime.Now;

            var result = repository.Update(cart);
            Cart updatedCart = repository.Get(1).Data;

            Assert.IsTrue(result.Success);
            Assert.IsTrue(updatedCart.CheckOutDate != null);
        }

        [Test]
        public void ShouldAdd()
        {
            cart1 = new Cart()
            {
                AccountId = 1,
                PaymentMethodId = 1,
                OrderTotal = 10M,
                CheckOutDate = null
            };
            var result = repository.Add(cart1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.AccountId);
        }
    }
}
