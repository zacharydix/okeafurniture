using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using okeafurniture.DAL.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace okeafurniture.DAL.Tests
{
    class EFPaymentMethodRepositoryTests
    {
        IPaymentMethodRepository repository;
        Account account1;
        PaymentMethod payment1;
        PaymentMethod payment2;
        OkeaFurnitureContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase("okea").Options;
            context = new OkeaFurnitureContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repository = new EFPaymentMethodRepository(context);
            account1 = new Account()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@gmail.com",
                Password = "johndoe123",
                DateOfBirth = new DateTime(1990, 01, 01),
                IsAdmin = false
            };
            context.Account.Add(account1);
            context.SaveChanges();
            payment1 = new PaymentMethod()
            {
                AccountId = account1.AccountId,
                CardHolderFirstName = "John",
                CardHolderLastName = "Doe",
                CardNumber = "1234123412341234",
                CardExpiration = new DateTime(2050, 01, 01),
                CardCVV = "1234",
                BillingAddress = "1234 Number Drive, Milwaukee, WI 53214",
                Account = account1
            };
            payment2 = new PaymentMethod()
            {
                AccountId = account1.AccountId,
                CardHolderFirstName = "Jane",
                CardHolderLastName = "Doe",
                CardNumber = "1234123412341234",
                CardExpiration = new DateTime(2050, 10, 10),
                CardCVV = "4321",
                BillingAddress = "4321 Count Drive, Milwaukee, WI 53214",
                Account = account1
            };
            context.PaymentMethod.Add(payment1);
            context.SaveChanges();
        }

        [Test]
        public void ShouldGetPaymentMethod()
        {
            Response<PaymentMethod> expected = new Response<PaymentMethod>()
            {
                Data = payment1,
                Success = true,
                Message = "Successfully retrieved payment method."
            };
            Response<PaymentMethod> actual = repository.Get(payment1.PaymentMethodId);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data.PaymentMethodId, actual.Data.PaymentMethodId);
            Assert.AreEqual(expected.Data.AccountId, actual.Data.AccountId);
            Assert.AreEqual(expected.Data.CardHolderFirstName, actual.Data.CardHolderFirstName);
            Assert.AreEqual(expected.Data.CardHolderLastName, actual.Data.CardHolderLastName);
            Assert.AreEqual(expected.Data.CardNumber, actual.Data.CardNumber);
            Assert.AreEqual(expected.Data.CardExpiration, actual.Data.CardExpiration);
            Assert.AreEqual(expected.Data.CardCVV, actual.Data.CardCVV);
            Assert.AreEqual(expected.Data.BillingAddress, actual.Data.BillingAddress);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void ShouldGetPaymentMethodByUser()
        {
            context.PaymentMethod.Add(payment2);
            context.SaveChanges();
            Response<List<PaymentMethod>> expected = new Response<List<PaymentMethod>>()
            {
                Data = new List<PaymentMethod>()
                {
                    payment1,
                    payment2
                },
                Success = true,
                Message = "Successfully retrieved payment methods."
            };
            Response<List<PaymentMethod>> actual = repository.GetByUser(account1.AccountId);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data[0].PaymentMethodId, actual.Data[0].PaymentMethodId);
            Assert.AreEqual(expected.Data[0].AccountId, actual.Data[0].AccountId);
            Assert.AreEqual(expected.Data[0].CardHolderFirstName, actual.Data[0].CardHolderFirstName);
            Assert.AreEqual(expected.Data[0].CardHolderLastName, actual.Data[0].CardHolderLastName);
            Assert.AreEqual(expected.Data[0].CardNumber, actual.Data[0].CardNumber);
            Assert.AreEqual(expected.Data[0].CardExpiration, actual.Data[0].CardExpiration);
            Assert.AreEqual(expected.Data[0].CardCVV, actual.Data[0].CardCVV);
            Assert.AreEqual(expected.Data[0].BillingAddress, actual.Data[0].BillingAddress);

            Assert.AreEqual(expected.Data[1].PaymentMethodId, actual.Data[1].PaymentMethodId);
            Assert.AreEqual(expected.Data[1].AccountId, actual.Data[1].AccountId);
            Assert.AreEqual(expected.Data[1].CardHolderFirstName, actual.Data[1].CardHolderFirstName);
            Assert.AreEqual(expected.Data[1].CardHolderLastName, actual.Data[1].CardHolderLastName);
            Assert.AreEqual(expected.Data[1].CardNumber, actual.Data[1].CardNumber);
            Assert.AreEqual(expected.Data[1].CardExpiration, actual.Data[1].CardExpiration);
            Assert.AreEqual(expected.Data[1].CardCVV, actual.Data[1].CardCVV);
            Assert.AreEqual(expected.Data[1].BillingAddress, actual.Data[1].BillingAddress);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void ShouldAddPaymentMethod()
        {
            Response<PaymentMethod> expected = new Response<PaymentMethod>()
            {
                Data = payment2,
                Success = true,
                Message = "Successfully added payment method."
            };
            Response<PaymentMethod> actual = repository.Add(payment2);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data.PaymentMethodId, actual.Data.PaymentMethodId);
            Assert.AreEqual(expected.Data.AccountId, actual.Data.AccountId);
            Assert.AreEqual(expected.Data.CardHolderFirstName, actual.Data.CardHolderFirstName);
            Assert.AreEqual(expected.Data.CardHolderLastName, actual.Data.CardHolderLastName);
            Assert.AreEqual(expected.Data.CardNumber, actual.Data.CardNumber);
            Assert.AreEqual(expected.Data.CardExpiration, actual.Data.CardExpiration);
            Assert.AreEqual(expected.Data.CardCVV, actual.Data.CardCVV);
            Assert.AreEqual(expected.Data.BillingAddress, actual.Data.BillingAddress);
            Assert.AreEqual(expected.Message, actual.Message);

            Assert.AreEqual(2, context.PaymentMethod.ToList().Count());
        }

        [Test]
        public void ShouldUpdatePaymentMethod()
        {
            Response expected = new Response()
            {
                Success = true,
                Message = "Successfully updated payment method."
            };
            payment1.AccountId = payment2.AccountId;
            payment1.CardHolderFirstName = payment2.CardHolderFirstName;
            payment1.CardHolderLastName = payment2.CardHolderLastName;
            payment1.CardNumber = payment2.CardNumber;
            payment1.CardExpiration = payment2.CardExpiration;
            payment1.CardCVV = payment2.CardCVV;
            payment1.BillingAddress = payment2.BillingAddress;
            payment1.Account = payment2.Account;
            Response actual = repository.Update(payment1);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

            PaymentMethod temp = context.PaymentMethod.Find(payment1.PaymentMethodId);
            Assert.AreEqual(payment1.PaymentMethodId, temp.PaymentMethodId);
            Assert.AreEqual(payment1.AccountId, temp.AccountId);
            Assert.AreEqual(payment1.CardHolderFirstName, temp.CardHolderFirstName);
            Assert.AreEqual(payment1.CardHolderLastName, temp.CardHolderLastName);
            Assert.AreEqual(payment1.CardNumber, temp.CardNumber);
            Assert.AreEqual(payment1.CardExpiration, temp.CardExpiration);
            Assert.AreEqual(payment1.CardCVV, temp.CardCVV);
            Assert.AreEqual(payment1.BillingAddress, temp.BillingAddress);
        }

        [Test]
        public void ShouldDeletePaymentMethod()
        {
            Response expected = new Response()
            {
                Success = true,
                Message = "Successfully deleted payment method."
            };
            Response actual = repository.Delete(payment1.PaymentMethodId);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

            Assert.AreEqual(0, context.PaymentMethod.ToList().Count());
        }
    }
}
