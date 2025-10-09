
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_RECURSIVE_HDR
    {
        public int MKEY { get; set; }
        public string TASK_NAME { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public int? CAREGORY { get; set; }
        public int? PROJECT_ID { get; set; }
        public int? SUB_PROJECT_ID { get; set; }
        public int? ASSIGNED_TO { get; set; }
        public string? TAGS { get; set; }
        public string? TERM { get; set; }
        public int? NO_DAYS { get; set; }
        public DateTime START_DATE { get; set; }
        public string? ENDS { get; set; }
        public DateTime? END_DATE { get; set; }
        public decimal? CREATED_BY { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public string? ATTRIBUTE6 { get; set; }
        public string? ATTRIBUTE7 { get; set; }

        public string? ATTRIBUTE8 { get; set; }

        public string? ATTRIBUTE9 { get; set; }

        public string? ATTRIBUTE10 { get; set; }

        public string? ATTRIBUTE11 { get; set; }

        public string? ATTRIBUTE12 { get; set; }
        public string? ATTRIBUTE13 { get; set; }

        public string? ATTRIBUTE14 { get; set; }

        public string? ATTRIBUTE15 { get; set; }
        public string? ATTRIBUTE16 { get; set; }

        //public string? ATTRIBUTE17 { get; set; }

        //public string? FILE_NAME { get; set; }
        //public string? FILE_PATH { get; set; }
        //public int? FILE_MKEY { get; set; }

        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }

        [JsonPropertyName("Task_Checklist")]
        public List<TASK_CHECKLIST_TABLE_INPUT_NT>? tASK_CHECKLIST_TABLE_INPUT_NT { get; set; }
        [JsonPropertyName("Task_Endlist")]
        public List<TASK_ENDLIST_TABLE_INPUT_NT>? tASK_ENDLIST_TABLE_INPUT_NTs { get; set; }

        //[JsonPropertyName("Task_Attachment")]
        //public List<Recursive_TASK_MEDIA_NT> tASK_MEDIA_NTs { get; set; }

        //[JsonPropertyName("Task_Sanctioning")]
        //public List<TaskSanctioningDepartmentOutput_NT>? tASK_SANCTIONING_INPUT_NT { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class RECURSIVE_TASK_DETAILS_BY_MKEY_list_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<Recursive_TASK_DETAILS_BY_MKEY_NT> Data { get; set; }
    }

    public class Recursive_TASK_MEDIA_NT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Sr_No")]
        public int? SR_NO { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int? TASK_MKEY { get; set; }

        //[JsonPropertyName("Task_Parent_Id")]
        //public int? Task_Parent_Id { get; set; }

        //[JsonPropertyName("Task_Main_Node_Id")]
        //public int? Task_Main_Node_Id { get; set; }

        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }

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

    public class Recursive_TASK_DETAILS_BY_MKEY_NT
    {
        public int MKEY { get; set; }
        public string TASK_NAME { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public int? CAREGORY { get; set; }
        public int? PROJECT_ID { get; set; }
        public int? SUB_PROJECT_ID { get; set; }
        public int? ASSIGNED_TO { get; set; }
        public string? TAGS { get; set; }
        public string? TERM { get; set; }
        public int? NO_DAYS { get; set; }
        public DateTime START_DATE { get; set; }
        public string? ENDS { get; set; }
        public DateTime? END_DATE { get; set; }
        public decimal? CREATED_BY { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public string? ATTRIBUTE6 { get; set; }
        public string? ATTRIBUTE7 { get; set; }

        public string? ATTRIBUTE8 { get; set; }

        public string? ATTRIBUTE9 { get; set; }

        public string? ATTRIBUTE10 { get; set; }

        public string? ATTRIBUTE11 { get; set; }

        public string? ATTRIBUTE12 { get; set; }
        public string? ATTRIBUTE13 { get; set; }

        public string? ATTRIBUTE14 { get; set; }

        public string? ATTRIBUTE15 { get; set; }
        public string? ATTRIBUTE16 { get; set; }

        public string? ATTRIBUTE17 { get; set; }

        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }
        public int? FILE_MKEY { get; set; }
        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }

        [JsonPropertyName("Task_Checklist")]
        public List<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT_NT>? tASK_CHECKLIST_TABLE_INPUT_NT { get; set; }
        [JsonPropertyName("Task_Endlist")]
        public List<TASK_ENDLIST_DETAILS_OUTPUT_NT>? tASK_ENDLIST_TABLE_INPUT_NTs { get; set; }

        [JsonPropertyName("Task_Attachment")]
        public List<Recursive_TASK_MEDIA_NT> tASK_MEDIA_NTs { get; set; }

        [JsonPropertyName("Task_Sanctioning")]
        public List<TaskSanctioningDepartmentOutput_NT>? tASK_SANCTIONING_INPUT_NT { get; set; }


        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class RecursiveTaskFileUploadAPI_NT
    {
        [JsonPropertyName("Files")]
        public List<IFormFile>? files { get; set; }
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Mkey")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Sr_No")]
        public int? Sr_No { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }

    }

    public class Add_RecursiveTaskOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<Add_TaskOutPut_NT> Data { get; set; }
        [JsonPropertyName("Data1")]
        public TaskPostActionFileUploadAPI_NT Data1 { get; set; }
        [JsonPropertyName("Data2")]
        public RecursiveTaskFileUploadAPI_NT Data2 { get; set; }
    }

    public class Add_Recursive_TaskOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public TASK_RECURSIVE_HDR Data { get; set; }
        [JsonPropertyName("Data1")]
        public TaskPostActionFileUploadAPI_NT Data1 { get; set; }
        [JsonPropertyName("Data2")]
        public TaskFileUploadAPI_NT Data2 { get; set; }
    }

    public class Add_Recursive_TaskOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public TASK_RECURSIVE_HDR Data { get; set; }
        public TaskPostActionFileUploadAPI Data1 { get; set; }
        public TaskFileUploadAPI Data2 { get; set; }
    }
}
