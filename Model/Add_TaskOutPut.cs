using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Add_TaskOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<Add_TaskOutPut> Data { get; set; }
        public TaskPostActionFileUploadAPI Data1 { get; set; }
        public TaskFileUploadAPI Data2 { get; set; }
    }

    public class Add_TaskOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("TASK_PARENT_ID")]
        public string TASK_PARENT_ID { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public string TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
    }

    //public class Add_TaskOutPut_NT
    //{
    //    [JsonPropertyName("Mkey")]
    //    public int MKEY { get; set; }

    //    [JsonPropertyName("Task_Parent_Id")]
    //    public string TASK_PARENT_ID { get; set; }
    //    [JsonPropertyName("Task_Main_Node_Id")]
    //    public string TASK_MAIN_NODE_ID { get; set; }
    //    [JsonPropertyName("Task_No")]
    //    public string TASK_NO { get; set; }
    //}

    public class Add_TaskOutPut_List_NT
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
        public TaskFileUploadAPI_NT Data2 { get; set; }
    }

    public class Add_TaskOutPut_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Task_Parent_Id")]
        public string TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public string TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }

        [JsonIgnore]
        public string Status { get; set; }
        [JsonIgnore]
        public string Message { get; set; }
    }

    public class TaskPostActionFileUploadAPI_NT
    {
        [JsonPropertyName("Files")]
        public IFormFile? files { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }

    public class TaskFileUploadAPI_NT
    {
        [JsonPropertyName("Files")]
        public IFormFile? files { get; set; }
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
        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
    }

}
