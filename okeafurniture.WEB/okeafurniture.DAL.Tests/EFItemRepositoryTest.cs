using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using okeafurniture.DAL.EFRepositories;
using System.Collections.Generic;
namespace okeafurniture.DAL.Tests
{
    public class EFItemRepositoryTests
    {
        private OkeaFurnitureContext db;
        private IItemRepository itemRepo;

        private static readonly Category CATEGORY = MakeCategory();
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase("okea").Options;
            db = new OkeaFurnitureContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Item item1 = MakeItem();
            Item item2 = MakeItem1();
            Category category = MakeCategory();
            db.SaveChanges();

            itemRepo = new EFItemRepository(db);
        }

        [Test]
        public void InsertingItem()
        {
            Item item = MakeItem();

            Response<Item> expected = new Response<Item>()
            {
                Data = item,
                Success = true,
                Message = "Successfully inserted Item"
            };

            Response<Item> actual = itemRepo.Insert(item);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Data.ItemId, actual.Data.ItemId);
            Assert.AreEqual(expected.Data.ItemName, actual.Data.ItemName);
            Assert.AreEqual(expected.Data.ItemDescription, actual.Data.ItemDescription);
            Assert.AreEqual(expected.Data.UnitPrice, actual.Data.UnitPrice);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void GetItemById()
        {
            Item item = MakeItem();

            db.Items.Add(item);
            db.SaveChanges();

            Response<Item> actual = itemRepo.Get(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Successfully retrieved Item", actual.Message);
            Assert.AreEqual(item.ItemId, actual.Data.ItemId);

        }
        [Test]
        public void GetListofItemsByCategoryID()
        {
            Item item = MakeItem();
            itemRepo.Insert(item);

            Item item1 = MakeItem1();
            Item item2 = MakeItem2();

            itemRepo.Insert(item1);
            itemRepo.Insert(item2);

            Response<List<Item>> response = new Response<List<Item>>();


            response = itemRepo.GetByCategory(1);
            Assert.AreEqual(3, response.Data.Count);
        }
        [Test]
        public void ItemShouldUpdate()
        {
            Item item = MakeItem();
            itemRepo.Insert(item);

            Response expected = new Response()
            {
                Success = true,
                Message = "update was successful"
            };
            Response actual = new Response();
            var itemToUpdate = item;

            itemRepo.Insert(itemToUpdate);


            itemToUpdate.ItemName = "Ghost";
            actual = itemRepo.Update(itemToUpdate);

            Assert.AreEqual(expected.Message, actual.Message);
            Assert.IsTrue(actual.Success);
        }
        [Test]
        public void ShouldDeleteItem()
        {
            Item item = MakeItem();
            itemRepo.Insert(item);

            Response aResponse = new Response();
            itemRepo.Insert(item);
            itemRepo.Insert(item);
            aResponse = itemRepo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }
        [Test]
        public void GetAllItems()
        {
            Item item = MakeItem();
            itemRepo.Insert(item);

            Item item1 = MakeItem1();
            Item item2 = MakeItem2();

            itemRepo.Insert(item1);
            itemRepo.Insert(item2);

            Response<List<Item>> response = new Response<List<Item>>();

            itemRepo.Insert(item);
            itemRepo.Insert(item1);
            itemRepo.Insert(item2);

            response = itemRepo.GetAll();
            Assert.AreEqual(3, response.Data.Count);
        }
        //supporting Code
        public static Item MakeItem()
        {
            Item item = new Item()
            {

                ItemName = "Couch",
                ItemDescription = "bigCouch",
                UnitPrice =200.00M
            };
            item.Categories.Add(CATEGORY);
            return item;
        }
        public static Item MakeItem1()
        {
            Item item = new Item()
            {

                ItemName = "SuperCouch",
                ItemDescription = "biggerCouch",
                UnitPrice = 999.00M
            };
            item.Categories.Add(CATEGORY);

            return item;
        }
        public static Item MakeItem2()
        {
            Item item = new Item()
            {

                ItemName = "UltraMegaCouch",
                ItemDescription = "BiggerBetterBestest",
                UnitPrice = 10000.00M
            };
            item.Categories.Add(CATEGORY);
            return item;
        }

        public static Category MakeCategory()
        {
            Category category = new Category()
            {
                CategoryName = "Couch"       
            };
            
            return category;
        }
    }
}
