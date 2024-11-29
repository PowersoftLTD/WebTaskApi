namespace TaskManagement.API.Model
{
    public class PROJECT_TRL_APPROVAL_ABBR
    {
        public int? HEADER_MKEY { get; set; }
        public int? APPROVAL_MKEY { get; set; }  /// Seq_no is Task_no
        public string? TASK_NO { get; set; }  /// Seq_no is Task_no
        public string? APPROVAL_ABBRIVATION { get; set; }
        public string? APPROVAL_DESCRIPTION { get; set; }
        public int? DAYS_REQUIRED { get; set; }
        public int? DEPARTMENT { get; set; }
        public int? JOB_ROLE { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public string? OUTPUT_DOCUMENT { get; set; }
        public DateTime? TENTATIVE_START_DATE { get; set; }
        public DateTime? TENTATIVE_END_DATE { get; set; }
        public string? STATUS { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public int CREATED_BY { get; set; }
    }
}
