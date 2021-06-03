﻿using Microsoft.AspNetCore.Mvc;
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
    public class AccountsController : ControllerBase
    {
        private IAccountRepository repository;

        public AccountsController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("get/all")]
        public IActionResult GetAccounts()
        {
            Response<List<Account>> response = repository.GetAll();
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
        public IActionResult GetAccountById(int id)
        {
            Response<Account> response = repository.Get(id);
            if (response.Success)
            {
                return Ok(response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpGet, Route("get/email")]
        public IActionResult GetAccountByEmail(string email)
        {
            Response<Account> response = repository.GetByEmail(email);
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
        public IActionResult AddAccount(Account model)
        {
            Response<Account> response = repository.Add(model.MapToAccount());
            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetAccountById), new { id = response.Data.AccountId }, response.Data.MapToModel());
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Route("edit")]
        public IActionResult EditAccount(Account model)
        {
            Response<Account> response = repository.Get(model.AccountId);
            if (!response.Success)
            {
                return NotFound($"Account {model.AccountId} not found");
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
        public IActionResult DeleteAccount(int id)
        {
            Response<Account> response = repository.Get(id);
            if (!response.Success)
            {
                return NotFound($"Account {id} not found");
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
