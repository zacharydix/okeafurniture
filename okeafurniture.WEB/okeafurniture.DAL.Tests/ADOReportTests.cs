using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using okeafurniture.DAL;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using okeafurniture.CORE.Entites;

namespace okeafurniture.DAL.Tests
{
    public class AdoReportTests
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
        public void ShouldReturnTopItems()
        {
            var result = repo.GetTopItems();
            Assert.IsTrue(result.Success);
            switch(result.Data.Count)
            {
                case 3:
                    Assert.IsTrue(result.Data[0].Revenue >= result.Data[1].Revenue && result.Data[1].Revenue >= result.Data[2].Revenue);
                    break;
                case 2:
                    Assert.IsTrue(result.Data[0].Revenue >= result.Data[1].Revenue);
                    break;
            }
        }

        [Test]
        public void ShouldReturnTopCustomers()
        {
            var result = repo.GetTopCustomers();
            Assert.IsTrue(result.Success);
            switch (result.Data.Count)
            {
                case 3:
                    Assert.IsTrue(result.Data[0].TotalSpent >= result.Data[1].TotalSpent && result.Data[1].TotalSpent >= result.Data[2].TotalSpent);
                    break;
                case 2:
                    Assert.IsTrue(result.Data[0].TotalSpent >= result.Data[1].TotalSpent);
                    break;
            }
        }

        [Test]
        public void ShouldReturnRevenueReport()
        {
            var StartDate = DateTime.Parse("2021-05-01");
            var EndDate = DateTime.Parse("2021-06-30");
            var result = repo.GetRevenueReport(StartDate, EndDate);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.All(r => r.SaleDate >= StartDate && r.SaleDate <= EndDate));
        }
    }
}
