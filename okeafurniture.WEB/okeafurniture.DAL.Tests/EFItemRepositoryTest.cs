using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.DAL.EFRepositories;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
namespace okeafurniture.DAL.Tests
{
    public class EFItemRepositoryTests
    {
        private OkeaFurnitureContext db;
        private IItemRepository itemRepo;

        public readonly static Item ITEM = MakeItem();
        public readonly static Item ITEM1 = MakeItem1();
        public readonly static Item ITEM2 = MakeItem2();
        public readonly static Category CATEGORY = MakeCategory();


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase(databaseName: "okea").Options;
            db = new OkeaFurnitureContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Categories.Add(CATEGORY);
            db.SaveChanges();

            itemRepo = new EFItemRepository(db);
        }

        [Test]
        public void InsertingItem()
        {
            Response<Item> result = itemRepo.Insert(ITEM);
            Assert.IsTrue(result.Success);
            //put message assert here also
            //and data assert here

            var response = db.Items.Find(1);
            Assert.AreEqual(response.ItemName, ITEM.ItemName); //backwards, should be expected then actual
            Assert.AreEqual(CATEGORY.CategoryId, response.Categories[0].CategoryId);
            Assert.AreEqual(CATEGORY.CategoryName, response.Categories[0].CategoryName);
            Assert.AreEqual(response.ItemDescription, ITEM.ItemDescription);
            Assert.AreEqual(response.UnitPrice,ITEM.UnitPrice);
        }

        [Test]
        public void GetItemById()
        {
            Response<Item> response = new Response<Item>();
            itemRepo.Insert(ITEM);

            response.Data = ITEM;
            var fromMethod = itemRepo.Get(1);

            Assert.AreEqual(response.Data, fromMethod.Data);

        }
        [Test]
        public void GetListofItemsByCategoryID()
        {
            Response<List<Item>> response = new Response<List<Item>>();

            itemRepo.Insert(ITEM);
            itemRepo.Insert(ITEM1);
            itemRepo.Insert(ITEM2);

            response = itemRepo.GetByCategory(1);
            Assert.AreEqual(3, response.Data.Count);
        }
        [Test]
        public void ItemShouldUpdate()
        {
            Response response = new Response();
            var itemToUpdate = ITEM;
            itemRepo.Insert(itemToUpdate);

            itemToUpdate.ItemName= "Ghost";
            response = itemRepo.Update(itemToUpdate);

            Assert.IsTrue(response.Success);
        }
        [Test]
        public void ShouldDeleteItem()
        {
            Response aResponse = new Response();
            itemRepo.Insert(ITEM);
            itemRepo.Insert(ITEM);
            aResponse = itemRepo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }
        [Test]
        public void GetAllItems()
        {
            Response<List<Item>> response = new Response<List<Item>>();

            itemRepo.Insert(ITEM);
            itemRepo.Insert(ITEM2);
            itemRepo.Insert(ITEM1);

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
