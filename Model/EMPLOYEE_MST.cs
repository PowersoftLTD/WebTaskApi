using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

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
        //[JsonIgnore]  // Prevent serialization of the property
        ////[ApiExplorerSettings(IgnoreApi = true)]
        //public string? Status { get; set; }
        //[JsonIgnore]  // Prevent serialization of the property
        ////[ApiExplorerSettings(IgnoreApi = true)]
        //public string? Message { get; set; }
    }
}
