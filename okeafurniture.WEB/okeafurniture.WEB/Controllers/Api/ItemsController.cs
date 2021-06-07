using Microsoft.AspNetCore.Mvc;
using okeafurniture.CORE.Interfaces;
using okeafurniture.CORE.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        [Route("get/{id}", Name ="GetItemById")]
        public IActionResult GetItem(int id)
        {
            var result = _repo.Get(id);
            
            if(result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet]
        [Route("get/all", Name="GetAllItems")]
        public IActionResult GetAllItems()
        {
            var result = _repo.GetAll();

            if(result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet]
        [Route("get/all/categories/{id}", Name ="GetItemsByCategory")]

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
        [HttpPost]
        [Route("add", Name ="AddItem")]

        public IActionResult AddItem(Item item)
        {
            if (ModelState.IsValid)
            {
                var result = _repo.Insert(item);
                
                if(result.Success)
                {
                    return CreatedAtRoute(nameof(GetItem), new { id = item.ItemId }, item);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut, Route("edit", Name ="EditItem")]
        public IActionResult EditItem(Item item)
        {
            Item existingitem = _repo.Get(item.ItemId).Data;

            existingitem.ItemName = item.ItemName;
            existingitem.ItemDescription = item.ItemDescription;
            existingitem.UnitPrice = item.UnitPrice;

            if(ModelState.IsValid)
            {
                var result = _repo.Update(existingitem);

                if(result.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("delete/{id}", Name ="DeleteItem")]
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
