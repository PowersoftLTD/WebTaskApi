using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class EmployeeCompanyMSTInput
    {
        [JsonPropertyName("Login_ID")]
        public string Login_ID { get; set; }
        [JsonPropertyName("Login_Password")]
        public string Login_Password { get; set; }
    }


    public class EmployeeCompanyMSTInput_NT
    {
        [JsonPropertyName("Login_Name")]
        public string Login_ID { get; set; }
        [JsonPropertyName("Login_Password")]
        public string Login_Password { get; set; }
    }
}
