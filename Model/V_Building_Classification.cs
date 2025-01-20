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
    public class GetTaskTypeList
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
  
        [JsonPropertyName("Data")]
        public IEnumerable<GetTaskTypeOutPut> Data { get; set; }

    }

    public class GetTaskTypeOutPut
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
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
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
        [JsonIgnore]
        public string? ATTRIBUTE1 { get; set; }
        [JsonIgnore]
        public string? ATTRIBUTE2 { get; set; }
        [JsonIgnore]
        public string? ATTRIBUTE3 { get; set; }
        [JsonIgnore]
        public string? ATTRIBUTE4 { get; set; }
        [JsonIgnore]
        public string? ATTRIBUTE5 { get; set; }
        [JsonIgnore]
        public decimal? ATTRIBUTE6 { get; set; }
        [JsonIgnore]
        public decimal? ATTRIBUTE7 { get; set; }
        [JsonIgnore]
        public decimal? ATTRIBUTE8 { get; set; }
        [JsonIgnore]
        public decimal? ATTRIBUTE9 { get; set; }
        [JsonIgnore]
        public decimal? ATTRIBUTE10 { get; set; }

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

        public char? DELETE_FLAG { get; set; }

    }
}
