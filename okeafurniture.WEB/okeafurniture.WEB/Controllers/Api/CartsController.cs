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

        [Route("get/all", Name ="GetAllCarts")]
        [HttpGet]
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

        [Route("get/{id}", Name ="GetCartById")]
        [HttpGet]
        public IActionResult GetById(int id)
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

        [Route("get/account/{id}", Name ="GetCartByAccountId")]
        [HttpGet]
        public IActionResult GetByAccount(int id)
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

        [Route("get/active/{id}", Name ="GetCartByActive")]
        [HttpGet]
        public IActionResult GetActive(int id)
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

        [Route("get/status/{status}", Name ="GetCartByStatus")]
        [HttpGet]
        public IActionResult GetByStatus(bool CheckedOut)
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

        [Route("add/{id}", Name ="AddCart")]
        [HttpPost]
        public IActionResult Add(CartModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = repo.Add(model.AccountId);

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetById), new { id = result.Data.CartId }, result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Route("update/{id}", Name ="EditCart")]
        [HttpPut]
        public IActionResult Update(CartModel model)
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
