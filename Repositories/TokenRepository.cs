using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Model;

namespace TaskManagement.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly IJWTConfigure _configure;

        public TokenRepository(IConfiguration configuration, IJWTConfigure jWTConfigure)
        {
            this.configuration = configuration;
            this._configure = jWTConfigure;
        }
        public async Task<string> CreateJWTToken(string LoginUser)
        {
            var JWTtokens = await _configure.JWTToken();
            var JWTKEY = JWTtokens.FirstOrDefault();
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, LoginUser, JWTKEY.ClientID, JWTKEY.ClientSecret));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKEY.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                JWTKEY.Issuer,
                JWTKEY.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> CreateJWTToken_NT(string Login_ID)
        {
            var JWTtokens = await _configure.JWTToken();
            var JWTKEY = JWTtokens.FirstOrDefault();
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, Login_ID, JWTKEY.ClientID, JWTKEY.ClientSecret));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKEY.Key));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                JWTKEY.Issuer,
                JWTKEY.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<string> CreateJWTNTToken(EMPLOYEE_MST_NT user)
        //{
        //    var JWTtokens = await _configure.JWTToken();
        //    var JWTKEY = JWTtokens.FirstOrDefault();
        //    var claims = new List<Claim>();

        //    claims.Add(new Claim(ClaimTypes.Email, user.LOGIN_NAME, JWTKEY.ClientID, JWTKEY.ClientSecret));

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKEY.Key));

        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        JWTKEY.Issuer,
        //        JWTKEY.Audience,
        //        claims,
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}



        //public Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    string clientId = string.Empty;
        //    string clientSecret = string.Empty;
        //    string symmetricKeyAsBase64 = string.Empty;

        //    if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
        //    {
        //        context.TryGetFormCredentials(out clientId, out clientSecret);
        //    }

        //    if (context.ClientId == null)
        //    {
        //        context.SetError("invalid_clientId", "client_Id is not set");
        //        return Task.FromResult<object>(null);
        //    }

        //    var audience = AudiencesStore.FindAudience(context.ClientId);

        //    if (audience == null)
        //    {
        //        context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
        //        return Task.FromResult<object>(null);
        //    }

        //    context.Validated();
        //    return Task.FromResult<object>(null);
        //}
    }
}
