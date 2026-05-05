using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class ImpactAttachHdrModel
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("projectMkey")]
        public int? PROJECT_MKEY { get; set; }
        [JsonPropertyName("buildingMkey")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("fileName")]

        public string FILE_NAME { get; set; }
        [JsonPropertyName("filePath")]
        public string FILE_PATH { get; set; }

        [JsonPropertyName("attribute1")]
        public string ATTRIBUTE1 { get; set; }
        [JsonPropertyName("attribute2")]
        public string ATTRIBUTE2 { get; set; }
        [JsonPropertyName("attribute3")]
        public string ATTRIBUTE3 { get; set; }
        [JsonPropertyName("attribute4")]
        public string ATTRIBUTE4 { get; set; }
        [JsonPropertyName("attribute5")]
        public string ATTRIBUTE5 { get; set; }
        [JsonPropertyName("createdBy")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("creationDate")]
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("lastUpdatedBy")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("deleteFlag")]
        public string DELETE_FLAG { get; set; }

        [JsonPropertyName("filePathUrl")]
        public string? File_Path_Url { get; set; }
    }

    public class CommonResponseObject<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class ImpactAttachHDR_InputResponse
    {
        public int projectMkey { get; set; }
        public int buildingMkey { get; set; }
        public int sessionUserId { get; set; }
        public int businessGroupId { get; set; }
    }

    public class Ghantchart_UploadAPI_NT
    {
        [JsonPropertyName("Files")]
        public IFormFile? files { get; set; }
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("projectMkey")]
        public int? PROJECT_MKEY { get; set; }

        [JsonPropertyName("buildingMkey")]
        public int? BUILDING_MKEY { get; set; }

        //[JsonPropertyName("Task_Mkey")]
        //public int TASK_MKEY { get; set; }
        //[JsonPropertyName("Task_Main_Node_Id")]
        //public int TASK_MAIN_NODE_ID { get; set; }
        //[JsonPropertyName("Mkey")]
        //public int TASK_PARENT_ID { get; set; }
        //[JsonPropertyName("Sr_No")]
        //public int? Sr_No { get; set; }
        //[JsonPropertyName("Delete_Flag")]
        [DefaultValue("N")]
        public char? DELETE_FLAG { get; set; }
        //[JsonPropertyName("Created_By")]
       // public int CREATED_BY { get; set; }

    }

    public class GhantChart_TaskOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Mkey")]
       public int? Mkey { get; set; }
        [JsonPropertyName("filePath")]
        public string? FILE_PATH { get; set; }
    }
}
