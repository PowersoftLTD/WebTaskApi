using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;
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
        private readonly IHttpClientFactory _httpClientFactory;    // Added By Itemad Hyder 27-10-2025
        public AuthenticationController(IProjectEmployee repository, IEmployeeMst userManager, ITokenRepository tokenRepository, IHttpClientFactory httpClientFactory)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            _repository = repository;
            _httpClientFactory = httpClientFactory;
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
                //// To get The IpAddress Added By Itemad Hyder 27-10-2025
                var ipAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();

                if (ipAddress == "::1")
                    ipAddress = "127.0.0.1";

                var client = _httpClientFactory.CreateClient();
                var publicIpResponse = await client.GetStringAsync("https://api.ipify.org");
                var publicIp = publicIpResponse.Trim();
                var response = await client.GetAsync($"https://ipinfo.io/{publicIp}/json");
                if (!response.IsSuccessStatusCode)
                {
                    var responses = new EmployeeLoginOutput_LIST
                    {
                        Status = "Error",
                        Message = "Unable to get location.",
                        Data = null
                    };
                    return Ok(response);
                }
                //return BadRequest("Unable to get location.");

                var json = await response.Content.ReadAsStringAsync();
                var locationData = JsonConvert.DeserializeObject<UserLocationInfo>(json);
                if (locationData != null)
                {
                    locationData.CREATED_BY = employeeCompanyMSTInput_NT.Login_ID;
                    var userLocationStr = await _repository.InsertUserLocationAsync(locationData);
                    var userAuditModel = new User_Audit
                    {
                        User_Id = locationData.CREATED_BY,
                        User_IP = locationData.Ip,
                        User_Location = locationData.Loc,
                        Activity = "Login Activity",
                        ATTRIBUTE1 = locationData.Hostname,
                        ATTRIBUTE2 = locationData.City,
                        ATTRIBUTE3 = locationData.Region,
                        ATTRIBUTE4 = locationData.Country,
                        ATTRIBUTE5 = locationData.Org,
                        ATTRIBUTE6 = locationData.Postal,
                        ATTRIBUTE7 = locationData.Timezone,
                        ATTRIBUTE8 = locationData.Readme,
                        CREATED_BY = locationData.CREATED_BY,
                        CREATION_DATE = locationData.CREATION_DATE,
                        LAST_UPDATED_BY = locationData.LAST_UPDATED_BY,
                        LAST_UPDATE_DATE = locationData.LAST_UPDATE_DATE,
                        DELETE_FLAG = locationData.DELETE_FLAG


                    };
                    var userAudit = await _repository.InsertUserAuditAsync(userAuditModel);
                }
                //// END IP Address by Itemad Hyder 27-10-2025
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
                //// To get The IpAddress Added By Itemad Hyder 27-10-2025
                var ipAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();

                if (ipAddress == "::1")
                    ipAddress = "127.0.0.1";

                var client = _httpClientFactory.CreateClient();
                var publicIpResponse = await client.GetStringAsync("https://api.ipify.org");
                var publicIp = publicIpResponse.Trim();
                var response = await client.GetAsync($"https://ipinfo.io/{publicIp}/json");
                if (!response.IsSuccessStatusCode)
                {
                    var responses = new EmployeeLoginOutput_LIST
                    {
                        Status = "Error",
                        Message = "Unable to get location.",
                        Data = null
                    };
                    return Ok(response);
                }
                //return BadRequest("Unable to get location.");

                var json = await response.Content.ReadAsStringAsync();
                var locationData = JsonConvert.DeserializeObject<UserLocationInfo>(json);
                if (locationData != null)
                {
                    locationData.CREATED_BY = employeeCompanyMSTInput_NT.Login_ID;
                    var userLocationStr = await _repository.InsertUserLocationAsync(locationData);
                    var userAuditModel = new User_Audit
                    {
                        User_Id = locationData.CREATED_BY,
                        User_IP = locationData.Ip,
                        User_Location = locationData.Loc,
                        Activity = "Login Activity",
                        ATTRIBUTE1 = locationData.Hostname,
                        ATTRIBUTE2 = locationData.City,
                        ATTRIBUTE3 = locationData.Region,
                        ATTRIBUTE4 = locationData.Country,
                        ATTRIBUTE5 = locationData.Org,
                        ATTRIBUTE6 = locationData.Postal,
                        ATTRIBUTE7 = locationData.Timezone,
                        ATTRIBUTE8 = locationData.Readme,
                        CREATED_BY = locationData.CREATED_BY,
                        CREATION_DATE = locationData.CREATION_DATE,
                        LAST_UPDATED_BY = locationData.LAST_UPDATED_BY,
                        LAST_UPDATE_DATE = locationData.LAST_UPDATE_DATE,
                        DELETE_FLAG = locationData.DELETE_FLAG


                    };
                    var userAudit = await _repository.InsertUserAuditAsync(userAuditModel);
                }
                //// END IP Address by Itemad Hyder 27-10-2025
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
                //// To get The IpAddress Added By Itemad Hyder 27-10-2025
                var ipAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();

                if (ipAddress == "::1")
                    ipAddress = "127.0.0.1";

                var client = _httpClientFactory.CreateClient();
                var publicIpResponse = await client.GetStringAsync("https://api.ipify.org");
                var publicIp = publicIpResponse.Trim();
                var response = await client.GetAsync($"https://ipinfo.io/{publicIp}/json");
                if (!response.IsSuccessStatusCode)
                {
                    var responses = new EmployeeLoginOutput_LIST
                    {
                        Status = "Error",
                        Message = "Unable to get location.",
                        Data = null
                    };
                    return Ok(response);
                }
                //return BadRequest("Unable to get location.");

                var json = await response.Content.ReadAsStringAsync();
                var locationData = JsonConvert.DeserializeObject<UserLocationInfo>(json);
                if (locationData != null)
                {
                    locationData.CREATED_BY = employeeCompanyMSTInput_NT.Login_ID;
                    var userLocationStr = await _repository.InsertUserLocationAsync(locationData);
                    var userAuditModel = new User_Audit
                    {
                        User_Id = locationData.CREATED_BY,
                        User_IP = locationData.Ip,
                        User_Location = locationData.Loc,
                        Activity = "Login Activity",
                        ATTRIBUTE1 = locationData.Hostname,
                        ATTRIBUTE2 = locationData.City,
                        ATTRIBUTE3 = locationData.Region,
                        ATTRIBUTE4 = locationData.Country,
                        ATTRIBUTE5 = locationData.Org,
                        ATTRIBUTE6 = locationData.Postal,
                        ATTRIBUTE7 = locationData.Timezone,
                        ATTRIBUTE8 = locationData.Readme,
                        CREATED_BY = locationData.CREATED_BY,
                        CREATION_DATE = locationData.CREATION_DATE,
                        LAST_UPDATED_BY = locationData.LAST_UPDATED_BY,
                        LAST_UPDATE_DATE = locationData.LAST_UPDATE_DATE,
                        DELETE_FLAG = locationData.DELETE_FLAG


                    };
                    var userAudit = await _repository.InsertUserAuditAsync(userAuditModel);
                }
                //// END IP Address by Itemad Hyder 27-10-2025

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


        [HttpPost]
        [Route("LoginEmail_ResetOtp_NT")]
        public async Task<ActionResult<LoginMobileEmail_NT>> LoginEmail_ResetOtp_NT([FromBody] EmployeeMobileMSTInput_NT employeeCompanyMSTInput_NT)
        {
            try
            {
                var LoginValidate = await _repository.LoginEmailOtpResetNTAsync(employeeCompanyMSTInput_NT);
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
