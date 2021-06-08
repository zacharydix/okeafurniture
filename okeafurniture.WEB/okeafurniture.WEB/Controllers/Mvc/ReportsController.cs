using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.DTOs;
using okeafurniture.CORE.Interfaces;
using okeafurniture.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Controllers.Mvc
{
    public class ReportsController : Controller
    {
        private IReportRepository _repo;
        public ReportsController(IReportRepository repo)
        {
            _repo = repo;
        }
        [Route("Reports")]
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }

        [Route("Reports/TopItems")]
        [HttpGet]
        public IActionResult TopItems()
        {
            var data = _repo.GetTopItems();
            return View(data.Data);
        }

        [Route("Reports/TopCustomers")]
        public IActionResult TopCustomers()
        {
            var data = _repo.GetTopCustomers();
            return View(data.Data);
        }

        [Route("Reports/GetRevenueDateRange")]
        [HttpGet]
        public IActionResult GetRevenueDateRange()
        {
            var model = new ParameterModel();
            return View();
        }

        [Route("Reports/RevenueReport")]
        [HttpPost]
        public IActionResult RevenueReport(ParameterModel param)
        {
            var result = _repo.GetRevenueReport(param.StartDate, param.EndDate);
            var revenueReport = new RevenueReportModel()
            {
                StartDate = param.StartDate,
                EndDate = param.EndDate,
                ListItems = result.Data,
                TotalRevenue = result.Data.Sum(r => r.RevenueGenerated)
            };
            return View(revenueReport);
        }  
    }
}
