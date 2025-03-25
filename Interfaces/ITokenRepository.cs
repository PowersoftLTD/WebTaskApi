using Microsoft.AspNetCore.Identity;
using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> CreateJWTToken(string LoginUser);
        Task<string> CreateJWTToken_NT(string Login_ID);
    }
}
