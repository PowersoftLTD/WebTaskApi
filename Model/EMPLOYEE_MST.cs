using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagement.API.Model
{
    public class EMPLOYEE_MST 
    {
        [Required]
        //[DataType(DataType.Text)]
        public string LOGIN_NAME { get; set; }
        [Required]
        //[DataType(DataType.Password)]
        public string LOGIN_PASSWORD { get; set; }
    }

    public class EMPLOYEE_MST_NT
    {
        
        [JsonPropertyName("Login_Name")]
        public string LOGIN_NAME { get; set; }
        
        [JsonPropertyName("Login_Password")]
        public string LOGIN_PASSWORD { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_ID { get; set; }
    }
    public class userDepartment
    {
        [JsonPropertyName("departmentId")]
        public string? departmentId { get; set; }
    }
}
