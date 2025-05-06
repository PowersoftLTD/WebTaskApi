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
        private readonly IProjectEmployee _repository;
        public AuthenticationController(IProjectEmployee repository, IEmployeeMst userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            _repository = repository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<EMPLOYEE_MST>> Login([FromBody] EMPLOYEE_MST eMPLOYEE_MST)
        {

            try
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(eMPLOYEE_MST.LOGIN_NAME.Trim(), eMPLOYEE_MST.LOGIN_PASSWORD.Trim());

                if (checkPasswordResult != null)
                {
                    //create token
                    var jwtToken = await tokenRepository.CreateJWTToken(eMPLOYEE_MST.LOGIN_NAME.Trim());
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
                    return BadRequest("Username or Password incorrect");
                }
                else
                {
                    return BadRequest("Username or Password incorrect");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        [Route("Login_NT")]
        public async Task<ActionResult<EmployeeLoginOutput_LIST_NT>> Login_NT([FromBody] EmployeeCompanyMSTInput_NT employeeCompanyMSTInput_NT)
        {
            try
            {
                var LoginValidate = await _repository.Login_Validate_NT(employeeCompanyMSTInput_NT);
                return Ok(LoginValidate);
            }
            catch (Exception ex)
            {
                var response = new EmployeeLoginOutput_LIST
                {
                    Status = "Error",
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost]
        [Route("LoginMobileEmailCheck_NT")]
        public async Task<ActionResult<EmployeeMobile_NT>> LoginMobile_NT([FromBody] EmployeeMobileMSTInput_NT employeeCompanyMSTInput_NT)
        {
            try
            {
                var LoginValidate = await _repository.Login_Mobile_Validate_NT(employeeCompanyMSTInput_NT);
                return Ok(LoginValidate);
            }
            catch (Exception ex)
            {
                var response = new EmployeeMobile_NT
                {
                    Status = "Error",
                    Message = ex.Message
                };
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("LoginMobileEmail_NT")]
        public async Task<ActionResult<LoginMobileEmail_NT>> LoginMobileEmail_NT([FromBody] EmployeeMobileMSTInput_NT employeeCompanyMSTInput_NT)
        {
            try
            {
                var LoginValidate = await _repository.LoginMobileEmailNTAsync(employeeCompanyMSTInput_NT);
                return Ok(LoginValidate);
            }
            catch (Exception ex)
            {
                var response = new LoginMobileEmail_NT
                {
                    Status = "Error",
                    Message = ex.Message
                };
                return Ok(response);
            }
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
