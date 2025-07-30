using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Header_Mkey")]
        public int? HEADER_MKEY { get; set; }

        [Column("Approval_Mkey")]
        public int? APPROVAL_MKEY { get; set; }

        [JsonPropertyName("Task_No")]
        public string? TASK_NO { get; set; }  // SEQ NO

        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }  // SEQ NO

        [JsonPropertyName("Approval_Abbrivation")]
        public string? APPROVAL_ABBRIVATION { get; set; }

        [JsonPropertyName("Long_Description")]
        public string? LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("Short_Description")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("Days_Required")]
        public int? DAYS_REQUIRED { get; set; }

        [JsonPropertyName("Department")]
        public int? DEPARTMENT { get; set; }
        [JsonPropertyName("Job_Role")]
        public int? JOB_ROLE { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [JsonPropertyName("Resposible_Emp_Name")]
        public string? RESPOSIBLE_EMP_NAME { get; set; }

        [JsonPropertyName("Output_Document")]
        public string? OUTPUT_DOCUMENT { get; set; }
        [JsonPropertyName("Tentative_Start_Date")]
        public DateTime? TENTATIVE_START_DATE { get; set; }
        [JsonPropertyName("Tentative_End_Date")]
        public DateTime? TENTATIVE_END_DATE { get; set; }
        [JsonPropertyName("Complition_Date")]
        public string? COMPLITION_DATE { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int? LAST_UPDATED_BY { get; set; }
        public string? TRLStatus { get; set; }
        public string? Message { get; set; }
    }

    public class APPROVAL_TASK_INITIATION_TRL_SUBTASK
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("HEADER_MKEY")]
        public int? HEADER_MKEY { get; set; }

        [Column("APPROVAL_MKEY")]
        public int? APPROVAL_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string? TASK_NO { get; set; }  // SEQ NO

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

        [JsonPropertyName("RESPOSIBLE_EMP_NAME")]
        public string? RESPOSIBLE_EMP_NAME { get; set; }

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
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int? LAST_UPDATED_BY { get; set; }
        public string? TRLStatus { get; set; }
        public string? Message { get; set; }
    }
}
