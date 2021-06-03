using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using okeafurniture.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace okeafurniture.WEB.Controllers.Api
{
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
                return BadRequest("Invalid request");
            }

            //get account by Email
            var account = _accountRepo.GetByEmail(login.Email);

            //;
            //var account = _accountRepo.GetAll().SingleOrDefault(a => a.Email == login.Email);

            //if no user found, say "That email is not registered"
            if (account == null)
            {
                return BadRequest("Invalid email");
            }

            //check that password on that account matches record
            //if password doesn't match, say "Invalid password"
            if (account.Password != login.Password)
            {
                return BadRequest("Invalid password");
            }

            //else, execute the below codeblock
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
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
