﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet, Route("get/all", Name ="GetAllCategories")]
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

        [HttpGet, Route("get/id/{id}", Name ="GetCategoryById")]
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

        [HttpPost, Route("add", Name ="AddCategory")]
        public IActionResult AddCategory(CategoryModel model)
        {
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

        [HttpPut, Route("edit", Name ="EditCategory")]
        public IActionResult EditCategory(CategoryModel model)
        {
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

        [HttpDelete, Route("delete/{id}", Name ="DeleteCategory")]
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
