using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.Entites;
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
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository repository;
        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("get/all", Name ="GetAllCategories"), Authorize]
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

        [HttpGet, Route("get/id/{id}", Name ="GetCategoryById"), Authorize]
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

        [HttpPost, Route("add", Name ="AddCategory"), Authorize]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategories = repository.GetAll().Data;
            if (existingCategories.Any(c => c.CategoryName == model.CategoryName))
            {
                return BadRequest(new { Message = "That category already exists in the catalog." });
            }

            Response<Category> response = repository.Add(model.MapToCategory());
            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetCategoryById), new { id = response.Data.CategoryId }, response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Route("edit", Name ="EditCategory"), Authorize]
        public IActionResult EditCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Response<Category> response = repository.Get(model.CategoryId);
            if (!response.Success)
            {
                return NotFound($"Category {model.CategoryId} not found");
            }
            Response updateResponse = repository.Update(model.MapToCategory());
            if (updateResponse.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(updateResponse.Message);
            }
        }

        [HttpDelete, Route("delete/{id}", Name ="DeleteCategory"), Authorize]
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
