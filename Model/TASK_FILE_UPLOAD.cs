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
}
