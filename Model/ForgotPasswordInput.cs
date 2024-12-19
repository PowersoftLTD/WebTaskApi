using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class ForgotPasswordInput
    {
        [JsonPropertyName("LoginName")]
        public string LoginName { get; set; }
        
    }
}
