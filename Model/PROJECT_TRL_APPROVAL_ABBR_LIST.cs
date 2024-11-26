namespace TaskManagement.API.Model
{
    public class PROJECT_TRL_APPROVAL_ABBR_LIST
    {
        public int HEADER_MKEY { get; set; }
        public string TASK_NO { get; set; }  // SEQ_NO IS TASK_NO
        public string MAIN_ABBR { get; set; }
        public string ABBR_SHORT_DESC { get; set; }
        public string DAYS_REQUIERD { get; set; }
        public string AUTHORITY_DEPARTMENT { get; set; }
        public string JOB_ROLE { get; set; }
        public string RESPOSIBLE_EMP_MKEY { get; set; }
        public string END_RESULT_DOC { get; set; }
    }
}
