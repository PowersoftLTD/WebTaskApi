using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_ACTIONSOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<GET_ACTIONSOutPut> Data { get; set; }
        [JsonPropertyName("Data1")]
        public IEnumerable<GET_ACTIONSOutPut> Data1 { get; set; }

    }

    public class GET_ACTIONS_TYPE_FILE
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<GetActionsListTypeDesc> Data { get; set; }
        [JsonPropertyName("Data1")]
        public IEnumerable<GetActionsListFile> Data1 { get; set; }
    }

    public class GetActionsListTypeDesc
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("TYPE_DESC")]
        public String TYPE_DESC { get; set; }
    }
    public class GetActionsListFile
    {
        [JsonPropertyName("TYPE")]
        public string? Type { get; set; }

        [JsonPropertyName("PROGRESS_PERC")]
        public decimal? PROGRESS_PERC { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string? ACTION_TYPE { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("CREATED_BY_ID")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("CREATED_BY_NAME")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("LAST_UPDATED_BY")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("UPDATED_BY_NAME")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("LAST_UPDATE_DATE")]
        public string? LAST_UPDATE_DATE { get; set; }

        //[JsonPropertyName("CREATION_DATE")]
        //public string? CREATION_DATE { get; set; }
        //[JsonPropertyName("PROGRESS_PERC")]
        //public decimal? PROGRESS_PERC { get; set; }
        //[JsonPropertyName("STATUS")]
        //public string? STATUS { get; set; }
        //[JsonPropertyName("ACTION_TYPE")]
        //public string? ACTION_TYPE { get; set; }
        //[JsonPropertyName("COMMENT")]
        //public string? COMMENT { get; set; }
        //[JsonPropertyName("FILE_NAME")]
        //public string? FILE_NAME { get; set; }
        //[JsonPropertyName("FILE_PATH")]
        //public string? FILE_PATH { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>

    public class GET_ACTIONS_TYPE_FILE_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<GetActionsListTypeDesc_NT> Data { get; set; }
        [JsonPropertyName("Data1")]
        public IEnumerable<GetActionsListFile_NT> Data1 { get; set; }
    }

    public class GetActionsListTypeDesc_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Type_Desc")]
        public String TYPE_DESC { get; set; }
    }
    public class GetActionsListFile_NT
    {
        [JsonPropertyName("Type")]
        public string? Type { get; set; }

        [JsonPropertyName("Progress_Perc")]
        public decimal? PROGRESS_PERC { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Action_Type")]
        public string? ACTION_TYPE { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }
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

    /// <summary>
    /// 
    /// </summary>



    public class GET_ACTIONSOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }
        [JsonPropertyName("PROGRESS_PERC")]
        public decimal? PROGRESS_PERC { get; set; }
        [JsonPropertyName("ACTION_TYPE")]
        public string? ACTION_TYPE { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public int? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string? CURR_ACTION { get; set; }
        [JsonPropertyName("RESPONSE_STATUS")]
        public string? RESPONSE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
