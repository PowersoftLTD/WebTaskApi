namespace TaskManagement.API.Model
{
    public class TASK_ACTION_TRL_PS
    {
        public decimal MKEY { get; set; }
        public decimal SR_NO { get; set; }
        public decimal TASK_MKEY { get; set; }
        public decimal? TASK_PARENT_ID { get; set; }
        public decimal? TASK_MAIN_NODE_ID { get; set; }

        public string ACTION_TYPE { get; set; }
        public string? DESCRIPTION_COMMENT { get; set; }

        public decimal PROGRESS_PERC { get; set; }
        public string? STATUS { get; set; }

        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }

        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }

        public decimal CREATED_BY { get; set; }
        public DateTime CREATION_DATE { get; set; }

        public decimal? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }

        public string DELETE_FLAG { get; set; }
        public string? CREATED_BYName { get; set; }
        public string? ASSIGNED_TO { get; set; }
        public string? ASSIGNED_ToName { get; set; }
        public string? RedFlag { get; set; }
        public int? Session_User_ID { get; set; }
        public int? Business_Group_ID { get; set; }
    }
    public class TASK_ACTION_TRL_PS_LIST
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<TASK_ACTION_TRL_PS> Data { get; set; }
    }

    public class Task_CommonAction_TRL
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public TASK_ACTION_TRL_PS? Data { get; set; }
    }
}
