using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class InsertApprovalTemplates
    {
        public int BUILDING_TYPE { get; set; }
        public int BUILDING_STANDARD { get; set; }
        public int STATUTORY_AUTHORITY { get; set; }
        public string MAIN_ABBR { get; set; }
        public string? SHORT_DESCRIPTION { get; set; }
        public string? LONG_DESCRIPTION { get; set; }
        public int? AUTHORITY_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public string? TAGS { get; set; }
        public int? DAYS_REQUIERD { get; set; }
        public string? SEQ_ORDER { get; set; }
        public int CREATED_BY { get; set; }
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK>();
        [JsonPropertyName("SANCTIONING_DEPARTMENT_LIST")]
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
    }
    public class InsertApprovalTemplatesNT
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
        [JsonPropertyName("End_Result_Doc_Lst")]
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        [JsonPropertyName("Checklist_Doc_Lst")]
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK_NT>? SUBTASK_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK_NT>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT>();
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK_NT
    {
        [JsonPropertyName("Seq_No")]
        public string? SEQ_NO { get; set; }
        [JsonPropertyName("Subtask_Abbr")]
        public string? SUBTASK_ABBR { get; set; }
        [JsonPropertyName("Subtask_Mkey")]
        public int? SUBTASK_MKEY { get; set; }

    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK
    {
        public string? SEQ_NO { get; set; }
        public string? SUBTASK_ABBR { get; set; }
        public int? SUBTASK_MKEY { get; set; }
    }

    public class GetAbbrAndShortAbbrOutPutNT
    {
        [JsonPropertyName("Building")]
        public int? Building { get; set; }
        [JsonPropertyName("Standard")]
        public int? Standard { get; set; }
        [JsonPropertyName("Authority")]
        public int? Authority { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT_NT
    {
        [JsonPropertyName("Level")]
        public string LEVEL { get; set; }
        [JsonPropertyName("Sanctioning_Department")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("Sanctioning_Authority")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("Start_Date")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("End_Date")]
        public DateTime? END_DATE { get; set; }

    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
    {
        [JsonPropertyName("LEVEL")]
        public string LEVEL { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("END_DATE")]
        public DateTime? END_DATE { get; set; }
    }
}
