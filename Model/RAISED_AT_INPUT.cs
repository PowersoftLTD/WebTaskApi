using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class RAISED_AT_OUTPUT_LIST
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<RAISED_AT_OUTPUT> Data { get; set; }
    }

    public class RAISED_AT_INPUT
    {
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        
        [JsonPropertyName("USER_ID")]
        public int USER_ID { get; set; }
    }

    public class RAISED_AT_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SEQ_NO")]
        public string? SEQ_NO { get; set; }
        [JsonPropertyName("TYPE_CODE")]
        public string? TYPE_CODE { get; set; }
        [JsonPropertyName("TYPE_DESC")]
        public string? TYPE_DESC { get; set; }
        [JsonPropertyName("TYPE_ABBR")]
        public string? TYPE_ABBR { get; set; }

        [JsonPropertyName("CREATED_BY_ID")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("CREATED_BY_NAME")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("LAST_UPDATED_BY")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("UPDATED_BY_NAME")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("LAST_UPDATE_DATE")]
        public string? LAST_UPDATE_DATE { get; set; }

    }
}
