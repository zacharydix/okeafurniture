using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using okeafurniture.CORE.Interfaces;
using okeafurniture.WEB.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
//using System.Web.Extensions;
//using System.Web.Script.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace okeafurniture.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAccountRepository _accountRepo;

        public AuthController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpPost, Route("login")]
        public IActionResult Login(LoginModel login)
        {
            if (login == null)
            {
                return BadRequest(JObject.FromObject(new { message = "Invalid Request" }));
            }

            var account = _accountRepo.GetByEmail(login.Email).Data;
            
            if (account == null)
            {
                //return BadRequest("Invalid Email");
                //return BadRequest(new JavaScriptSerializer.Serialize(new { message = "Invalid Email" }));
                return BadRequest(JObject.FromObject(new { message = "Invalid Email" }));
            }

            if (account.Password != login.Password)
            {
                return BadRequest(JObject.FromObject(new { message = "Invalid Password" }));
            }

            else if (account.Password == login.Password)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:2000",
                    audience: "http://localhost:2000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString, IsAdmin = account.IsAdmin });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
