using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class UpdateApprovalTemplates
    {
        public int MKEY { get; set; }
        public int BUILDING_TYPE { get; set; }
        public int BUILDING_STANDARD { get; set; }
        public int STATUTORY_AUTHORITY { get; set; }
        public string MAIN_ABBR { get; set; }
        public string SHORT_DESCRIPTION { get; set; }
        public string LONG_DESCRIPTION { get; set; }
        public int? AUTHORITY_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public string? TAGS { get; set; }
        public int? DAYS_REQUIERD { get; set; }
        [Required]
        public string SEQ_ORDER { get; set; }
        public int? CREATED_BY { get; set; }
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        public List<UPDATE_APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<UPDATE_APPROVAL_TEMPLATE_TRL_SUBTASK>();
        public List<UPDATE_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<UPDATE_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
    }

    public class UpdateApprovalTemplatesNT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Building_Type")]
        public int Building_Type { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int Building_Standard { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int Statutory_Authority { get; set; }
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
        [JsonPropertyName("Job_Role")]
        public int? Job_Role { get; set; }
        [JsonPropertyName("Tags")]
        public string? Tags { get; set; }
        [JsonPropertyName("Days_Requierd")]
        public int? Days_Requierd { get; set; }
        [JsonPropertyName("Seq_Order")]
        [Required]
        public string Seq_Order { get; set; }
        [JsonPropertyName("Created_By")]
        public int? Created_By { get; set; }
        [JsonPropertyName("End_Result_Doc_Lst")]
        public Dictionary<string, object>? End_Result_Doc_Lst { get; set; }
        [JsonPropertyName("Checklist_Doc_Lst")]
        public Dictionary<string, object>? Checklist_Doc_Lst { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<UPDATE_APPROVAL_TEMPLATE_TRL_SUBTASK>? Subtask_List { get; set; } = new List<UPDATE_APPROVAL_TEMPLATE_TRL_SUBTASK>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<UPDATE_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? Sanctioning_Department_List { get; set; } = new List<UPDATE_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }


    public class UPDATE_APPROVAL_TEMPLATE_TRL_SUBTASK
    {
        public int MKEY { get; set; }
        public string SEQ_NO { get; set; }
        public string SUBTASK_ABBR { get; set; }
        public int SUBTASK_MKEY { get; set; }
    }

    public class UPDATE_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }   
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
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
