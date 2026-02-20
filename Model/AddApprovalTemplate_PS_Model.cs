using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class AddApprovalTemplate_PS_Model
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("Building_Type")]
        public int BUILDING_TYPE { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Main_Abbr")]
        public string MAIN_ABBR { get; set; }
        [JsonPropertyName("Short_Description")]
        public string? SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("Long_Description")]
        public string? LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("Authority_Department")]
        public int? AUTHORITY_DEPARTMENT { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("Job_Role")]
        public int? JOB_ROLE { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Days_Requierd")]
        public int? DAYS_REQUIERD { get; set; }
        [JsonPropertyName("Seq_Order")]
        public string? SEQ_ORDER { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        //[JsonPropertyName("End_Result_Doc_Lst")]
        //public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        //[JsonPropertyName("Checklist_Doc_Lst")]
        //public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK__PS>? SUBTASK_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK__PS>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT_Model>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT_Model>();
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

        [JsonPropertyName("Delete_Flag")]
        public string DELETE_FLAG { get; set; }

        [JsonPropertyName("Checklist_Doc_Lst")]
        public List<TASK_CHECKLIST_TABLE_INPUT_PS>? tASK_CHECKLIST_TABLE_INPUT_NT { get; set; }
        [JsonPropertyName("End_Result_Doc_Lst")]
        public List<TASK_ENDLIST_TABLE_INPUT_PS>? tASK_ENDLIST_TABLE_INPUT_NTs { get; set; }
    }


    public class Add_ApprovalTemplate_TaskOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public AddApprovalTemplate_PS_Model Data { get; set; }
        //[JsonPropertyName("Data1")]
        //public TaskPostActionFileUploadAPI_NT Data1 { get; set; }
        //[JsonPropertyName("Data2")]
        //public TaskFileUploadAPI_NT Data2 { get; set; }
    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT_Model
    {
        [JsonPropertyName("Level")]
        public string LEVEL { get; set; }
        [JsonPropertyName("SR_NO")]
        public string SR_NO { get; set; }
        [JsonPropertyName("Sanctioning_Department")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("Sanctioning_Authority")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT_MKEY")]
        public int? SANCTIONING_AUTHORITY_MKEY { get; set; }
        [JsonPropertyName("Start_Date")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("End_Date")]
        public DateTime? END_DATE { get; set; }

        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char DELETE_FLAG { get; set; }

    }

    public class CommonResponseAddApprovalTemplatePS
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public Add_ApprovalTemplate_TaskOutPut_List_NT Data { get; set; }
    }

    public class AddApprovalTemplate_PS_Return_Models
    {
        [JsonPropertyName("MKEY")]
        public decimal? MKEY { get; set; }
        [JsonPropertyName("Status")]
        public string Status { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }

    public class AddApprovalTemplate_HDR_PS_Model
    {
        public int MKEY { get; set; }
        public int? BUILDING_TYPE { get; set; }
        public int? BUILDING_STANDARD { get; set; }
        public int? STATUTORY_AUTHORITY { get; set; }
        public string MAIN_ABBR { get; set; }
        public string SHORT_DESCRIPTION { get; set; }
        public string LONG_DESCRIPTION { get; set; }
        public int? AUTHORITY_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public string DAYS_REQUIERD { get; set; }
        public int? SANCTIONING_DEPARTMENT_MKEY { get; set; }
        public int? SANCTION_AUTHORITY { get; set; }
        public string SANCTION_DEPARTMENT { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public string END_RESULT_DOC { get; set; }
        public string CHECKLIST_DOC { get; set; }
        public string? DELETE_FLAG { get; set; } 
        public string TAGS { get; set; }
        public string IS_NODE { get; set; }
        public string SEQ_ORDER { get; set; }
    }

    public class EndResultDocModel
    {
        public string? DOCUMENT_NAME { get; set; }
        public int? DOCUMENT_CATEGORY { get; set; }
    }



    public class ApprovalTemplatesNT_OUtPutResponse
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Building_Type")]
        public int Building_Type { get; set; }
        [JsonPropertyName("Building_Type_Name")]
        public string Building_Type_Name { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int Building_Standard { get; set; }
        [JsonPropertyName("Building_Standard_Name")]
        public string Building_Standard_Name { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int Statutory_Authority { get; set; }
        [JsonPropertyName("Statutory_Authority_Name")]
        public string Statutory_Authority_Name { get; set; }
        [JsonPropertyName("Main_Abbr")]
        public string Main_Abbr { get; set; }
        [JsonPropertyName("Short_Description")]
        public string Short_Description { get; set; }
        [JsonPropertyName("Long_Description")]
        public string Long_Description { get; set; }
        [JsonPropertyName("Authority_Department")]
        public int? Authority_Department { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int? Resposible_Emp_Mkey { get; set; }
        [JsonPropertyName("Resposible_Emp_Name")]
        public string? Resposible_Emp_Name { get; set; }
        [JsonPropertyName("Job_Role")]
        public int? Job_Role { get; set; }
        [JsonPropertyName("Tags")]
        public string? Tags { get; set; }
        [JsonPropertyName("Days_Requierd")]
        public int? Days_Requierd { get; set; }
        [JsonPropertyName("Seq_Order")]
        public string? Seq_Order { get; set; }
        [JsonPropertyName("End_Result_Doc_Lst")]
        public List<APPROVAL_TEMPLATE_TRL_ENDRESULT_PS_Model>? End_Result_Doc_Lst { get; set; }
        [JsonPropertyName("Checklist_Doc_Lst")]
        public List<APPROVAL_TEMPLATE_TRL_CHECKLIST_PS_Model>? Checklist_Doc_Lst { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK_Ps>? Subtask_List { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK_Ps>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_Ps>? Sanctioning_Department_List { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_Ps>();
    }


    public class Commonresponse
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public object Data { get; set; }
    }

    public class APPROVAL_TEMPLATE_TRL_ENDRESULT_PS_Model
    {
        public int MKEY { get; set; }

        public int? SR_NO { get; set; }

        public string? DOCUMENT_NAME { get; set; }

        public string? DOCUMENT_CATEGORY { get; set; }

        public string? ATTRIBUTE1 { get; set; }

        public string? ATTRIBUTE2 { get; set; }

        public string? ATTRIBUTE3 { get; set; }

        public string? ATTRIBUTE4 { get; set; }

        public string? ATTRIBUTE5 { get; set; }

        public int? CREATED_BY { get; set; }

        public DateTime? CREATION_DATE { get; set; }

        public int? LAST_UPDATED_BY { get; set; }

        public DateTime? LAST_UPDATE_DATE { get; set; }

        public string? DELETE_FLAG { get; set; }

        public string? DOCUMENT_MKEY { get; set; }
    }

    public class APPROVAL_TEMPLATE_TRL_CHECKLIST_PS_Model
    {
        public int MKEY { get; set; }

        public int? SR_NO { get; set; }

        public string? DOCUMENT_NAME { get; set; }

        public string? DOCUMENT_CATEGORY { get; set; }

        public string? ATTRIBUTE1 { get; set; }

        public string? ATTRIBUTE2 { get; set; }

        public string? ATTRIBUTE3 { get; set; }

        public string? ATTRIBUTE4 { get; set; }

        public string? ATTRIBUTE5 { get; set; }

        public int? CREATED_BY { get; set; }

        public DateTime? CREATION_DATE { get; set; }

        public int? LAST_UPDATED_BY { get; set; }

        public DateTime? LAST_UPDATE_DATE { get; set; }

        public string? DELETE_FLAG { get; set; }

        public string? APP_CHECK { get; set; }

        public int? DOCUMENT_MKEY { get; set; }
    }

    public class OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_Ps
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("LEVEL")]
        public string LEVEL { get; set; }

        [JsonPropertyName("SR_NO")]
        public string SR_NO { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("END_DATE")]
        public DateTime? END_DATE { get; set; }
        public string? DELETE_FLAG { get; set; }
    }

    public class OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK_Ps
    {
        public int HEADER_MKEY { get; set; }
        public string SEQ_NO { get; set; }
        public string SUBTASK_ABBR { get; set; }
        public int SUBTASK_MKEY { get; set; }

        public string? DELETE_FLAG { get; set; }
    }
}
