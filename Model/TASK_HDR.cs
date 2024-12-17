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
}
