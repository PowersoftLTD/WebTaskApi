using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class ChangePasswordInput
    {
        [JsonPropertyName("LoginName")]
        public string LoginName { get; set; }
        [JsonPropertyName("Old_Password")]
        public string Old_Password { get; set; }
        [JsonPropertyName("New_Password")]
        public string New_Password { get; set; }
    }

    public class ChangePasswordInputNT
    {
        [JsonPropertyName("LoginName")]
        public string LoginName { get; set; }
        [JsonPropertyName("Old_Password")]
        public string Old_Password { get; set; }
        [JsonPropertyName("New_Password")]
        public string New_Password { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }

    }
}
