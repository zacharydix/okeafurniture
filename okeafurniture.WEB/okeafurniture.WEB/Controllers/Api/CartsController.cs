using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CartsController : ControllerBase
    {
        private ICartRepository repo;

        public CartsController(ICartRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet, Route("get/all", Name = "GetAllCarts"), Authorize]
        public IActionResult GetAllCarts()
        {
            var result = repo.GetAll();

            if (result.Success)
            {
                return Ok(result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }        
        
        [HttpGet, Route("get/{id}", Name = "GetCartById"), Authorize]
        public IActionResult GetCartById(int id)
        {
            var result = repo.Get(id);

            if (result.Success)
            {
                return Ok(result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        
        [HttpGet, Route("get/account/{id}", Name ="GetCartByAccountId"), Authorize]
        public IActionResult GetCartByAccount(int id)
        {
            var result = repo.GetAllByAccount(id);

            if (result.Success)
            {
                return Ok(result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        
        [HttpGet, Route("get/active/{id}", Name ="GetCartByActive"), Authorize]
        public IActionResult GetActiveCarts(int id)
        {
            var result = repo.GetActive(id);

            if (result.Success)
            {
                return Ok(result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet, Route("get/status/{status}", Name ="GetCartByStatus"), Authorize]
        public IActionResult GetCartsByStatus(bool CheckedOut)
        {
            var result = repo.GetAllByStatus(CheckedOut);

            if (result.Success)
            {
                return Ok(result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost, Route("add", Name ="AddCart"), Authorize]
        public IActionResult AddCart(CartModel model)
        {
            if(repo.GetActive(model.AccountId).Success)
            {
                return BadRequest(new { Message = "Customer cannot have more than one active cart." });
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = repo.Add(model.MapToCart());

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetCartById), new { id = result.Data.CartId }, result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut, Route("update", Name ="EditCart"), Authorize]
        public IActionResult UpdateCart(CartModel model)
        {
            var result = repo.Get(model.CartId);
            if (!result.Success)
            {
                return NotFound($"Cart {model.CartId} not found");
            }

            if (ModelState.IsValid)
            {
                var updatedResult = repo.Update(model.MapToCart());

                if (updatedResult.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
