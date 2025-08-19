using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class PutChangePasswordOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<PutChangePasswordOutPut> Data { get; set; }
    }
    public class PutChangePasswordOutPut
    {

        [JsonPropertyName("MessageText")]
        public string? MessageText { get; set; }
    }
    public class PutChangePasswordOutPutNT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<PutChangePasswordNT> Data { get; set; }
    }
    public class PutChangePasswordNT
    {

        [JsonPropertyName("MessageText")]
        public string? MessageText { get; set; }
    }

}
