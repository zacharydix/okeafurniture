using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using okeafurniture.DAL.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.DAL.Tests
{
    public class EFItemCategoryRepositoryTests
    {
        private IItemCategoryRepository _repository;
        private OkeaFurnitureContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                .UseInMemoryDatabase(databaseName: "okea").Options;
            _context = new OkeaFurnitureContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _repository = new EFItemCategoryRepository(_context);
        }

        [Test]
        public void ShouldGetItemCategory()
        {
            var item1 = MakeItemChair();
            var cat1 = MakeCategoryChair();

            _context.Items.Add(item1);
            _context.Categories.Add(cat1);
            _context.SaveChanges();

            var itemCategory = MakeItemCategory(item1.ItemId, cat1.CategoryId);
            _context.ItemCategories.Add(itemCategory);
            _context.SaveChanges();

            var response = _repository.Get(item1.ItemId, cat1.CategoryId);


            Assert.IsTrue(response.Success);
            Assert.NotNull(response.Data);
        }

        [Test]
        public void ShouldNotFindItemCategoryIfNonexistant()
        {
            var response = _repository.Get(5, 5);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.AreEqual(response.Message, "ItemCategory not found. ");
        }

        [Test]
        public void ShouldGetAllByCategory()
        {
            var item1 = MakeItemChair();
            var item2 = MakeItemDesk();
            var cat1 = MakeCategoryOffice();

            _context.Items.Add(item1);
            _context.Items.Add(item2);
            _context.Categories.Add(cat1);
            _context.SaveChanges();

            var itemCategory1 = MakeItemCategory(item1.ItemId, cat1.CategoryId);
            var itemCategory2 = MakeItemCategory(item2.ItemId, cat1.CategoryId);
            _context.ItemCategories.Add(itemCategory1);
            _context.ItemCategories.Add(itemCategory2);
            _context.SaveChanges();

            var response = _repository.GetAllByCategory(cat1.CategoryId);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.Count, 2);
        }

        [Test]
        public void ShouldNotFindByCategoryIfNoneFound()
        {
            var response = _repository.GetAllByCategory(9);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.AreEqual(response.Message, "No ItemCategories found for that category. ");
        }

        [Test]
        public void ShouldGetAllByItem()
        {
            var item1 = MakeItemDesk();
            var cat1 = MakeCategoryDesk();
            var cat2 = MakeCategoryOffice();

            _context.Items.Add(item1);
            _context.Categories.Add(cat1);
            _context.SaveChanges();
        }



        public ItemCategory MakeItemCategory(int itemId, int categoryId)
        {
            return new ItemCategory()
            {
                ItemId = itemId,
                CategoryId = categoryId
            };
        }

        public Item MakeItemChair()
        {
            return new Item()
            {
                ItemName = "Metal Desk Chair",
                ItemDescription = "Cushion not included",
                UnitPrice = 50M
            };
        }

        public Item MakeItemDesk()
        {
            return new Item()
            {
                ItemName = "Wooden Desk",
                ItemDescription = "Sturdy and simple",
                UnitPrice = 100M
            };
        }

        public Category MakeCategoryChair()
        {
            return new Category()
            {
                CategoryName = "Chair"
            };
        }

        public Category MakeCategoryDesk()
        {
            return new Category()
            {
                CategoryName = "Desk"
            };
        }

        public Category MakeCategoryOffice()
        {
            return new Category()
            {
                CategoryName = "Office"
            };
        }
    }
}
