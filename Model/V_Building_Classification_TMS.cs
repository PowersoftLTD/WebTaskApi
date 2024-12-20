using System;
using System.Collections;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class V_Building_Classification_new
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<V_Building_Classification_TMS> Data { get; set; }
    
    }

    public class V_Building_Classification_TMS
    {
        [JsonPropertyName("MKEY")]
        public decimal? MKEY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public decimal? COMPANY_ID { get; set; }
        [JsonPropertyName("TYPE_CODE")]
        public string? TYPE_CODE { get; set; }
        [JsonPropertyName("TYPE_DESC")]
        public string? TYPE_DESC { get; set; }
        [JsonPropertyName("TYPE_ABBR")]
        public string? TYPE_ABBR { get; set; }
        [JsonPropertyName("PARENT_ID")]
        public decimal? PARENT_ID { get; set; }
        [JsonPropertyName("MASTER_MKEY")]
        public decimal? MASTER_MKEY { get; set; }
        [JsonPropertyName("EFFECTIVE_START_DATE")]
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        [JsonPropertyName("EFFECTIVE_END_DATE")]
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        [JsonPropertyName("ENABLE_FLAG")]
        public char? ENABLE_FLAG { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("ATTRIBUTE4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("ATTRIBUTE5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("ATTRIBUTE6")]
        public decimal? ATTRIBUTE6 { get; set; }
        [JsonPropertyName("ATTRIBUTE7")]
        public decimal? ATTRIBUTE7 { get; set; }
        [JsonPropertyName("ATTRIBUTE8")]
        public decimal? ATTRIBUTE8 { get; set; }
        [JsonPropertyName("ATTRIBUTE9")]
        public decimal? ATTRIBUTE9 { get; set; }
        [JsonPropertyName("ATTRIBUTE10")]
        public decimal? ATTRIBUTE10 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public decimal? CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public decimal? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("LAST_UPDATE_DATE")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }

       
    }
}
