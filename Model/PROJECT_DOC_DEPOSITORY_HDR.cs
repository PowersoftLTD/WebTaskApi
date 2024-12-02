namespace TaskManagement.API.Model
{
    public class PROJECT_DOC_DEPOSITORY_HDR
    {
        public int MKEY { get; set; }
        public int? BUILDING_TYPE { get; set; }
        public int? PROPERTY_TYPE { get; set; }
        public int? DOC_NAME { get; set; }
        public string? DOC_NUMBER { get; set; }
        public string? DOC_DATE { get; set; }
        public string? DOC_ATTACHMENT { get; set; }
        public string? VALIDITY_DATE { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public int? CREATED_BY { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }

    }
}
