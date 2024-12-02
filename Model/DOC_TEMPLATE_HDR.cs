namespace TaskManagement.API.Model
{
    public class DOC_TEMPLATE_HDR
    {
        public int? MKEY { get; set; }
        public int? DOC_CATEGORY { get; set; }
        public string? DOC_NAME { get; set; }
        public string? DOC_ABBR { get; set; }
        public string? DOC_NUM_FIELD_NAME { get; set; }
        public string? DOC_NUM_DATE_NAME { get; set; }
        public char? DOC_NUM_APP_FLAG { get; set; }
        public char? DOC_NUM_VALID_FLAG { get; set; }
        public char? DOC_NUM_DATE_APP_FLAG { get; set; }
        public char? DOC_ATTACH_APP_FLAG { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public int? CREATED_BY { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
