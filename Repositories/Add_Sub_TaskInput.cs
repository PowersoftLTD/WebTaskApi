namespace TaskManagement.API.Repositories
{
    public class Add_Sub_TaskInput
    {
        public string TASK_NO { get; set; }
        public string TASK_NAME { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public string CATEGORY { get; set; }
        public string PROJECT_ID { get; set; }
        public string SUBPROJECT_ID { get; set; }
        public string COMPLETION_DATE { get; set; }
        public string ASSIGNED_TO { get; set; }
        public string TAGS { get; set; }
        public string ISNODE { get; set; }
        public string START_DATE { get; set; }
        public string CLOSE_DATE { get; set; }
        public string DUE_DATE { get; set; }
        public string TASK_PARENT_ID { get; set; }
        public string TASK_PARENT_NODE_ID { get; set; }
        public string TASK_PARENT_NUMBER { get; set; }
        public string STATUS { get; set; }
        public string STATUS_PERC { get; set; }
        public string TASK_CREATED_BY { get; set; }
        public string APPROVER_ID { get; set; }
        public string IS_ARCHIVE { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATION_DATE { get; set; }
        public string LAST_UPDATED_BY { get; set; }
        public string APPROVE_ACTION_DATE { get; set; }
        public string Current_task_mkey { get; set; }
    }
}
