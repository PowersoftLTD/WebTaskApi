namespace TaskManagement.API.Model
{
    public class APPROVAL_TEMPLATE_HDR
    {
        public int MKEY { get; set; }
        public int BUILDING_TYPE { get; set; }
        public int BUILDING_STANDARD { get; set; }
        public int STATUTORY_AUTHORITY { get; set; }
        public string MAIN_ABBR { get; set; }
        public string? SHORT_DESCRIPTION { get; set; }
        public string? LONG_DESCRIPTION { get; set; }
        public int? AUTHORITY_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public int? DAYS_REQUIERD { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public int? CREATED_BY { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public int? SANCTION_AUTHORITY { get; set; }
        public string? SANCTION_DEPARTMENT { get; set; }
        public string? END_RESULT_DOC { get; set; }
        public string? CHECKLIST_DOC { get; set; }
        public string? ABBR_SHORT_DESC { get; set; }
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        public List<APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TEMPLATE_TRL_SUBTASK>();
    }
}
