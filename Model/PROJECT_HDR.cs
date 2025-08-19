using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{

    public class ProjectHdrNT
    {
        [JsonPropertyName("ProjectMkey")]
        public int ProjectMkey { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class PROJECT_HDR_NT_OUTPUT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<PROJECT_HDR_NT> Data { get; set; }
    }

    public class PROJECT_HDR_NT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int? Building_Mkey { get; set; }  // BUILDING_MKEY
        [JsonPropertyName("Building_Name")]
        public string? BUILDING_NAME { get; set; }
        [JsonPropertyName("Project_Abbr")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        [JsonPropertyName("Property")]
        public int? PROPERTY { get; set; }
        [JsonPropertyName("Property_Name")]
        public string? PROPERTY_NAME { get; set; }
        [JsonPropertyName("Legal_Entity")]
        public string? LEGAL_ENTITY { get; set; }
        [JsonPropertyName("Project_Address")]
        public string? PROJECT_ADDRESS { get; set; }
        [JsonPropertyName("Building_Classification")]
        public int? BUILDING_CLASSIFICATION { get; set; }
        [JsonPropertyName("Building_Type_Name")]
        public string? BUILDING_TYPE_NAME { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Building_Standard_Name")]
        public string? BUILDING_STANDARD_NAME { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Statutory_Authority_Name")]
        public string? STATUTORY_AUTHORITY_NAME { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Approvals_Abbr_List")]
        public List<PROJECT_TRL_APPROVAL_ABBR>? APPROVALS_ABBR_LIST { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
        [JsonPropertyName("Created_By_Id")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("Created_By_Name")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("Last_Updated_By")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("Updated_By_Name")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("Last_Update_Date")]
        public string? LAST_UPDATE_DATE { get; set; }
    }

    public class PROJECT_HDR_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }  // BUILDING_MKEY
        [JsonPropertyName("Building_Name")]
        public string? BUILDING_NAME { get; set; }
        [JsonPropertyName("Project_Abbr")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        [JsonPropertyName("Property")]
        public int? PROPERTY { get; set; }
        [JsonPropertyName("Property_Name")]
        public string? PROPERTY_NAME { get; set; }
        [JsonPropertyName("Legal_Entity")]
        public string? LEGAL_ENTITY { get; set; }
        [JsonPropertyName("Project_Address")]
        public string? PROJECT_ADDRESS { get; set; }
        [JsonPropertyName("Building_Classification")]
        public int? BUILDING_CLASSIFICATION { get; set; }
        [JsonPropertyName("Building_Type_Name")]
        public string? BUILDING_TYPE_NAME { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Building_Standard_Name")]
        public string? BUILDING_STANDARD_NAME { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Statutory_Authority_Name")]
        public string? STATUTORY_AUTHORITY_NAME { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Approvals_Abbr_List")]
        public List<PROJECT_TRL_APPROVAL_ABBR_NT>? APPROVALS_ABBR_LIST { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }

      


    }

    public class PROJECT_HDR
    {
        //[JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }
        //[JsonPropertyName("PROJECT_NAME")]
        public int? PROJECT_NAME { get; set; }  // BUILDING_MKEY
        public string? BUILDING_NAME { get; set; }
        //[JsonPropertyName("PROJECT_ABBR")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        //[JsonPropertyName("PROPERTY")]
        public int? PROPERTY { get; set; } // Property
        public string? PROPERTY_NAME { get; set; }
        public string? LEGAL_ENTITY { get; set; }
        public string? PROJECT_ADDRESS { get; set; }
        public int? BUILDING_CLASSIFICATION { get; set; }
        public string? BUILDING_TYPE_NAME { get; set; }
        public int? BUILDING_STANDARD { get; set; }
        public string? BUILDING_STANDARD_NAME { get; set; }
        public int? STATUTORY_AUTHORITY { get; set; }
        public string? STATUTORY_AUTHORITY_NAME { get; set; }

        public string? ATTRIBUTE1 { get; set; }

        public string? ATTRIBUTE2 { get; set; }

        public string? ATTRIBUTE3 { get; set; }

        public int? CREATED_BY { get; set; }

        public int LAST_UPDATED_BY { get; set; }

        public List<PROJECT_TRL_APPROVAL_ABBR>? APPROVALS_ABBR_LIST { get; set; }

        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class ProjectApprovalDetailsNT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<ProjectApprovalDetailsOutputNT> Data { get; set; }
    }

    public class ProjectApprovalDetailsOutputNT
    {
        [JsonPropertyName("Header_Mkey")]
        public int HEADER_MKEY { get; set; }
        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }  // SEQ_NO IS TASK_NO

        [JsonPropertyName("Seq_No")]
        public string SEQ_NO { get; set; }  // 

        [JsonPropertyName("Main_Abbr")]
        public string MAIN_ABBR { get; set; }
        [JsonPropertyName("Abbr_Short_Desc")]
        public string ABBR_SHORT_DESC { get; set; }

        [JsonPropertyName("Short_Description")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("Days_Requierd")]
        public string DAYS_REQUIERD { get; set; }
        [JsonPropertyName("Authority_Department")]
        public string AUTHORITY_DEPARTMENT { get; set; }
        [JsonPropertyName("End_Result_Doc")]
        public string END_RESULT_DOC { get; set; }

        [JsonPropertyName("Job_Role")]
        public int JOB_ROLE { get; set; }
        [JsonPropertyName("Subtask_Parent_Id")]
        public int SUBTASK_PARENT_ID { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("Long_Description")]
        public string LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("Seq_Order")]
        public string SEQ_ORDER { get; set; }

        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

    }


    public class ProjectApprovalDetailsInputNT
    {
        [JsonPropertyName("Building_Type")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
}
