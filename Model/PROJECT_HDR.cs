namespace TaskManagement.API.Model
{
    public class PROJECT_HDR
    {
        public int? MKEY { get; set; }
        public int? PROJECT_NAME { get; set; }
        public string? PROJECT_ABBR { get; set; }
        public int? PROPERTY { get; set; }
        public string? LEGAL_ENTITY { get; set; }
        public string? PROJECT_ADDRESS { get; set; }
        public int? BUILDING_CLASSIFICATION { get; set; }
        public int? BUILDING_STANDARD { get; set; }
        public int? STATUTORY_AUTHORITY { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public int? CREATED_BY { get; set; }
        public int LAST_UPDATED_BY { get; set; }
        public List<PROJECT_TRL_APPROVAL_ABBR>? APPROVALS_ABBR_LIST { get; set; }
    }
}
