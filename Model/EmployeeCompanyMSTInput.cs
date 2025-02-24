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
        [JsonPropertyName("Login_Id")]
        public string Login_ID { get; set; }
        [JsonPropertyName("Login_Password")]
        public string Login_Password { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

    }
}
