using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_FILE_UPLOAD
    {
        public IFormFile? files { get; set; }
        public int? Mkey { get; set; }
        public char? DELETE_FLAG { get; set; }
        public int? TASK_PARENT_ID { get; set; }
        public int? TASK_MAIN_NODE_ID { get; set; }
        public int? CREATED_BY { get; set; }
        public string? FileName { get; set; }
        public string? FILE_PATH { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
    }

    public class TASK_FILE_UPLOAD_NT
    {
        [JsonPropertyName("Files")]
        public IFormFile? files { get; set; }
        [JsonPropertyName("Mkey")]
        public int? Mkey { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public int? TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Task_Main_Node_Id")]
        public int? TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Filename")]
        public string? FileName { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
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
        [JsonPropertyName("Last_Update_Date")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
    }

    public class FileDownloadNT
    {
        [JsonPropertyName("File_Name")]
        public string File_Name { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

}
