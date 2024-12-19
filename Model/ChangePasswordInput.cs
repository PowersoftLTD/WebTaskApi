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
}
