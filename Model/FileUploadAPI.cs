using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class FileUploadAPI
    {

        public IFormFile? files { get; set; }
        [JsonIgnore]
        public string? FILE_NAME { get; set; }
        [JsonIgnore]
        public string? FILE_PATH { get; set; }
        //public int? FILE_MKEY { get; set; }
        //public int? FILE_SR_NO { get; set; }
        public int? TASK_MKEY { get; set; }
        public int? CREATED_BY { get; set; }
        public string? DELETE_FLAG { get; set; }
        //public string? ATTRIBUTE15 { get; set; }
        //public string? ATTRIBUTE16 { get; set; }
    }

    public class FileUploadAPIOutPut
    {
        public int MKEY { get; set; }
        public int SR_NO { get; set; }
        public int TASK_MKEY { get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_PATH { get; set; }
        public int CREATED_BY { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public char DELETE_FLAG { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class TaskFileUploadAPI
    {
        public IFormFile? files { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
    }


    public class TaskFileUploadAInput
    {
        public IFormFile files { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
    }
    public class FileSettings
    {
        public string FilePath { get; set; }
    }

    public class HostEnvironment
    {
        public string env { get; set; }
    }

    public class TaskPostActionFileUploadAPIOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<TaskPostActionFileUploadAPIOutPut> Data { get; set; }

    }

    public class TaskPostActionFileUploadAPIOutPut
    {
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }
    /// <summary>
    /// ///////////////////////
    /// 
    /// 
    /// 
    /// </summary>
    public class TaskPostActionFileUploadAPIOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<TaskPostActionFileUploadAPIOutPut_NT> Data { get; set; }

    }

    public class TaskPostActionFileUploadAPIOutPut_NT
    {
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }



    public class TaskPostActionFileUploadAPI
    {
        public IFormFile? files { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }

    public class TaskPostActionFileUploadAPI_NT
    {
        [JsonPropertyName("Files")]
        public IFormFile? files { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }

    public class TaskPostActionAPIOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public TaskPostActionOutput Data { get; set; }

    }


    public class TaskPostActionOutput
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string ACTION_TYPE { get; set; }
        [JsonPropertyName("DESCRIPTION_COMMENT")]
        public string DESCRIPTION_COMMENT { get; set; }
        [JsonPropertyName("PROGRESS_PERC")]
        public string PROGRESS_PERC { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }


    public class TaskPostActionAPIOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<TaskPostActionOutput_NT> Data { get; set; }
    }


    public class TaskPostActionOutput_NT
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("Action_Type")]
        public string ACTION_TYPE { get; set; }
        [JsonPropertyName("Description_Comment")]
        public string DESCRIPTION_COMMENT { get; set; }
        [JsonPropertyName("Progress_Perc")]
        public string PROGRESS_PERC { get; set; }
        [JsonPropertyName("Status")]
        public string STATUS { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int TASK_MAIN_NODE_ID { get; set; }

        [JsonIgnore]
        public string ResponseStatus { get; set; }
        [JsonIgnore]
        public string StatusMessage { get; set; }

    }

    public class TaskPostActionInput
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string ACTION_TYPE { get; set; }
        [JsonPropertyName("DESCRIPTION_COMMENT")]
        public string DESCRIPTION_COMMENT { get; set; }
        [JsonPropertyName("PROGRESS_PERC")]
        public string PROGRESS_PERC { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public int TASK_MAIN_NODE_ID { get; set; }
    }


    public class TaskPostActionInput_NT
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("Action_Type")]
        public string ACTION_TYPE { get; set; }
        [JsonPropertyName("Description_Comment")]
        public string DESCRIPTION_COMMENT { get; set; }
        [JsonPropertyName("Progress_Perc")]
        public string PROGRESS_PERC { get; set; }
        [JsonPropertyName("Status")]
        public string STATUS { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }
}
