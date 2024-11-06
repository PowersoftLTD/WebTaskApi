namespace TaskManagement.API.Model
{
    public class APPROVAL_TEMPLATE_HDR
    {
        public int? MKEY { get; set; }
        public int? BUILDING_TYPE { get; set; }
        public int? BUILDING_STANDARD { get; set; }
        public int? STATUTORY_AUTHORITY { get; set; }
        public string?   SHORT_DESCRIPTION { get; set; }
        public string? LONG_DESCRIPTION { get; set; }
        public string? ABBR { get; set; }
        public int? APPROVAL_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public int? NO_DAYS_REQUIRED { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public int? SANCTION_AUTHORITY { get; set; }
        public string? SANCTION_DEPARTMENT { get; set; }
        public string? END_RESULT_DOC { get; set; }
        public string? CHECKLIST_DOC { get; set; }
        public char? DELETE_FLAG { get; set; }
    }
}
