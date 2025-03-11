using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_DETAILS_BY_MKEY_list
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<TASK_DETAILS_BY_MKEY> Data { get; set; }
    }


    public class TASK_DETAILS_BY_MKEY
    {
        [JsonPropertyName("MKEY")]
        public string? MKEY { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string TASK_NAME { get; set; }
        [JsonPropertyName("ISNODE")]
        public char ISNODE { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public decimal TASK_PARENT_ID { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public decimal TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("STATUS_PERC")]
        public decimal STATUS_PERC { get; set; }
        [JsonPropertyName("TASK_CREATED_BY")]
        public decimal TASK_CREATED_BY { get; set; }
        [JsonPropertyName("APPROVER_ID")]
        public decimal APPROVER_ID { get; set; }
        [JsonPropertyName("APPROVE_ACTION_DATE")]
        public DateTime APPROVE_ACTION_DATE { get; set; }
        [JsonPropertyName("CAREGORY")]
        public string? CAREGORY { get; set; }
        [JsonPropertyName("PROJECT_MKEY")]
        public string PROJECT_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("PROJECT")]
        public string? PROJECT { get; set; }
        [JsonPropertyName("Sub_PROJECT")]
        public string? Sub_PROJECT { get; set; }
        [JsonPropertyName("CATEGORY_MKEY")]
        public int? CATEGORY_MKEY { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime? START_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public DateTime? COMPLETION_DATE { get; set; }
        [JsonPropertyName("CLOSE_DATE")]
        public DateTime? CLOSE_DATE { get; set; }
        [JsonPropertyName("DUE_DATE")]
        public DateTime? DUE_DATE { get; set; }
        [JsonPropertyName("ASSIGNED_TO")]
        public decimal? ASSIGNED_TO { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("EMP_FULL_NAME")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("RESPOSIBLE_EMP_MKEY")]
        public string? RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("RESPONSE_STATUS")]
        public string? RESPONSE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }

    public class TASK_DETAILS_BY_MKEY_list_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TASK_DETAILS_BY_MKEY_NT> Data { get; set; }
    }


    public class TASK_DETAILS_BY_MKEY_NT
    {
        [JsonPropertyName("Mkey")]
        public string? MKEY { get; set; }
        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("Task_Name")]
        public string TASK_NAME { get; set; }
        [JsonPropertyName("Isnode")]
        public char ISNODE { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public decimal TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public decimal TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Status")]
        public string STATUS { get; set; }
        [JsonPropertyName("Status_Perc")]
        public decimal STATUS_PERC { get; set; }
        [JsonPropertyName("Task_Created_By")]
        public decimal TASK_CREATED_BY { get; set; }
        [JsonPropertyName("Approver_Id")]
        public decimal APPROVER_ID { get; set; }
        [JsonPropertyName("Approve_Action_Date")]
        public DateTime APPROVE_ACTION_DATE { get; set; }
        [JsonPropertyName("Caregory")]
        public string? CAREGORY { get; set; }
        [JsonPropertyName("Project_Mkey")]
        public string PROJECT_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("Project")]
        public string? PROJECT { get; set; }
        [JsonPropertyName("Sub_Project")]
        public string? Sub_PROJECT { get; set; }
        [JsonPropertyName("Category_Mkey")]
        public int? CATEGORY_MKEY { get; set; }
        [JsonPropertyName("Task_Description")]
        public string TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Start_Date")]
        public DateTime? START_DATE { get; set; }
        [JsonPropertyName("Completion_Date")]
        public DateTime? COMPLETION_DATE { get; set; }
        [JsonPropertyName("Close_Date")]
        public DateTime? CLOSE_DATE { get; set; }
        [JsonPropertyName("Due_Date")]
        public DateTime? DUE_DATE { get; set; }
        [JsonPropertyName("Assigned_To")]
        public decimal? ASSIGNED_TO { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Creation_Date")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("Emp_Full_Name")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("Task_Checklist")]
        public List<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT>? tASK_CHECKLIST_TABLE_INPUT_NT { get; set; }
        [JsonPropertyName("Task_Endlist")]
        public List<TASK_ENDLIST_DETAILS_OUTPUT>? tASK_ENDLIST_TABLE_INPUT_NTs { get; set; }
        [JsonPropertyName("Task_Sanctioning")]
        public List<TaskSanctioningDepartmentOutput>? tASK_SANCTIONING_INPUT_NT { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public string? RESPOSIBLE_EMP_MKEY { get; set; }
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

    }
}
