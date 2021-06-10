using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.Entities;
using okeafurniture.CORE.Interfaces;
using okeafurniture.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryItemsController : Controller
    {
        private ICategoryItemRepository repository;

        public CategoryItemsController(ICategoryItemRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("get/category/{categoryId}/itemid/{itemId}", Name= "GetCategoryItemById"), Authorize]
        public IActionResult GetCategoryItemById(int categoryId, int itemId)
        {
            Response<CategoryItem> result = repository.Get(categoryId, itemId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost, Route("add", Name = "AddCategoryItem"), Authorize]
        public IActionResult AddCategoryItem(CategoryItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Response<CategoryItem> result = repository.Add(model.MapToCategoryItem());
            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetCategoryItemById), new { categoryId = result.Data.CategoryId, itemId = result.Data.ItemId }, result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
