namespace TaskManagement.API.Model
{
    public class PROJECT_APPROVAL_DETAILS_OUTPUT
    {
        public int HEADER_MKEY { get; set; }
        public string SUBTASK_PARENT_ID { get; set; }
        public string TASK_NO { get; set; }
        public string MAIN_ABBR { get; set; }
        public string ABBR_SHORT_DESC { get; set; }
        public string LONG_DESCRIPTION { get; set; }
        public string DAYS_REQUIERD { get; set; }
        public string AUTHORITY_DEPARTMENT { get; set; }
        public int JOB_ROLE { get; set; }
        public int RESPOSIBLE_EMP_MKEY { get; set; }
        public string END_RESULT_DOC { get; set; }

    }
}
