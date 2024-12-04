using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class APPROVAL_TASK_INITIATION
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("HEADER_MKEY")]
        public int HEADER_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string? TASK_NO { get; set; }  // SEQ NO
        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("CAREGORY")]
        public int? CAREGORY { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }

        [JsonPropertyName("MAIN_ABBR")]
        public string? MAIN_ABBR { get; set; }

        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("LONG_DESCRIPTION")]
        public string? LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("AUTHORITY_DEPARTMENT")]
        public int? AUTHORITY_DEPARTMENT { get; set; }

        [JsonPropertyName("RESPOSIBLE_EMP_MKEY")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [JsonPropertyName("JOB_ROLE")]
        public int? JOB_ROLE { get; set; }

        [JsonPropertyName("SANCTION_AUTHORITY")]
        public int? SANCTION_AUTHORITY { get; set; }

        [JsonPropertyName("SANCTION_DEPARTMENT")]
        public string? SANCTION_DEPARTMENT { get; set; }

        [JsonPropertyName("COMPLITION_DATE")]
        public string? COMPLITION_DATE { get; set; }

        [JsonPropertyName("PROPERTY")]
        public int? PROPERTY { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }

        [JsonPropertyName("TENTATIVE_START_DATE")]
        public DateTime? TENTATIVE_START_DATE { get; set; }
        [JsonPropertyName("TENTATIVE_END_DATE")]
        public DateTime? TENTATIVE_END_DATE { get; set; }

        [JsonPropertyName("SUBTASK_LIST")]
        public List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();

        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }
    }
}
