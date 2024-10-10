using System.ComponentModel.DataAnnotations;
using TaskManagement.API.Interfaces;

namespace TaskManagement.API.Model
{
    public class EMPLOYEE_MST 
    {
        [Required]
        [DataType(DataType.Text)]
        public string LOGIN_NAME { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string LOGIN_PASSWORD { get; set; }
    }
}
