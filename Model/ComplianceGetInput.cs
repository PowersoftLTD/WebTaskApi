using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Model
{
    public class ComplianceOutput_LIST
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<ComplianceOutPut> Data { get; set; }
    }

    public class ComplianceOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("PROPERTY")]
        public int PROPERTY { get; set; }
        [JsonPropertyName("BUILDING")]
        public int BUILDING { get; set; }
        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("LONG_DESCRIPTION")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("RAISED_AT")]
        public int RAISED_AT { get; set; }
        [JsonPropertyName("RESPONSIBLE_DEPARTMENT")]
        public int RESPONSIBLE_DEPARTMENT { get; set; }
        [JsonPropertyName("JOB_ROLE")]
        public int JOB_ROLE { get; set; }
        [JsonPropertyName("Tags")]
        public int Tags { get; set; }
        //public List<ComplianceTagsInput>? Tags { get; set; } = new List<ComplianceTagsInput>();
        [JsonPropertyName("RESPONSIBLE_PERSON")]
        public int RESPONSIBLE_PERSON { get; set; }
        [JsonPropertyName("TO_BE_COMPLETED_BY")]
        public DateTime TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("NO_DAYS")]
        public int NO_DAYS { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonIgnore]
        public string? ResponseStatus { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
    }

    public class ComplianceTagsInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("TAGS_NAME")]
        public string TAGS_NAME { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public Char DELETE_FLAG { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
    }

    public class ComplianceInsertUpdateInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("PROPERTY")]
        public int PROPERTY { get; set; }
        [JsonPropertyName("BUILDING")]
        public int BUILDING { get; set; }
        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("LONG_DESCRIPTION")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("RAISED_AT")]
        public int RAISED_AT { get; set; }
        [JsonPropertyName("RESPONSIBLE_DEPARTMENT")]
        public int RESPONSIBLE_DEPARTMENT { get; set; }
        [JsonPropertyName("JOB_ROLE")]
        public int JOB_ROLE { get; set; }
        [JsonPropertyName("RESPONSIBLE_PERSON")]
        public int RESPONSIBLE_PERSON { get; set; }
        [JsonPropertyName("TO_BE_COMPLETED_BY")]
        public DateTime TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("NO_DAYS")]
        public int? NO_DAYS { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Tags")]
        public string? Tags { get; set; }
        //public List<ComplianceTagsInput>? Tags { get; set; } = new List<ComplianceTagsInput>();
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
    }

    public class ComplianceGetInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("LOGGED_IN")]
        public int LOGGED_IN { get; set; }
    }
}
