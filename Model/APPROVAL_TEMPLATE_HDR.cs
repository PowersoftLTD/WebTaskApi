using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class OutPutApprovalTemplates_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<OutPutApprovalTemplatesNT> Data { get; set; }
    }
    public class OutPutApprovalTemplatesNT
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
        public Dictionary<string, object>? End_Result_Doc_Lst { get; set; }
        [JsonPropertyName("Checklist_Doc_Lst")]
        public Dictionary<string, object>? Checklist_Doc_Lst { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>? Subtask_List { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? Sanctioning_Department_List { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
    }

    public class APPROVAL_TEMPLATE_HDR_NT_OUTPUT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<APPROVAL_TEMPLATE_HDR_NT> Data { get; set; }
    }

    public class APPROVAL_TEMPLATE_HDR_INPUT
    {
        [JsonPropertyName("Abbr")]
        public string? strABBR { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int? Business_Group_Id { get; set; }
    }

    public class APPROVAL_TEMPLATE_HDR_INPUT_NT
    {
        [JsonPropertyName("LoggedInID")]
        public string? LoggedInID { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int? Business_Group_Id { get; set; }
    }
    public class APPROVAL_TEMPLATE_HDR_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Building_Type")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Main_Abbr")]
        public string? MAIN_ABBR { get; set; }
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
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Attribute4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("Attribute5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Sanction_Authority")]
        public int? SANCTION_AUTHORITY { get; set; }
        [JsonPropertyName("Sanction_Department")]
        public string? SANCTION_DEPARTMENT { get; set; }
        [JsonPropertyName("End_Result_Doc")]
        public string? END_RESULT_DOC { get; set; }
        [JsonPropertyName("Checklist_Doc")]
        public string? CHECKLIST_DOC { get; set; }
        [JsonPropertyName("Abbr_Short_Desc")]
        public string? ABBR_SHORT_DESC { get; set; }
        [JsonPropertyName("End_Result_Doc_Lst")]
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        [JsonPropertyName("Checklist_Doc_Lst")]
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TEMPLATE_TRL_SUBTASK>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
        public string? Status { get; set; }
        public string? Message { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public Char? DELETE_FLAG { get; set; }

    }


    public class APPROVAL_TEMPLATE_HDR
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Building_Type")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Main_Abbr")]
        public string? MAIN_ABBR { get; set; }
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
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Attribute4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("Attribute5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Sanction_Authority")]
        public int? SANCTION_AUTHORITY { get; set; }
        [JsonPropertyName("Sanction_Department")]
        public string? SANCTION_DEPARTMENT { get; set; }
        [JsonPropertyName("End_Result_Doc")]
        public string? END_RESULT_DOC { get; set; }
        [JsonPropertyName("Checklist_Doc")]
        public string? CHECKLIST_DOC { get; set; }
        [JsonPropertyName("Abbr_Short_Desc")]
        public string? ABBR_SHORT_DESC { get; set; }
        [JsonPropertyName("End_Result_Doc_Lst")]
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        [JsonPropertyName("Checklist_Doc_Lst")]
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        [JsonPropertyName("Subtask_List")]
        public List<APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TEMPLATE_TRL_SUBTASK>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
        public string? Status { get; set; }
        public string? Message { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public Char? DELETE_FLAG { get; set; }

    }

    public class APPROVAL_TASK_INITIATION_NT_INUT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }

        [JsonPropertyName("Approval_MKEY")]
        public int? APPROVAL_MKEY { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int? Business_Group_Id { get; set; }
    }
    public class APPROVAL_TASK_INITIATION_NT_OUTPUT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<APPROVAL_TASK_INITIATION_NT> Data { get; set; }
    }


    public class APPROVAL_TASK_INITIATION_NT
    {

        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Header_Mkey")]
        public int HEADER_MKEY { get; set; }

        [JsonPropertyName("Task_No")]
        public string? TASK_NO { get; set; }  // SEQ NO
        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("Caregory")]
        public int? CAREGORY { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }

        [JsonPropertyName("Main_Abbr")]
        public string? MAIN_ABBR { get; set; }

        [JsonPropertyName("Short_Description")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("Long_Description")]
        public string? LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("Authority_Department")]
        public int? AUTHORITY_DEPARTMENT { get; set; }

        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [JsonPropertyName("Resposible_Emp_Name")]
        public string? RESPOSIBLE_EMP_NAME { get; set; }

        [JsonPropertyName("Job_Role")]
        public int? JOB_ROLE { get; set; }

        [JsonPropertyName("Sanction_Authority")]
        public int? SANCTION_AUTHORITY { get; set; }

        [JsonPropertyName("Sanction_Department")]
        public string? SANCTION_DEPARTMENT { get; set; }

        [JsonPropertyName("Complition_Date")]
        public string? COMPLITION_DATE { get; set; }

        [JsonPropertyName("Days_Requierd")]
        public string? DAYS_REQUIERD { get; set; }

        [JsonPropertyName("Property")]
        public int? PROPERTY { get; set; }

        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }

        [JsonPropertyName("Tentative_Start_Date")]
        public DateTime? TENTATIVE_START_DATE { get; set; }
        [JsonPropertyName("Tentative_End_Date")]
        public DateTime? TENTATIVE_END_DATE { get; set; }

        [JsonPropertyName("Subtask_List")]
        public List<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK_NT>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();

        [JsonPropertyName("Initiator")]
        public int INITIATOR { get; set; }
        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }

    }

}
