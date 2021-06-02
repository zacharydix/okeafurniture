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
                .UseInMemoryDatabase(databaseName: "FieldAgent").Options;
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
            var response = _repository.Add(itemCategory);

            Assert.IsTrue(response.Success);
            //Assert.AreEqual(response.Data.Count, 1);
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
