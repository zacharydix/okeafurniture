using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.DTOs;
using okeafurniture.CORE.Interfaces;
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
    }
}
