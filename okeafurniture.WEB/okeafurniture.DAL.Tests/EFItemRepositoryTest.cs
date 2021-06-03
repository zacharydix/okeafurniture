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
    public class ItemRepository
    {
        private OkeaFurnitureContext db;
        private IItemRepository itemRepo;

        public readonly static Item ITEM = MakeItem();
        public readonly static Item ITEM1 = MakeItem1();
        public readonly static Item ITEM2 = MakeItem2();


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase(databaseName: "okea").Options;
            db = new OkeaFurnitureContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.SaveChanges();

            itemRepo = new EFItemRepository(db);
        }

        [Test]
        public void InsertingItem()
        {
            itemRepo.Insert(ITEM);

            var response = db.Items.Find(1);
            Assert.AreEqual(response.ItemName, ITEM.ItemName);
            Assert.AreEqual(response.CategoryId, 1);
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
                CategoryId = 1,
                ItemDescription = "bigCouch",
                UnitPrice =200.00M
            };
            return item;
        }
        public static Item MakeItem1()
        {
            Item item = new Item()
            {

                ItemName = "SuperCouch",
                CategoryId = 1,
                ItemDescription = "biggerCouch",
                UnitPrice = 999.00M
            };
            return item;
        }
        public static Item MakeItem2()
        {
            Item item = new Item()
            {

                ItemName = "UltraMegaCouch",
                CategoryId = 1,
                ItemDescription = "BiggerBetterBestest",
                UnitPrice = 10000.00M
            };
            return item;
        }
    }
}