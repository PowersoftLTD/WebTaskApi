using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class ValidateEmailInput
    {
        [JsonPropertyName("Login_ID")]
        public string Login_ID { get; set; }

    }
}
