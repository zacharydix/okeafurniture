using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.Interfaces;
using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using okeafurniture.WEB.Models;

namespace okeafurniture.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IItemRepository _repo;

        public ItemsController(IItemRepository repo)
        {
            _repo = repo;
        }

        [HttpGet, Route("get/{id}", Name = "GetItem"), Authorize]
        public IActionResult GetItem(int id)
        {
            var result = _repo.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet, Route("get/all", Name = "GetAllItems"), Authorize]
        public IActionResult GetAllItems()
        {
            var result = _repo.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet, Route("get/all/categories/{id}", Name = "GetItemsByCategory"), Authorize]
        public IActionResult GetItemsByCategories(int id)
        {
            var result = _repo.GetByCategory(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost, Route("add", Name = "AddItem"), Authorize]
        public IActionResult AddItem(ItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingItems = _repo.GetAll().Data;
            if (existingItems.Any(i => i.ItemName == model.ItemName))
            {
                return BadRequest(new { Message = "That item already exists in the catalog." });
            }

            var result = _repo.Insert(model.MapToItem());

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetItem), new { id = result.Data.ItemId }, result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut, Route("edit", Name = "EditItem"), Authorize]
        public IActionResult EditItem(ItemModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Response<Item> response = _repo.Get(model.ItemId);
            if (!response.Success)
            {
                return NotFound($"Item {model.ItemId} not found");
            }

            var itemToUpdate = response.Data;

            itemToUpdate.ItemName = model.ItemName;
            itemToUpdate.ItemDescription = model.ItemDescription;
            itemToUpdate.UnitPrice = model.UnitPrice;
            itemToUpdate.ImageName = model.ImageName;

            Response updateResponse = _repo.Update(itemToUpdate);
            if (updateResponse.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(updateResponse.Message);
            }
        }

        [HttpDelete("delete/{id}", Name = "DeleteItem"), Authorize]
        public IActionResult DeleteItem(int id)
        {
            if (!_repo.Get(id).Success)
            {
                return NotFound($"Alias {id} not found");
            }

            var result = _repo.Delete(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
