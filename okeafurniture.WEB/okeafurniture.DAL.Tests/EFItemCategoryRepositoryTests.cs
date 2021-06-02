using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
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
        IItemCategoryRepository repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                .UseInMemoryDatabase(databaseName: "FieldAgent").Options;
            var context = new OkeaFurnitureContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repository = new EFItemCategoryRepository(context);
        }

        [Test]
        public void ShouldGetItemCategory()
        {

        }

        public 
    }
}
