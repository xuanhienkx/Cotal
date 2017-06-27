using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Models.AccountViewModels;
using Cotal.Core.Identity.Services;
using Cotal.WebAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.WebAPI.Controllers
{
    [Route("api/[controller]")] 
    public class AccountController : Controller
    {
         
        private readonly ILogger _logger;
        private IUserService _userService;

        public AccountController(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel mode)
        {
            try
            {
                var user = await _userService.GetUserByUsername(mode.UserName);
                if (user != null)
                {
                    if (_userService.VerifyHashedPassword(user, mode.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _userService.GetClaims(user);

                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.GivenName, user.FullName),
                            new Claim(JwtRegisteredClaimNames.FamilyName, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        }.Union(userClaims);                                                           

                        var token = new JwtSecurityToken(
                            issuer: "http://localhost:5838",
                            audience: "http://localhost:5838",
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: TokenAuthOption.SigningCredentials
                        );
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return Ok(results);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to login");
        }
        /* [HttpPost]         
         public string GetAuthToken(LoginViewModel model)
         {
            // var existUser = UserStorage.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
             var result =   _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false).Result;
             if (result.Succeeded)
             {
                 AppUser user;
                 user =   _userManager.FindByNameAsync(model.UserName).Result;
                 var requestAt = DateTime.Now;
                 var expiresIn = requestAt + TokenAuthOption.Expiration;
                 var token = GenerateToken(user, expiresIn);

                 return JsonConvert.SerializeObject(new RequestResult
                 {
                     State = RequestState.Success,
                     Data = new
                     {
                         requertAt = requestAt,
                         expiresIn = TokenAuthOption.Expiration.TotalSeconds,
                         tokeyType = TokenAuthOption.TokenType,
                         accessToken = token
                     }
                 });
             }
             else
             {
                 return JsonConvert.SerializeObject(new RequestResult
                 {
                     State = RequestState.Failed,
                     Msg = "Username or password is invalid"
                 });
             }
         }*/
        private string GenerateToken(AppUser user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();
           
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                new[] {
                    new Claim("Id", user.Id.ToString())
                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
            return handler.WriteToken(securityToken);
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
