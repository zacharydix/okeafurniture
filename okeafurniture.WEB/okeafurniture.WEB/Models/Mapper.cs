using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Entities;
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
                Address = model.Address,
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

        public static CategoryItem MapToCategoryItem(this CategoryItemModel model)
        {
            return new CategoryItem()
            {
                CategoryId = model.CategoryId,
                ItemId = model.ItemId,
                Category = model.Category,
                Item = model.Item
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
                CheckOutDate = model.CheckOutDate
            };
        }

        public static Category MapToCategory(this CategoryModel model)
        {
            return new Category()
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                ImageName = model.ImageName,
                CategoryItems = model.CategoryItems
            };
        }

        public static Item MapToItem(this ItemModel model)
        {
            return new Item()
            {
                ItemId=model.ItemId,
                ItemName=model.ItemName,
                ItemDescription=model.ItemDescription,
                UnitPrice=model.UnitPrice,
                ImageName = model.ImageName,
                CategoryItems = model.CategoryItems
            };
        }

        public static PaymentMethod MapToPaymentMethod(this PaymentMethodModel model)
        {
            return new PaymentMethod()
            {
                PaymentMethodId = model.PaymentMethodId,
                AccountId=model.AccountId,
                CardHolderFirstName=model.CardHolderFirstName,
                CardHolderLastName=model.CardHolderLastName,
                CardNumber=model.CardNumber,
                CardExpiration=model.CardExpiration,
                CardCVV=model.CardCVV,
                BillingAddress=model.BillingAddress
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

            foreach (var account in accounts)
            {
                accountModels.Add(
                    new AccountModel()
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

        public static CategoryItemModel MapToModel(this CategoryItem categoryItem)
        {
            return new CategoryItemModel()
            {
                CategoryId = categoryItem.CategoryId,
                ItemId = categoryItem.ItemId,
                Category = categoryItem.Category,
                Item = categoryItem.Item
            };
        }

        public static List<CartItemModel> MapToModel(this List<CartItem> cartItems)
        {
            var cartItemModels = new List<CartItemModel>();

            foreach (var cartItem in cartItems)
            {
                cartItemModels.Add(
                    new CartItemModel()
                    {
                        CartId = cartItem.CartId,
                        ItemId = cartItem.ItemId,
                        Quantity = cartItem.Quantity,
                        Cart = cartItem.Cart,
                        Item = cartItem.Item
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
                CheckOutDate = cart.CheckOutDate,
                Account = cart.Account,
                PaymentMethod = cart.PaymentMethod,
                CartItems = cart.CartItems
            };
        }

        public static List<CartModel> MapToModel(this List<Cart> carts)
        {
            var cartModels = new List<CartModel>();

            foreach (var cart in carts)
            {
                cartModels.Add(
                    new CartModel()
                    {
                        CartId = cart.CartId,
                        AccountId = cart.AccountId,
                        PaymentMethodId = cart.PaymentMethodId,
                        OrderTotal = cart.OrderTotal,
                        CheckOutDate = cart.CheckOutDate,
                        Account = cart.Account,
                        PaymentMethod = cart.PaymentMethod,
                        CartItems = cart.CartItems
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
                ImageName = category.ImageName,
                CategoryItems = category.CategoryItems

            };
        }

        public static List<CategoryModel> MapToModel(this List<Category> categories)
        {
            var categoryModels = new List<CategoryModel>();

            foreach (var category in categories)
            {
                categoryModels.Add(
                    new CategoryModel()
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        ImageName = category.ImageName,
                        CategoryItems = category.CategoryItems
                    }); ;
            };
            return categoryModels;
        }

        public static ItemModel MapToModel(this Item item)
        {
            return new ItemModel()
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                ImageName = item.ImageName,
                UnitPrice = item.UnitPrice,
                CartItems = item.CartItems,
                CategoryItems = item.CategoryItems
            };
        }

        public static List<ItemModel> MapToModel(this List<Item> items)
        {
            var itemModels = new List<ItemModel>();

            foreach (var item in items)
            {
                itemModels.Add(
                    new ItemModel()
                    {
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        ItemDescription = item.ItemDescription,
                        UnitPrice = item.UnitPrice,
                        CartItems = item.CartItems,
                        CategoryItems = item.CategoryItems
                    });
            };
            return itemModels;
        }

        public static PaymentMethodModel MapToModel(this PaymentMethod paymentMethod)
        {
            return new PaymentMethodModel()
            {
                PaymentMethodId = paymentMethod.PaymentMethodId,
                AccountId = paymentMethod.AccountId,
                CardHolderFirstName = paymentMethod.CardHolderFirstName,
                CardHolderLastName = paymentMethod.CardHolderLastName,
                CardNumber = paymentMethod.CardNumber,
                CardExpiration = paymentMethod.CardExpiration,
                CardCVV = paymentMethod.CardCVV,
                BillingAddress = paymentMethod.BillingAddress,
                Account = paymentMethod.Account
            };
        }

        public static List<PaymentMethodModel> MapToModel(this List<PaymentMethod> paymentMethods)
        {
            var paymentMethodModels = new List<PaymentMethodModel>();

            foreach (var paymentMethod in paymentMethods)
            {
                paymentMethodModels.Add(
                    new PaymentMethodModel()
                    {
                        PaymentMethodId = paymentMethod.PaymentMethodId,
                        AccountId = paymentMethod.AccountId,
                        CardHolderFirstName = paymentMethod.CardHolderFirstName,
                        CardHolderLastName = paymentMethod.CardHolderLastName,
                        CardNumber = paymentMethod.CardNumber,
                        CardExpiration = paymentMethod.CardExpiration,
                        CardCVV = paymentMethod.CardCVV,
                        BillingAddress = paymentMethod.BillingAddress,
                        Account = paymentMethod.Account
                    });
            };
            return paymentMethodModels;
        }
    }
}
