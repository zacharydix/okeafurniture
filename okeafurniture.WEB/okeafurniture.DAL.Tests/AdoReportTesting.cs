using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.DAL;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace okeafurniture.DAL.Tests
{
    public class AdoReportTesting
    {
        private OkeaFurnitureContext db;
        private AdoReportRepository repo;

        [SetUp]
        
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OkeaFurnitureContext>()
                    .UseInMemoryDatabase(databaseName: "okea").Options;
            db = new OkeaFurnitureContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.SaveChanges();
            repo = new AdoReportRepository(OkeaFurnitureContext.GetConnectionString());
        }
        [Test]
        public void ShouldReturnItemList()
        {
            var result = repo.GetTopItems();
            Assert.AreEqual(2, result.Data.Count);
        }
    }
}
