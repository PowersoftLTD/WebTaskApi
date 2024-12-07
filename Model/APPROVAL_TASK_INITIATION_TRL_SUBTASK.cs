using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class APPROVAL_TASK_INITIATION_TRL_SUBTASK
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        
        [Column("APPROVAL_MKEY")]
        public int? APPROVAL_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }  // SEQ NO

        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }  // SEQ NO

        [JsonPropertyName("APPROVAL_ABBRIVATION")]
        public string? APPROVAL_ABBRIVATION { get; set; }

        [JsonPropertyName("LONG_DESCRIPTION")]
        public string? LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("DAYS_REQUIRED")]
        public int? DAYS_REQUIRED { get; set; }

        [JsonPropertyName("DEPARTMENT")]
        public int? DEPARTMENT { get; set; }
        [JsonPropertyName("JOB_ROLE")]
        public int? JOB_ROLE { get; set; }
        [JsonPropertyName("RESPOSIBLE_EMP_MKEY")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [JsonPropertyName("OUTPUT_DOCUMENT")]
        public string? OUTPUT_DOCUMENT { get; set; }
        [JsonPropertyName("TENTATIVE_START_DATE")]
        public DateTime? TENTATIVE_START_DATE { get; set; }
        [JsonPropertyName("TENTATIVE_END_DATE")]
        public DateTime? TENTATIVE_END_DATE { get; set; }
        [JsonPropertyName("COMPLITION_DATE")]
        public string? COMPLITION_DATE { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }

        public string? TRLStatus { get; set; }
        public string? Message { get; set; }
    }
}
