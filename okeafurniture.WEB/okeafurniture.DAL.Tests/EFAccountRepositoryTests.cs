using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace okeafurniture.DAL.Tests
{
    public class EFAccountRepositoryTests
    {
        IAccountRepository repository;
        Account account1;
        Account account2;
        OkeaFurnitureContext context;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase(databaseName: "okea").Options;
            context = new OkeaFurnitureContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repository = new EFAccountRepository(context);
            account1 = new Account()
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@gmail.com",
                Password = "johndoe123",
                DateOfBirth = new DateTime(1990, 01, 01),
                IsAdmin = false
            };
            account2 = new Account()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "janedoe@gmail.com",
                Password = "janedoe123",
                DateOfBirth = new DateTime(1992, 01, 01),
                IsAdmin = false
            };
            context.Accounts.Add(account1);
            context.SaveChanges();


        }

        [Test]
        public void ShouldGetAccount()
        {
            Response<Account> expected = new Response<Account>()
            {
                Data = account1,
                Success = true,
                Message = "Successfully retrieved account."
            };
            Response<Account> actual = repository.Get(expected.Data.AccountId);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data.AccountId,actual.Data.AccountId);
            Assert.AreEqual(expected.Data.FirstName,actual.Data.FirstName);
            Assert.AreEqual(expected.Data.LastName,actual.Data.LastName);
            Assert.AreEqual(expected.Data.Email,actual.Data.Email);
            Assert.AreEqual(expected.Data.Password,actual.Data.Password);
            Assert.AreEqual(expected.Data.DateOfBirth,actual.Data.DateOfBirth);
            Assert.AreEqual(expected.Data.IsAdmin,actual.Data.IsAdmin);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void ShouldGetAllAccounts()
        {
            context.Accounts.Add(account2);
            context.SaveChanges();
            Response<List<Account>> expected = new Response<List<Account>>()
            {
                Data = new List<Account>()
                {
                    account1,
                    account2
                },
                Success = true,
                Message = "Successfully retrieved accounts."
            };
            Response<List<Account>> actual = repository.GetAll();
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data[0].AccountId, actual.Data[0].AccountId);
            Assert.AreEqual(expected.Data[0].FirstName, actual.Data[0].FirstName);
            Assert.AreEqual(expected.Data[0].LastName, actual.Data[0].LastName);
            Assert.AreEqual(expected.Data[0].Email, actual.Data[0].Email);
            Assert.AreEqual(expected.Data[0].Password, actual.Data[0].Password);
            Assert.AreEqual(expected.Data[0].DateOfBirth, actual.Data[0].DateOfBirth);
            Assert.AreEqual(expected.Data[0].IsAdmin, actual.Data[0].IsAdmin);
            Assert.AreEqual(expected.Data[1].AccountId, actual.Data[1].AccountId);
            Assert.AreEqual(expected.Data[1].FirstName, actual.Data[1].FirstName);
            Assert.AreEqual(expected.Data[1].LastName, actual.Data[1].LastName);
            Assert.AreEqual(expected.Data[1].Email, actual.Data[1].Email);
            Assert.AreEqual(expected.Data[1].Password, actual.Data[1].Password);
            Assert.AreEqual(expected.Data[1].DateOfBirth, actual.Data[1].DateOfBirth);
            Assert.AreEqual(expected.Data[1].IsAdmin, actual.Data[1].IsAdmin);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void ShouldGetAccountByEmail()
        {
            Response<Account> expected = new Response<Account>()
            {
                Data = account1,
                Success = true,
                Message = "Successfully retrieved account."
            };
            Response<Account> actual = repository.GetByEmail(account1.Email);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data.AccountId, actual.Data.AccountId);
            Assert.AreEqual(expected.Data.FirstName, actual.Data.FirstName);
            Assert.AreEqual(expected.Data.LastName, actual.Data.LastName);
            Assert.AreEqual(expected.Data.Email, actual.Data.Email);
            Assert.AreEqual(expected.Data.Password, actual.Data.Password);
            Assert.AreEqual(expected.Data.DateOfBirth, actual.Data.DateOfBirth);
            Assert.AreEqual(expected.Data.IsAdmin, actual.Data.IsAdmin);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void ShouldAddAccount()
        {
            Response<Account> expected = new Response<Account>()
            {
                Data = account2,
                Success = true,
                Message = "Successfully added account."
            };
            Response<Account> actual = repository.Add(expected.Data);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data.AccountId, actual.Data.AccountId);
            Assert.AreEqual(expected.Data.FirstName, actual.Data.FirstName);
            Assert.AreEqual(expected.Data.LastName, actual.Data.LastName);
            Assert.AreEqual(expected.Data.Email, actual.Data.Email);
            Assert.AreEqual(expected.Data.Password, actual.Data.Password);
            Assert.AreEqual(expected.Data.DateOfBirth, actual.Data.DateOfBirth);
            Assert.AreEqual(expected.Data.IsAdmin, actual.Data.IsAdmin);
            Assert.AreEqual(expected.Message, actual.Message);

            Assert.AreEqual(2, context.Accounts.ToList().Count);
        }

        [Test]
        public void ShouldUpdateAccount()
        {
            Response expected = new Response()
            {
                Success = true,
                Message = "Successfully updated account."
            };
            account1.FirstName = account2.FirstName;
            account1.LastName = account2.LastName;
            account1.Email = account2.Email;
            account1.Password = account2.Password;
            account1.DateOfBirth = account2.DateOfBirth;
            account1.IsAdmin = account2.IsAdmin;
            Response actual = repository.Update(account1);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

            Account temp = context.Accounts.Find(account1.AccountId);
            Assert.AreEqual(account1.FirstName, temp.FirstName);
            Assert.AreEqual(account1.LastName, temp.LastName);
            Assert.AreEqual(account1.Email, temp.Email);
            Assert.AreEqual(account1.Password, temp.Password);
            Assert.AreEqual(account1.DateOfBirth, temp.DateOfBirth);
            Assert.AreEqual(account1.IsAdmin, temp.IsAdmin);
        }

        [Test]
        public void ShouldDeleteAccount()
        {
            Response expected = new Response()
            {
                Success = true,
                Message = "Successfully deleted account."
            };
            Response actual = repository.Delete(account1.AccountId);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

            Assert.AreEqual(0, context.Accounts.ToList().Count);

        }
    }
}