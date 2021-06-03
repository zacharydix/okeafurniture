﻿using Microsoft.EntityFrameworkCore;
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
                AccountId = 1,
                FirstName = "first1",
                LastName = "last1",
                Email = "test@email.com",
                Password = "password",
                DateOfBirth = DateTime.Now.AddYears(-20),
                IsAdmin = false
            };
            context.Accounts.Add(account);
            payment1 = new PaymentMethod()
            {
                AccountId = 1,
                CardHolderFirstName = "John",
                CardHolderLastName = "Doe",
                CardNumber = "1234123412341234",
                CardExpiration = new DateTime(2050, 01, 01),
                CardCVV = "1234",
                BillingAddress = "1234 Number Drive, Milwaukee, WI 53214",
                Account = account
            };
            context.PaymentMethods.Add(payment1);
            cart1 = new Cart()
            {
                CartId = 1,
                AccountId = 1,
                PaymentMethodId = 1,
                OrderTotal = 2M,
                CheckedOut = false
            };
            cart2 = new Cart()
            {
                CartId = 2,
                AccountId = 1,
                PaymentMethodId = 1,
                OrderTotal = 2M,
                CheckedOut = true
            };
            context.Carts.Add(cart1);
            context.Carts.Add(cart2);
            item1 = new Item()
            {
                ItemId = 1,
                ItemName = "test item 1",
                ItemDescription = "item 1 desc",
                UnitPrice = 2M
            };
            item2 = new Item()
            {
                ItemId = 2,
                ItemName = "test item 2",
                ItemDescription = "item 2 desc",
                UnitPrice = 2M
            };
            context.Items.Add(item1);
            context.Items.Add(item2);
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
            context.CartItems.Add(cartitem1);
            context.CartItems.Add(cartitem2);
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
            cart.CheckedOut = true;

            var result = repository.Update(cart);
            Cart updatedCart = repository.Get(1).Data;

            Assert.IsTrue(result.Success);
            Assert.IsTrue(updatedCart.CheckedOut);
        }

        [Test]
        public void ShouldAdd()
        {
            var result = repository.Add(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Data.AccountId);
        }
    }
}