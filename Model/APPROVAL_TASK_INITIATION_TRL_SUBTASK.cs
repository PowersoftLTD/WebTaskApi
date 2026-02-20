using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{

    public class APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT_OUTPUT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT> Data { get; set; }
    }

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
        //public string? SUBTASK_MKEY { get; set; }
        public string? ResponseStatus { get; set; }
        public List<APPROVAL_TEMPLATE_TRL_CHECKLIST_Model>? APPROVAL_CHECK_LIST { get; set; } = new List<APPROVAL_TEMPLATE_TRL_CHECKLIST_Model>();
        public List<APPROVAL_TEMPLATE_TRL_ENDRESULT_PS_Model>? End_Result_Doc_Lst { get; set; } = new List<APPROVAL_TEMPLATE_TRL_ENDRESULT_PS_Model>();
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();

    }


    #region 
    // Model Created by Itemad Hyder  04-02-2026 
    public class APPROVAL_TEMPLATE_TRL_CHECKLIST_Model
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int? SR_NO { get; set; }
        [JsonPropertyName("DOCUMENT_NAME")]
        public string DOCUMENT_NAME { get; set; }
        [JsonPropertyName("DOCUMENT_CATEGORY")]
        public string DOCUMENT_CATEGORY { get; set; }
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
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("LAST_UPDATE_DATE")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("APP_CHECK")]
        public string APP_CHECK { get; set; }
        [JsonPropertyName("DOCUMENT_MKEY")]
        public string? DOCUMENT_MKEY { get; set; }

    }



    public class APPROVAL_TASK_INITIATION_TRL_SUBTASK_PS
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
        //public string? SUBTASK_MKEY { get; set; }

        public List<TASK_CHECKLIST_TABLE_INPUT_PS>? APPROVAL_CHECK_LIST { get; set; } = new List<TASK_CHECKLIST_TABLE_INPUT_PS>();
        public List<TASK_ENDLIST_TABLE_INPUT_PS>? End_Result_Doc_Lst { get; set; } = new List<TASK_ENDLIST_TABLE_INPUT_PS>();
        //public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();

    }

    public class Add_ApprovalTemplateInitiation_OutPut_PS
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public APPROVAL_TASK_INITIATION_TRL_SUBTASK_PS Data { get; set; }
        //[JsonPropertyName("Data1")]
        //public TaskPostActionFileUploadAPI_NT Data1 { get; set; }
        //[JsonPropertyName("Data2")]
        //public TaskFileUploadAPI_NT Data2 { get; set; }
    }



    #endregion
}
