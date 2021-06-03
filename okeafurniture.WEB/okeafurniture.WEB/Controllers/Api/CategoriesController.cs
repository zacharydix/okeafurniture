using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.Entites;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository repository;
        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("get/all")]
        public IActionResult GetCategories()
        {
            Response<List<Category>> response = repository.GetAll();
            if (response.Success)
            {
                return Ok(response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet, Route("get/id")]
        public IActionResult GetCategoryById(int id)
        {
            Response<Category> response = repository.Get(id);
            if (response.Success)
            {
                return Ok(response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost, Route("add")]
        public IActionResult AddCategory(Category model)
        {
            Response<Category> response = repository.Add(model.MapToAccount());
            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetCategoryById), new { id = response.Data.CategoryId }, response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Route("edit")]
        public IActionResult EditCategory(Category model)
        {
            Response<Category> response = repository.Get(model.CategoryId);
            if (!response.Success)
            {
                return NotFound($"Category {model.CategoryId} not found");
            }
            Response updateResponse = repository.Update(model.MapToAccount());
            if (updateResponse.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(updateResponse.Message);
            }
        }

        [HttpDelete, Route("delete")]
        public IActionResult DeleteCategory(int id)
        {
            Response<Category> response = repository.Get(id);
            if (!response.Success)
            {
                return NotFound($"Category {id} not found");
            }
            Response deleteResponse = repository.Delete(id);
            if (deleteResponse.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(deleteResponse.Message);
            }
        }
    }
}
