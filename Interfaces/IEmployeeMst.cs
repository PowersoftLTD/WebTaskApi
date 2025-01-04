using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IEmployeeMst
    {
        Task<EMPLOYEE_MST> LoginAsync(string UserName);
        Task<EMPLOYEE_MST> CheckPasswordAsync(string UserName,string Password);
    }
}
