using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private IItemRepository repository;
        public IActionResult Index()
        {
            return View();
        }
    }
}
