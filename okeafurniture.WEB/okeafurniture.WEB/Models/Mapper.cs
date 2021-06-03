﻿using okeafurniture.CORE.Entites;
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
                IsAdmin = model.IsAdmin
                // account model will not have nav properties yet!
            };
        }

        public static CartItem MapToCartItem(this CartItemModel model)
        {
            return new CartItem()
            {
                CartId = model.CartId,
                ItemId = model.ItemId,
                Quantity = model.Quantity
            };
        }

        public static Cart MapToCart(this CartModel model)
        {
            return new Cart()
            {
                CartId = model.CartId,
                AccountId = model.AccountId,
                PaymentMethodId = model.PaymentMethodId,
                OrderTotal = model.OrderTotal,
                CheckedOut = model.CheckedOut
            };
        }

        public static Category MapToCategory(this CategoryModel model)
        {
            return new Category()
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName
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

        public static CartItemModel MapToModel(this CartItem cartItem)
        {
            return new CartItemModel()
            {
                CartId = cartItem.CartId,
                ItemId = cartItem.ItemId,
                Quantity = cartItem.Quantity,
                Cart = cartItem.Cart,
                Item = cartItem.Item
            };
        }

        public static List<CartItemModel> MapToModel(this List<CartItem> cartItems)
        {
            var cartItemModels = new List<CartItemModel>();

            foreach (var c in cartItems)
            {
                cartItemModels.Add(
                    new CartItemModel()
                    {
                        CartId = c.CartId,
                        ItemId = c.ItemId,
                        Quantity = c.Quantity,
                        Cart = c.Cart,
                        Item = c.Item
                    });
            }
            return cartItemModels;
        }

        public static CartModel MapToModel(this Cart cart)
        {
            return new CartModel()
            {
                CartId = cart.CartId,
                AccountId = cart.AccountId,
                PaymentMethodId = cart.PaymentMethodId,
                OrderTotal = cart.OrderTotal,
                CheckedOut = cart.CheckedOut,
                Account = cart.Account,
                PaymentMethod = cart.PaymentMethod,
                CartItems = cart.CartItems
            };
        }

        public static List<CartModel> MapToModel(this List<Cart> carts)
        {
            var cartModels = new List<CartModel>();

            foreach (var c in carts)
            {
                cartModels.Add(
                    new CartModel()
                    {
                        CartId = c.CartId,
                        AccountId = c.AccountId,
                        PaymentMethodId = c.PaymentMethodId,
                        OrderTotal = c.OrderTotal,
                        CheckedOut = c.CheckedOut,
                        Account = c.Account,
                        PaymentMethod = c.PaymentMethod,
                        CartItems = c.CartItems
                    });
            }
            return cartModels;
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
