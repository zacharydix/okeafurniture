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
using System.Threading.Tasks;

namespace okeafurniture.WEB.Controllers.Mvc
{
    public class AdminAuthController : Controller
    {
        private IAccountRepository _accountRepo;

        public AdminAuthController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpGet, Route("AdminAuth/login")]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost, Route("AdminAuth/login")]
        public IActionResult Login(LoginModel login)
        {
            if (login == null)
            {
                return View(); //BadRequest("Invalid request");
            }

            //get account by Email
            var account = _accountRepo.GetByEmail(login.Email).Data;

            //if no user found, say "That email is not registered"
            if (account == null)
            {
                return View(); // BadRequest("Invalid email");
            }

            //check that password on that account matches record
            //if password doesn't match, say "Invalid password"
            if (account.Password != login.Password)
            {
                return View(); // BadRequest("Invalid password");
            }

            if (!account.IsAdmin)
            {
                return View(); // BadRequest("You do not have access to this page");
            }
            //else, execute the below codeblock
            else if (account.Password == login.Password && account.IsAdmin)
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
                //return Ok(new { Token = tokenString });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(); // Authorized
            }
        }
    }
}
