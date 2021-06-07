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
    public class CartItemsController : Controller
    {
        private ICartItemRepository repository;

        public CartItemsController(ICartItemRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("get/cartid/{cartId}/itemid/{itemId}", Name="GetCartItemById"), Authorize]
        public IActionResult GetCartItemById(int cartId, int itemId)
        {
            Response<CartItem> result = repository.Get(cartId, itemId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet, Route("get/all/cartid/{cartid}", Name ="GetAllCartItemsById"), Authorize]
        public IActionResult GetCartItemsByCartId(int cartId)
        {
            Response<List<CartItem>> response = repository.GetByCart(cartId);
            if (response.Success)
            {
                return Ok(response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost, Route("add", Name ="AddCartItem"), Authorize]
        public IActionResult AddCartItem(CartItemModel model)
        {
            Response<CartItem> response = repository.Add(model.MapToCartItem());
            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetCartItemById), new { cartId = response.Data.CartId, itemId = response.Data.ItemId }, response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Route("edit", Name ="EditCartItem"), Authorize]
        public IActionResult EditCartItem(CartItemModel model)
        {
            Response<CartItem> response = repository.Get(model.CartId, model.ItemId);
            if (!response.Success)
            {
                return NotFound($"Cart Item not found");
            }
            Response updateResponse = repository.Update(model.MapToCartItem());
            if (updateResponse.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(updateResponse.Message);
            }
        }

        [HttpDelete, Route("delete/cartid/{cartId}/itemid/{itemId}", Name ="DeleteCartItem"), Authorize]
        public IActionResult DeleteCartItem(int cartId, int itemId)
        {
            Response<CartItem> response = repository.Get(cartId, itemId);
            if (!response.Success)
            {
                return NotFound($"Cart Item not found");
            }
            Response deleteResponse = repository.Delete(cartId, itemId);
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
