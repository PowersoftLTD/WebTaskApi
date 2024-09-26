using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IJWTConfigure
    {
        Task<IEnumerable<ConfigDetail>> JWTToken();
    }
}
