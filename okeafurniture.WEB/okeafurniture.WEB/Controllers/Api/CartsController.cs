using Microsoft.AspNetCore.Http;
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
    public class CartsController : ControllerBase
    {
        private ICartRepository repo;

        public CartsController(ICartRepository repo)
        {
            this.repo = repo;
        }

        [Route("/get/all", Name = "GetAll")]
        [HttpGet]
        public IActionResult GetAll()
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

        [Route("/get/{id}", Name = "GetById")]
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

        [Route("/getbyaccount/{id}", Name = "GetByAccount")]
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

        [Route("/getactive/{id}", Name = "GetActive")]
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

        [Route("/getbystatus/{status}", Name = "GetByStatus")]
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

        [Route("/add/{id}", Name = "Add")]
        [HttpPost]
        public IActionResult Add(CartModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = repo.Add(model.MapToCart());

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetById), new { id = result.Data.CartId }, result.Data.MapToModel());
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Route("/update/{id}", Name = "Update")]
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
