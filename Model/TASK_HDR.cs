using System;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_HDR
    {
        public int MKEY { get; set; }
        public string? TASK_MKEY { get; set; }
        public string? Current_task_mkey { get; set; }
        public string? TASK_NO { get; set; }
        public string? TASK_NAME { get; set; }
        public string? TASK_DESCRIPTION { get; set; }
        public decimal? CAREGORY { get; set; }
        public int? CATEGORY_MKEY { get; set; }
        public string? CATEGORY { get; set; }
        [JsonPropertyName("ProjectID")]
        public string? ProjectID { get; set; }
        [JsonPropertyName("SubProjectID")]
        public string? SubProjectID { get; set; }
        public decimal? PROJECT_ID { get; set; }
        public string? PROJECT { get; set; }
        public string? Sub_PROJECT { get; set; }
        public decimal? SUB_PROJECT_ID { get; set; }
        public decimal? SUBPROJECT_ID { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? COMPLETION_DATE { get; set; }
        public DateTime? CLOSE_DATE { get; set; }
        public DateTime? DUE_DATE { get; set; }
        public decimal? ASSIGNED_TO { get; set; }
        public string? TAGS { get; set; }
        public char? ISNODE { get; set; }
        public decimal? TASK_PARENT_ID { get; set; }
        public decimal? TASK_MAIN_NODE_ID { get; set; }
        public string? TASK_PARENT_NODE_ID { get; set; }
        public string? TASK_PARENT_NUMBER { get; set; }
        public string? STATUS { get; set; }
        public decimal? STATUS_PERC { get; set; }
        public decimal? TASK_CREATED_BY { get; set; }
        public decimal? APPROVER_ID { get; set; }
        public DateTime? APPROVE_ACTION_DATE { get; set; }
        public char? IS_ARCHIVE { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public decimal? CREATED_BY { get; set; }
        public decimal? CREATED_DATE { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public decimal? RESPOSIBLE_EMP_MKEY { get; set; }
        public char? Is_Scheduled { get; set; }
        public string? WBS { get; set; }
        public string? Duration { get; set; }
        public DateTime? Sch_Start_Date { get; set; }
        public DateTime? Finish_Date { get; set; }
        public string? Predecessors { get; set; }
        public string? Resource_Names { get; set; }
        public string? Text1 { get; set; }
        public int? Outline_Level { get; set; }
        public int? Number1 { get; set; }
        public decimal? Unique_ID { get; set; }
        public string? Percent_Complete { get; set; }
        public string? Status_Flag { get; set; }
        public int? Recursive_Id { get; set; }
        public int? Recursive_Created_For { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }

    }


    public class TASK_HDR_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public string? TASK_MKEY { get; set; }
        [JsonPropertyName("Current_Task_Mkey")]
        public string? Current_task_mkey { get; set; }
        [JsonPropertyName("Task_No")]
        public string? TASK_NO { get; set; }
        [JsonPropertyName("Task_Name")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("Task_Description")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Caregory")]
        public decimal? CAREGORY { get; set; }
        [JsonPropertyName("Category_Mkey")]
        public int? CATEGORY_MKEY { get; set; }
        [JsonPropertyName("Category")]
        public string? CATEGORY { get; set; }

        [JsonPropertyName("Projectid")]
        public string? ProjectID { get; set; }
        [JsonPropertyName("Subprojectid")]
        public string? SubProjectID { get; set; }
        [JsonPropertyName("Project_Id")]
        public decimal? PROJECT_ID { get; set; }
        [JsonPropertyName("Project")]
        public string? PROJECT { get; set; }
        [JsonPropertyName("Sub_Project")]
        public string? Sub_PROJECT { get; set; }
        [JsonPropertyName("Sub_Project_Id")]
        public decimal? SUB_PROJECT_ID { get; set; }
        [JsonPropertyName("Subproject_Id")]
        public decimal? SUBPROJECT_ID { get; set; }
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
        [JsonPropertyName("Isnode")]
        public char? ISNODE { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public decimal? TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public decimal? TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Task_Parent_Node_Id")]
        public string? TASK_PARENT_NODE_ID { get; set; }
        [JsonPropertyName("Task_Parent_Number")]
        public string? TASK_PARENT_NUMBER { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Status_Perc")]
        public decimal? STATUS_PERC { get; set; }
        [JsonPropertyName("Task_Created_By")]
        public decimal? TASK_CREATED_BY { get; set; }
        [JsonPropertyName("Approver_Id")]
        public decimal? APPROVER_ID { get; set; }
        [JsonPropertyName("Approve_Action_Date")]
        public DateTime? APPROVE_ACTION_DATE { get; set; }
        [JsonPropertyName("Is_Archive")]
        public char? IS_ARCHIVE { get; set; }
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
        public decimal? CREATED_BY { get; set; }
        [JsonPropertyName("Created_Date")]
        public decimal? CREATED_DATE { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public decimal? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public decimal? RESPOSIBLE_EMP_MKEY { get; set; }
        [JsonPropertyName("Is_Scheduled")]
        public char? Is_Scheduled { get; set; }
        [JsonPropertyName("Wbs")]
        public string? WBS { get; set; }
        [JsonPropertyName("Duration")]
        public string? Duration { get; set; }
        [JsonPropertyName("Sch_Start_Date")]
        public DateTime? Sch_Start_Date { get; set; }
        [JsonPropertyName("Finish_Date")]
        public DateTime? Finish_Date { get; set; }
        [JsonPropertyName("Predecessors")]
        public string? Predecessors { get; set; }
        [JsonPropertyName("Resource_Names")]
        public string? Resource_Names { get; set; }
        [JsonPropertyName("Text1")]
        public string? Text1 { get; set; }
        [JsonPropertyName("Outline_Level")]
        public int? Outline_Level { get; set; }
        [JsonPropertyName("Number1")]
        public int? Number1 { get; set; }
        [JsonPropertyName("Unique_ID")]
        public decimal? Unique_ID { get; set; }
        [JsonPropertyName("Percent_Complete")]
        public string? Percent_Complete { get; set; }
        [JsonPropertyName("Status_Flag")]
        public string? Status_Flag { get; set; }
        [JsonPropertyName("Recursive_Id")]
        public int? Recursive_Id { get; set; }
        [JsonPropertyName("Recursive_Created_For")]
        public int? Recursive_Created_For { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }

    }

}
