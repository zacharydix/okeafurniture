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
    public class EFCategoryRepositoryTests
    {
        private ICategoryRepository _repository;
        private OkeaFurnitureContext _context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                .UseInMemoryDatabase(databaseName: "okea").Options;
            _context = new OkeaFurnitureContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _repository = new EFCategoryRepository(_context);
        }

        [Test]
        public void ShouldGetCategory()
        {
            var category = MakeCategoryDesk();
            _context.Category.Add(category);
            _context.SaveChanges();

            var response = _repository.Get(category.CategoryId);

            Assert.IsTrue(response.Success);
            Assert.NotNull(response.Data);
        }

        [Test]
        public void ShouldGetCategoryWithItemsList()
        {
            var category = MakeCategoryOffice();
            var item1 = MakeItemChair();
            var item2 = MakeItemDesk();

            item1.Categories.Add(category);
            item2.Categories.Add(category);

            _context.Category.Add(category);
            _context.Item.Add(item1);
            _context.Item.Add(item2);
            _context.SaveChanges();

            var response = _repository.Get(category.CategoryId);

            Assert.IsTrue(response.Success);
            Assert.NotNull(response.Data);
            Assert.AreEqual(response.Data.Items.Count, 2);
        }

        [Test]
        public void ShouldFindCategoryIfDoesNotExist()
        {
            var response = _repository.Get(99);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.Message, "Category not found. ");
        }

        [Test]
        public void ShouldGetAll()
        {
            var cat1 = MakeCategoryChair();
            var cat2 = MakeCategoryDesk();
            var cat3 = MakeCategoryOffice();

            _context.Category.Add(cat1);
            _context.Category.Add(cat2);
            _context.Category.Add(cat3);
            _context.SaveChanges();

            var response = _repository.GetAll();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.Count, 3);
        }

        [Test]
        public void ShouldAddItemsListToAllCategories()
        {
            var cat1 = MakeCategoryChair();
            var cat2 = MakeCategoryDesk();
            var cat3 = MakeCategoryOffice();

            var item1 = MakeItemChair();
            var item2 = MakeItemDesk();

            item1.Categories.Add(cat1);
            item1.Categories.Add(cat3);
            item2.Categories.Add(cat2);
            item2.Categories.Add(cat3);

            _context.Category.Add(cat1);
            _context.Category.Add(cat2);
            _context.Category.Add(cat3);
            _context.Item.Add(item1);
            _context.Item.Add(item2);
            _context.SaveChanges();

            var response = _repository.GetAll();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.Count, 3);
            Assert.AreEqual(response.Data.Find(c => c.CategoryId == cat1.CategoryId).Items.Count, 1);
            Assert.AreEqual(response.Data.Find(c => c.CategoryId == cat3.CategoryId).Items.Count, 2);
        }

        [Test]
        public void ShouldNotGetCategoriesIfNoneStored()
        {
            var response = _repository.GetAll();

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
            Assert.AreEqual(response.Message, "No categories found. ");
        }

        [Test]
        public void ShouldAddCategory()
        {
            var category = MakeCategoryOffice();

            var response = _repository.Add(category);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.CategoryId, 1);
        }

        //[Test]
        //public void ShouldNotAddDuplicateCategory()
        //{
        //    var cat1 = MakeCategoryDesk();
        //    var cat2 = MakeCategoryDesk();

        //    _context.Categories.Add(cat1);
        //    _context.SaveChanges();

        //    var response = _repository.Add(cat2);

        //    Assert.AreEqual(response.Data.CategoryId, 2);
        //    //where should duplicate category be checked? scan for that name matches?
        //    Assert.IsTrue(false);
        //}

        [Test]
        public void ShouldAlertUserIfInsertFailed()
        {
            var category = MakeCategoryChair();

            _context.Category.Add(category);
            _context.SaveChanges();

            var response = _repository.Add(category);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.Message, "Could not add record. ");
        }

        [Test]
        public void ShouldUpdateCategory()
        {
            var category = MakeCategoryChair();

            _context.Category.Add(category);
            _context.SaveChanges();

            var existingCategory = _repository.Get(category.CategoryId).Data;
            existingCategory.CategoryName = "Ottoman";

            var response = _repository.Update(existingCategory);
            var readResponse = _context.Category.Find(category.CategoryId);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(readResponse.CategoryName, "Ottoman");
        }

        [Test]
        public void ShouldNotUpdateIfCategoryNotFound()
        {
            var category = MakeCategoryDesk();

            var response = _repository.Update(category);
            var readResponse = _repository.Get(category.CategoryId);

            Assert.IsFalse(response.Success);
            Assert.IsFalse(readResponse.Success);
        }

        [Test]
        public void ShouldDelete()
        {
            var category = MakeCategoryOffice();

            _context.Category.Add(category);
            _context.SaveChanges();

            var response = _repository.Delete(category.CategoryId);
            var readResponse = _repository.Get(category.CategoryId);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(readResponse.Success);
        }

        [Test]
        public void ShouldNotDeleteIfNotFound()
        {
            var response = _repository.Delete(88);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(response.Message, "Category not found. ");
        }


        public Category MakeCategoryDesk()
        {
            return new Category()
            {
                CategoryName = "Desk",
            };
        }

        public Category MakeCategoryChair()
        {
            return new Category()
            {
                CategoryName = "Chair",
            };
        }

        public Category MakeCategoryOffice()
        {
            return new Category()
            {
                CategoryName = "Office",
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
    }
}
