using System;
using System.Collections;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class V_Instruction
    {
        [JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }
        [JsonPropertyName("TYPE_DESC")]
        public string? TYPE_DESC { get; set; }
        [JsonPropertyName("TYPE_CODE")]
        public string? TYPE_CODE { get; set; }

        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }

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
    public class V_Building_Classification
    {
        public decimal? MKEY { get; set; }

        public decimal? COMPANY_ID { get; set; }

        public string? TYPE_CODE { get; set; }

        public string? TYPE_DESC { get; set; }

        public string? TYPE_ABBR { get; set; }

        public decimal? PARENT_ID { get; set; }

        public decimal? MASTER_MKEY { get; set; }

        public DateTime? EFFECTIVE_START_DATE { get; set; }

        public DateTime? EFFECTIVE_END_DATE { get; set; }

        public char? ENABLE_FLAG { get; set; }

        public string? ATTRIBUTE1 { get; set; }

        public string? ATTRIBUTE2 { get; set; }

        public string? ATTRIBUTE3 { get; set; }

        public string? ATTRIBUTE4 { get; set; }

        public string? ATTRIBUTE5 { get; set; }

        public decimal? ATTRIBUTE6 { get; set; }

        public decimal? ATTRIBUTE7 { get; set; }

        public decimal? ATTRIBUTE8 { get; set; }

        public decimal? ATTRIBUTE9 { get; set; }

        public decimal? ATTRIBUTE10 { get; set; }

        public decimal? CREATED_BY { get; set; }

        public DateTime? CREATION_DATE { get; set; }

        public decimal? LAST_UPDATED_BY { get; set; }

        public DateTime? LAST_UPDATE_DATE { get; set; }

        public char? DELETE_FLAG { get; set; }


    }
}
