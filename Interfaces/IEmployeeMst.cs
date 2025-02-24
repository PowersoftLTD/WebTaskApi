using TaskManagement.API.Model;

namespace TaskManagement.API.Interfaces
{
    public interface IEmployeeMst
    {
        Task<EMPLOYEE_MST> LoginAsync(string UserName);
        Task<EMPLOYEE_MST> CheckPasswordAsync(string UserName,string Password);

        Task<EMPLOYEE_MST> LoginNTAsync(EMPLOYEE_MST_NT eMPLOYEE_MST_NT);
        Task<EMPLOYEE_MST> CheckPasswordNTAsync(EMPLOYEE_MST_NT eMPLOYEE_MST_NT);
    }
}
