using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{

    public class PROJECT_TRL_APPROVAL_ABBR_NT
    {
        [JsonPropertyName("Header_Mkey")]
        public int? HEADER_MKEY { get; set; }

        [JsonPropertyName("Approval_Mkey")]
        public int? APPROVAL_MKEY { get; set; }

        [JsonPropertyName("Seq_No")]
        public string? SEQ_NO { get; set; }

        [JsonPropertyName("Task_No_Mkey")]
        public int? TASK_NO_MKEY { get; set; }

        [JsonPropertyName("Approval_Abbrivation")]
        public string? APPROVAL_ABBRIVATION { get; set; }

        [JsonPropertyName("Approval_Description")]
        public string? APPROVAL_DESCRIPTION { get; set; }
        [JsonPropertyName("Days_Required")]
        public int? DAYS_REQUIRED { get; set; }
        [JsonPropertyName("Department")]
        public int? DEPARTMENT { get; set; }

        [JsonPropertyName("Job_Role")]
        public int? JOB_ROLE { get; set; }

        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("Tentative_Start_Date")]
        public string? TENTATIVE_START_DATE { get; set; }

        [JsonPropertyName("Tentative_End_Date")]
        public string? TENTATIVE_END_DATE { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public string? DELETE_FLAG { get; set; }
        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }

    }

    public class PROJECT_TRL_APPROVAL_ABBR
    {
        [Column("HEADER_MKEY")]
        public int? HEADER_MKEY { get; set; }

        [Column("SEQ_NO")]
        public string? TASK_NO { get; set; }

        [Column("TASK_NO")]
        public string? SEQ_NO { get; set; }

        [Column("TASK_MKEY")]
        public int? MKEY { get; set; }

        [Column("TASK_NAME")]
        public string? TASK_NAME { get; set; }

        [Column("TASK_STATUS")]
        public string? TASK_STATUS { get; set; }

        [Column("TASK_RESPOSIBLE_EMP_MKEY")]
        public int? TASK_RESPOSIBLE_EMP_MKEY { get; set; }

        [Column("COMPLETION_DATE")]
        public string? COMPLETION_DATE { get; set; }

        [Column("PERCENT_COMPLETE")]
        public string? Percent_Complete { get; set; }

        [Column("EMP_FULL_NAME")]
        public string? EMP_FULL_NAME { get; set; }
        [Column("APPROVAL_MKEY")]
        public int? APPROVAL_MKEY { get; set; }

        [Column("APPROVAL_ABBRIVATION")]
        public string? APPROVAL_ABBRIVATION { get; set; }

        [Column("APPROVAL_DESCRIPTION")]
        public string? APPROVAL_DESCRIPTION { get; set; }

        [Column("DAYS_REQUIRED")]
        public int? DAYS_REQUIRED { get; set; }

        [Column("DEPARTMENT")]
        public int? DEPARTMENT { get; set; }

        [Column("JOB_ROLE")]
        public int? JOB_ROLE { get; set; }

        [Column("RESPOSIBLE_EMP_MKEY")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [Column("RESPOSIBLE_EMP_NAME")]
        public string? RESPOSIBLE_EMP_NAME { get; set; }

        [Column("OUTPUT_DOCUMENT")]
        public string? OUTPUT_DOCUMENT { get; set; }

        [Column("TENTATIVE_START_DATE")]
        public string? TENTATIVE_START_DATE { get; set; }

        [Column("TENTATIVE_END_DATE")]
        public string? TENTATIVE_END_DATE { get; set; }

        [Column("STATUS")]
        public string? STATUS { get; set; }

        [Column("CREATED_BY")]
        public int CREATED_BY { get; set; }

        [Column("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }

        [Column("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }

        [Column("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }

        [Column("ATTRIBUTE4")]
        public string? ATTRIBUTE4 { get; set; }

        [Column("ATTRIBUTE5")]
        public string? ATTRIBUTE5 { get; set; }
        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }

    }
}
