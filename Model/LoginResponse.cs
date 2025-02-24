using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class LoginResponse
    {
        public string JwtToken { get; set; }
        //public string? Status { get; set; }
        //public string? Message { get; set; }
    }

    public class LoginResponse_NT
    {
        [JsonPropertyName("JwtToken")]
        public string JwtToken { get; set; }
        //public string? Status { get; set; }
        //public string? Message { get; set; }
    }
}
