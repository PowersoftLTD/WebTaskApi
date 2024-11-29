using System.ComponentModel;
using TaskManagement.API.Repositories;
using Newtonsoft.Json;

namespace TaskManagement.API.Model
{
    public class ApiResponse<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
