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
    public class PaymentMethodsController : ControllerBase
    {
        private IPaymentMethodRepository repository;

        public PaymentMethodsController(IPaymentMethodRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("get/id/{id}", Name ="GetPaymentMethodById"), Authorize]
        public IActionResult GetPaymentMethodById(int id)
        {
            Response<PaymentMethod> response = repository.Get(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet, Route("get/account/{accountId}", Name ="GetPaymentMethodByAccount"), Authorize]
        public IActionResult GetPaymentMethodByAccount(int accountId)
        {
            Response<List<PaymentMethod>> response = repository.GetByUser(accountId);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost, Route("add", Name ="AddPaymentMethod"), Authorize]
        public IActionResult AddPaymentMethod(PaymentMethodModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Response<PaymentMethod> response = repository.Add(model.MapToPaymentMethod());
            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetPaymentMethodById), new { id = response.Data.PaymentMethodId }, response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Route("edit", Name ="EditPaymentMethod"), Authorize]
        public IActionResult EditPaymentMethod(PaymentMethodModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Response<PaymentMethod> response = repository.Get(model.PaymentMethodId);
            if (!response.Success)
            {
                return NotFound($"Payment method {model.PaymentMethodId} not found");
            }
            Response updateResponse = repository.Update(model.MapToPaymentMethod());
            if (updateResponse.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(updateResponse.Message);
            }
        }

        [HttpDelete, Route("delete/{id}", Name ="DeletePaymentMethod"), Authorize]
        public IActionResult DeletePaymentMethod(int id)
        {
            Response<PaymentMethod> response = repository.Get(id);
            if (!response.Success)
            {
                return NotFound($"Payment method {id} not found");
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
