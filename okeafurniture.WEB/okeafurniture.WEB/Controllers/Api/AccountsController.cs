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
                return Ok(response.Data);
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
                return Ok(response.Data);
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
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost, Route("add")]
        public IActionResult AddAccount(Account account)
        {
            Response<Account> response = repository.Add(account);
            if (response.Success)
            {
                return CreatedAtRoute(nameof(GetAccountById), new { id = account.AccountId }, account);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut, Route("edit")]
        public IActionResult EditAccount(Account account)
        {
            Response<Account> response = repository.Get(account.AccountId);
            if (!response.Success)
            {
                return NotFound($"Account {account.AccountId} not found");
            }
            Response updateResponse = repository.Update(account);
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
