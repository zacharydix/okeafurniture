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
        private Item item;
        private Item item1;
        private Item item2;
        
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase("okea").Options;
            db = new OkeaFurnitureContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Category category = new Category()
            {
                CategoryName = "Couch"
            };
            item = new Item()
            {
                ItemName = "Couch",
                ItemDescription = "bigCouch",
                UnitPrice = 200.00M,
                ImageURL = "test.png"
            };
            item1 = new Item()
            {

                ItemName = "SuperCouch",
                ItemDescription = "biggerCouch",
                UnitPrice = 999.00M,
                ImageURL = "test.png"
            };
            item2 = new Item()
            {
                ItemName = "UltraMegaCouch",
                ItemDescription = "BiggerBetterBestest",
                UnitPrice = 10000.00M,
                ImageURL = "test.png"
            };
            item.Categories.Add(category);
            item1.Categories.Add(category);
            item2.Categories.Add(category);
            db.SaveChanges();
            itemRepo = new EFItemRepository(db);
        }

        [Test]
        public void InsertingItem()
        {
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
            Assert.AreEqual(expected.Data.ImageURL, actual.Data.ImageURL);
            Assert.AreEqual(expected.Message, actual.Message);
        }

        [Test]
        public void GetItemById()
        {
            db.Item.Add(item);
            db.SaveChanges();

            Response<Item> actual = itemRepo.Get(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Successfully retrieved Item", actual.Message);
            Assert.AreEqual(item.ItemId, actual.Data.ItemId);

        }
        [Test]
        public void GetListofItemsByCategoryID()
        {
            itemRepo.Insert(item);
            itemRepo.Insert(item1);
            itemRepo.Insert(item2);
            db.SaveChanges();

            Response<List<Item>> actual = itemRepo.GetByCategory(1);
            Assert.AreEqual(3, actual.Data.Count);
        }
        [Test]
        public void ItemShouldUpdate()
        {
            Response expected = new Response()
            {
                Success = true,
                Message = "update was successful"
            };

            itemRepo.Insert(item);
            db.SaveChanges();

            item.ItemName = "Ghost";
            Response actual = itemRepo.Update(item);

            Assert.AreEqual(expected.Message, actual.Message);
            Assert.IsTrue(actual.Success);
        }
        [Test]
        public void ShouldDeleteItem()
        {
            itemRepo.Insert(item);
            db.SaveChanges();

            Response actual = itemRepo.Delete(1);

            Assert.IsTrue(actual.Success);

        }
        [Test]
        public void GetAllItems()
        {
            itemRepo.Insert(item);
            itemRepo.Insert(item1);
            itemRepo.Insert(item2);
            db.SaveChanges();

            Response<List<Item>> actual = itemRepo.GetAll();
            Assert.AreEqual(3, actual.Data.Count);
        }
    }
}
