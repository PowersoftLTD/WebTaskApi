using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Add_TaskInput
    {
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("CATEGORY")]
        public int CATEGORY { get; set; }
        [JsonPropertyName("PROJECT_ID")]
        public int PROJECT_ID { get; set; }
        [JsonPropertyName("SUBPROJECT_ID")]
        public int SUBPROJECT_ID { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string COMPLETION_DATE { get; set; }
        [JsonPropertyName("ASSIGNED_TO")]
        public string ASSIGNED_TO { get; set; }
        [JsonPropertyName("TAGS")]
        public string TAGS { get; set; }
        [JsonPropertyName("ISNODE")]
        public string ISNODE { get; set; }
        [JsonPropertyName("START_DATE")]
        public string START_DATE { get; set; }
        [JsonPropertyName("CLOSE_DATE")]
        public string CLOSE_DATE { get; set; }
        [JsonPropertyName("DUE_DATE")]
        public string DUE_DATE { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public string TASK_PARENT_ID { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("STATUS_PERC")]
        public string STATUS_PERC { get; set; }
        [JsonPropertyName("TASK_CREATED_BY")]
        public int TASK_CREATED_BY { get; set; }
        [JsonPropertyName("APPROVER_ID")]
        public int APPROVER_ID { get; set; }
        [JsonPropertyName("IS_ARCHIVE")]
        public string IS_ARCHIVE { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string ATTRIBUTE3 { get; set; }
        [JsonPropertyName("ATTRIBUTE4")]
        public string ATTRIBUTE4 { get; set; }
        [JsonPropertyName("ATTRIBUTE5")]
        public string ATTRIBUTE5 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("APPROVE_ACTION_DATE")]
        public string APPROVE_ACTION_DATE { get; set; }
    }

    public class Add_TaskInput_NT
    {
        [JsonPropertyName("Mode")]
        public string MODE { get; set; }
        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }

        [JsonPropertyName("Assign_By_Email")]
        public string? Assign_By_Email { get; set; }

        [JsonPropertyName("Created_By_Email")]
        public string? Created_By_Email { get; set; }


        [JsonPropertyName("Task_Name")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("Task_Description")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Category")]
        public int? CATEGORY { get; set; }
        [JsonPropertyName("Task_Type")]
        public int? TASK_TYPE { get; set; }
        [JsonPropertyName("Project_Id")]
        public int? PROJECT_ID { get; set; }
        [JsonPropertyName("Subproject_Id")]
        public int? SUBPROJECT_ID { get; set; }
        [JsonPropertyName("Completion_Date")]
        public string? COMPLETION_DATE { get; set; }
        [JsonPropertyName("Assigned_To")]
        public string? ASSIGNED_TO { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Isnode")]
        public string? ISNODE { get; set; }
        [JsonPropertyName("Start_Date")]
        public string? START_DATE { get; set; }
        [JsonPropertyName("Close_Date")]
        public string? CLOSE_DATE { get; set; }
        [JsonPropertyName("Due_Date")]
        public string? DUE_DATE { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public string? TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Status_Perc")]
        public string? STATUS_PERC { get; set; }
        [JsonPropertyName("Task_Created_By")]
        public int? TASK_CREATED_BY { get; set; }
        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }
        [JsonPropertyName("Tentative_Start_Date")]
        public DateTime? Tentative_Start_Date { get; set; }
        [JsonPropertyName("Tentative_End_Date")]
        public DateTime? Tentative_End_Date { get; set; }
        [JsonPropertyName("Actual_Start_Date")]
        public DateTime? Actual_Start_Date { get; set; }
        [JsonPropertyName("Actual_End_Date")]
        public DateTime? Actual_End_Date { get; set; }
        [JsonPropertyName("Approver_Id")]
        public int? APPROVER_ID { get; set; }
        [JsonPropertyName("Is_Archive")]
        public string? IS_ARCHIVE { get; set; }
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
        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Approve_Action_Date")]
        public string? APPROVE_ACTION_DATE { get; set; }
        [JsonPropertyName("Task_Checklist")]
        public List<TASK_CHECKLIST_TABLE_INPUT_NT>? tASK_CHECKLIST_TABLE_INPUT_NT { get; set; }
        [JsonPropertyName("Task_Endlist")]
        public List<TASK_ENDLIST_TABLE_INPUT_NT>? tASK_ENDLIST_TABLE_INPUT_NTs { get; set; }
        [JsonPropertyName("Task_Sanctioning")]
        public List<TASK_SANCTIONING_INPUT_NT>? tASK_SANCTIONING_INPUT_NT { get; set; }

        [JsonPropertyName("Delete_Flag")]
        public string? Delete_Flag { get; set; }

        [JsonPropertyName("Session_User_ID")]
        public int? Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int? Business_Group_ID { get; set; }
    }

    public class TASK_CHECKLIST_TABLE_INPUT_NT
    {
        [JsonPropertyName("Task_Mkey")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("Doc_Mkey")]
        public int DOC_MKEY { get; set; }
        [JsonPropertyName("Document_Category")]
        public int DOCUMENT_CATEGORY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("Created_By")]
        public string? CREATED_BY { get; set; }

    }

    public class TASK_ENDLIST_TABLE_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Output_Doc_List")]
        public Dictionary<string, object>? OUTPUT_DOC_LST { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }

        [JsonPropertyName("Created_By")]
        public string? CREATED_BY { get; set; }

        [JsonPropertyName("Delete_Flag")]
        public string? DELETE_FLAG { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

    }

    public class TASK_SANCTIONING_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Level")]
        public string LEVEL { get; set; }
        [JsonPropertyName("Sanctioning_Department")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("Sanctioning_Authority_Mkey")]
        public string SANCTIONING_AUTHORITY_MKEY { get; set; }

        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char DELETE_FLAG { get; set; }
    }
    public class ComplianceInsertUpdateInput_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("Short_Description")]
        public string SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("Long_Description")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("Raised_At")]
        public int? RAISED_AT { get; set; }
        [JsonPropertyName("Raised_At_Before")]
        public int? RAISED_AT_BEFORE { get; set; }
        [JsonPropertyName("Responsible_Department")]
        public int? RESPONSIBLE_DEPARTMENT { get; set; }
        [JsonPropertyName("Category")]
        public int? CAREGORY { get; set; }
        [JsonPropertyName("Task_Type")]
        public int? TASK_TYPE { get; set; }
        [JsonPropertyName("Job_Role")]
        public int? JOB_ROLE { get; set; }
        [JsonPropertyName("Responsible_Person")]
        public int? RESPONSIBLE_PERSON { get; set; }
        [JsonPropertyName("To_Be_Completed_By")]
        public DateTime? TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("No_Days")]
        public int? NO_DAYS { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
    }
}
