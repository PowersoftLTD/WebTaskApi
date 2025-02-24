using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TaskManagement.API.Model;
using Dapper;
using TaskManagement.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Security.Cryptography;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IEmployeeMst userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthenticationController(IEmployeeMst userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<EMPLOYEE_MST>> Login([FromBody] EMPLOYEE_MST eMPLOYEE_MST)
        {
            var user = await userManager.LoginAsync(eMPLOYEE_MST.LOGIN_NAME.Trim());
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(eMPLOYEE_MST.LOGIN_NAME.Trim() ,eMPLOYEE_MST.LOGIN_PASSWORD.Trim());

                if (checkPasswordResult != null)
                {
                    //create token
                    var jwtToken = await tokenRepository.CreateJWTToken(user);
                    if (IsValid(jwtToken))
                    {
                        var response = new LoginResponse
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return Ok("Session expired");
                    }
                }
            }
            return BadRequest("Username or Password incorrect");
        }

        [HttpPost]
        [Route("Login_NT")]
        public async Task<ActionResult<EMPLOYEE_MST>> Login_NT([FromBody] EMPLOYEE_MST_NT eMPLOYEE_MST)
        {
            var user = await userManager.LoginNTAsync(eMPLOYEE_MST);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordNTAsync(eMPLOYEE_MST);

                if (checkPasswordResult != null)
                {
                    //create token
                    var jwtToken = await tokenRepository.CreateJWTToken(user);
                    if (IsValid(jwtToken))
                    {
                        var response = new LoginResponse_NT
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                    else
                    {
                        return Ok("Session expired");
                    }
                }
            }
            return BadRequest("Username or Password incorrect");
        }


        private bool IsValid(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            try
            {
                jwtSecurityToken = new JwtSecurityToken(token);
            }
            catch (Exception)
            {
                return false;
            }

            return jwtSecurityToken.ValidTo > DateTime.UtcNow;
        }

        [HttpPost]
        public IActionResult GenerateClientCredentials()
        {
            var clientId = Guid.NewGuid().ToString();
            var clientSecret = GenerateClientSecret();

            clientId = clientId.Replace("-", "");
            // Optionally, save clientId and clientSecret to a database or configuration

            return Ok(new { ClientId = clientId, ClientSecret = clientSecret });
        }

        private string GenerateClientSecret()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var secretBytes = new byte[32];
                rng.GetBytes(secretBytes);
                return Convert.ToBase64String(secretBytes);
            }
        }
    }
}
