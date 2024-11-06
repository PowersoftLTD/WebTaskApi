namespace TaskManagement.API.Model
{
    public class FileUploadAPI
    {
        public IFormFile? files { get; set; }
        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }
        public int? FILE_MKEY { get; set; }
        public int? FILE_SR_NO { get; set; }
        public int? TASK_MKEY { get; set; }
        public int? CREATED_BY { get; set; }
        public string? ATTRIBUTE14 { get; set; }

        public string? ATTRIBUTE15 { get; set; }
        public string? ATTRIBUTE16 { get; set; }
       
        
            
    }
}
