using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Models
{
    public static class Mapper
    {
        public static Account MapToAccount(this AccountModel model)
        {
            return new Account()
            {
                AccountId = model.AccountId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                DateOfBirth = model.DateOfBirth,
                IsAdmin = model.IsAdmin,
                PaymentMethods = model.PaymentMethods,
                Carts = model.Carts
            };
        }
        public static Category MapToCategory(this CategoryModel model)
        {
            return new Category()
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Items = model.Items
            };
        }

        public static AccountModel MapToModel(this Account account)
        {
            return new AccountModel()
            {
                AccountId = account.AccountId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Password = account.Password,
                DateOfBirth = account.DateOfBirth,
                IsAdmin = account.IsAdmin,
                PaymentMethods = account.PaymentMethods,
                Carts = account.Carts
            };
        }

        public static List<AccountModel> MapToModel(this List<Account> accounts)
        {
            var accountModels = new List<AccountModel>();

            foreach (var a in accounts)
            {
                accountModels.Add(
                    new AccountModel()
                    {
                        AccountId = a.AccountId,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        Password = a.Password,
                        DateOfBirth = a.DateOfBirth,
                        IsAdmin = a.IsAdmin,
                        PaymentMethods = a.PaymentMethods,
                        Carts = a.Carts
                    });
            }
            return accountModels;
        }

        public static CategoryModel MapToModel(this Category category)
        {
            return new CategoryModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Items = category.Items
            };
        }

        public static List<CategoryModel> MapToModel(this List<Category> categories)
        {
            var categoryModels = new List<CategoryModel>();

            foreach (var c in categories)
            {
                categoryModels.Add(
                    new CategoryModel()
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                        Items = c.Items
                    });
            };
            return categoryModels;
        }
    }
}
