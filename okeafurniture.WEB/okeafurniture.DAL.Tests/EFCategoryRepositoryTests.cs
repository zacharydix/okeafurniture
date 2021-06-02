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
            _context.Categories.Add(category);
            _context.SaveChanges();

            var response = _repository.Get(category.CategoryId);

            Assert.IsTrue(response.Success);
            Assert.NotNull(response.Data);
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

            _context.Categories.Add(cat1);
            _context.Categories.Add(cat2);
            _context.Categories.Add(cat3);
            _context.SaveChanges();

            var response = _repository.GetAll();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Data.Count, 3);
        }



        public Category MakeCategoryDesk()
        {
            return new Category()
            {
                CategoryName = "Desk"
            };
        }

        public Category MakeCategoryChair()
        {
            return new Category()
            {
                CategoryName = "Chair"
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
