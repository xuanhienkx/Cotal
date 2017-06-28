   

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Models.AccountViewModels;
using Cotal.Core.Identity.Services;
using Cotal.WebAPIClient.Auth;
using Cotal_WebAPIClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Cotal.WebAPIClient.Controllers
{
    [Route("api/[controller]")]
    public class TokenAuthController : Controller
    {
        private IUserService _userService;

        public TokenAuthController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<string> GetAuthToken([FromBody]LoginViewModel model)
        {                 
            var signInResult = await _userService.GetUserByUsername(model.UserName);   
            if (signInResult!= null)
            {
                var verify =  _userService.VerifyHashedPassword(signInResult, model.Password);
                if (verify == PasswordVerificationResult.Success)
                {
                    var requestAt = DateTime.Now;
                    var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                    var token = GenerateToken(signInResult, expiresIn);

                    return   JsonConvert.SerializeObject(new RequestResult
                    {
                        State = RequestState.Success,
                        Data = new
                        {
                            requertAt = requestAt,
                            expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
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
               
            }
            else
            {
                return JsonConvert.SerializeObject(new RequestResult
                {
                    State = RequestState.Failed,
                    Msg = "Username or password is invalid"
                });
            }
        }

        private string GenerateToken(AppUser user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                new[] {
                    new Claim("ID", user.Id.ToString())
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

        [HttpGet]
        [Authorize("Bearer")]
        public string GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return JsonConvert.SerializeObject(new RequestResult
            {
                State = RequestState.Success,
                Data = new
                {
                    UserName = claimsIdentity.Name
                }
            });
        }

    }
}
