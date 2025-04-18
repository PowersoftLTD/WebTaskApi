using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GET_TASK_TREEOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Table")]
        public IEnumerable<GET_TASK_TREEOutPut> Table { get; set; }
        [JsonPropertyName("Table1")]
        public IEnumerable<GET_TASK_TREEOutPut> Table1 { get; set; }
        [JsonPropertyName("Table2")]
        public IEnumerable<GET_TASK_TREEOutPut> Table2 { get; set; }
        [JsonPropertyName("Table3")]
        public IEnumerable<GET_TASK_TREEOutPut> Table3 { get; set; }
        [JsonPropertyName("Table4")]
        public IEnumerable<GET_TASK_TREEOutPut> Table4 { get; set; }
        [JsonPropertyName("Table5")]
        public IEnumerable<GET_TASK_TREEOutPut> Table5 { get; set; }
        [JsonPropertyName("Table6")]
        public IEnumerable<GET_TASK_TREEOutPut> Table6 { get; set; }
        [JsonPropertyName("Table7")]
        public IEnumerable<GET_TASK_TREEOutPut> Table7 { get; set; }
        [JsonPropertyName("Table8")]
        public IEnumerable<GET_TASK_TREEOutPut> Table8 { get; set; }

        public IEnumerable<GetTaskTreeOutPut> Data { get; set; }
    }

    public class GetTaskTeamOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<GET_TASK_DepartmentOutPut> Data { get; set; }
        [JsonPropertyName("Data1")]
        public IEnumerable<TEAM_PROGRESSOutPut> Data1 { get; set; }
    }

    public class GET_TASK_DepartmentOutPut
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public int CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("DEPARTMENT_ID")]
        public int DEPARTMENT_ID { get; set; }
        [JsonPropertyName("ERP_EMP_MKEY")]
        public int ERP_EMP_MKEY { get; set; }
        [JsonPropertyName("MEMBER_NAME")]
        public string MEMBER_NAME { get; set; }
        [JsonPropertyName("RA1_MKEY")]
        public int RA1_MKEY { get; set; }
        [JsonPropertyName("Level")]
        public int Level { get; set; }
        [JsonPropertyName("DEPTTODAY")]
        public int DEPTTODAY { get; set; }
        [JsonPropertyName("DEPTOVERDUE")]
        public int DEPTOVERDUE { get; set; }
        [JsonPropertyName("DEPTFUTURE")]
        public int DEPTFUTURE { get; set; }
        [JsonPropertyName("INTERDEPTTODAY")]
        public int INTERDEPTTODAY { get; set; }
        [JsonPropertyName("INTERDEPTOVERDUE")]
        public int INTERDEPTOVERDUE { get; set; }
        [JsonPropertyName("INTERDEPTFUTURE")]
        public int INTERDEPTFUTURE { get; set; }
    }
    public class TEAM_PROGRESSOutPut
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public int CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("DEPARTMENT_ID")]
        public int DEPARTMENT_ID { get; set; }
        [JsonPropertyName("ERP_EMP_MKEY")]
        public int ERP_EMP_MKEY { get; set; }
        [JsonPropertyName("MEMBER_NAME")]
        public string MEMBER_NAME { get; set; }
        [JsonPropertyName("RA1_MKEY")]
        public int RA1_MKEY { get; set; }
        [JsonPropertyName("Level")]
        public int Level { get; set; }
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CATEGORY")]
        public string CATEGORY { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("CREATOR")]
        public string CREATOR { get; set; }
        [JsonPropertyName("RESPONSIBLE")]
        public string RESPONSIBLE { get; set; }
        [JsonPropertyName("ACTIONABLE")]
        public string ACTIONABLE { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string COMPLETION_DATE { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("TAGS")]
        public string TAGS { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("RESPONSIBLE_TAG")]
        public string RESPONSIBLE_TAG { get; set; }
        [JsonPropertyName("ASSIGNEE")]
        public string ASSIGNEE { get; set; }
        [JsonPropertyName("ASSIGNEE_DEPARTMENT_ID")]
        public int ASSIGNEE_DEPARTMENT_ID { get; set; }
        [JsonPropertyName("TASKTYPE")]
        public string TASKTYPE { get; set; }
        [JsonPropertyName("TASKTYPE_DESC")]
        public string TASKTYPE_DESC { get; set; }
        [JsonPropertyName("PROJECT_NAME")]
        public string PROJECT_NAME { get; set; }

    }

    public class GetTaskTreeOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CATEGORY")]
        public string? CATEGORY { get; set; }

        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("CREATOR")]
        public string? CREATOR { get; set; }

        [JsonPropertyName("StatusVal")]
        public string? StatusVal { get; set; }

        [JsonPropertyName("ACTIONABLE")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string? COMPLETION_DATE { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("RESPONSIBLE_TAG")]
        public string? RESPONSIBLE_TAG { get; set; }

        [JsonPropertyName("ASSIGNEE")]
        public string? ASSIGNEE { get; set; }

        [JsonPropertyName("ASSIGNEE_DEPARTMENT_ID")]
        public int? ASSIGNEE_DEPARTMENT_ID { get; set; }

        [JsonPropertyName("PROJECT_NAME")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("TASK_HISTORY")]
        public int TASK_HISTORY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("TASKTYPE")]
        public string? TASKTYPE { get; set; }
        [JsonPropertyName("TASKTYPE_DESC")]
        public string? TASKTYPE_DESC { get; set; }
        //[JsonPropertyName("mkey")]
        //public string? mkey { get; set; }
        [JsonPropertyName("unique_id")]
        public int unique_id { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("assigned_to")]
        public int assigned_to { get; set; }
        [JsonPropertyName("resposible_emp_mkey")]
        public int resposible_emp_mkey { get; set; }
        [JsonPropertyName("status_perc")]
        public decimal status_perc { get; set; }
        [JsonPropertyName("RESPONSIBLE")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("END_DATE")]
        public string? END_DATE { get; set; }
        [JsonPropertyName("START_DATE")]
        public string START_DATE { get; set; }
        [JsonPropertyName("ACTUAL_COMPLETION_DATE")]
        public string ACTUAL_COMPLETION_DATE { get; set; }
        [JsonPropertyName("RESPONE_STATUS")]
        public string? RESPONE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }

    public class GET_TASK_TREEOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("CATEGORY")]
        public string? CATEGORY { get; set; }

        [JsonPropertyName("TASK_MKEY")]
        public string TASK_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("CREATOR")]
        public string? CREATOR { get; set; }

        [JsonPropertyName("StatusVal")]
        public string? StatusVal { get; set; }

        [JsonPropertyName("ACTIONABLE")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("COMPLETION_DATE")]
        public string? COMPLETION_DATE { get; set; }
        [JsonPropertyName("TASK_NAME")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("TASK_DESCRIPTION")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("RESPONSIBLE_TAG")]
        public string? RESPONSIBLE_TAG { get; set; }

        [JsonPropertyName("ASSIGNEE")]
        public string? ASSIGNEE { get; set; }

        [JsonPropertyName("ASSIGNEE_DEPARTMENT_ID")]
        public int? ASSIGNEE_DEPARTMENT_ID { get; set; }

        [JsonPropertyName("PROJECT_NAME")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("TASK_HISTORY")]
        public int TASK_HISTORY { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("TASKTYPE")]
        public string? TASKTYPE { get; set; }
        [JsonPropertyName("TASKTYPE_DESC")]
        public string? TASKTYPE_DESC { get; set; }
        //[JsonPropertyName("mkey")]
        //public string? mkey { get; set; }
        [JsonPropertyName("unique_id")]
        public int unique_id { get; set; }
        [JsonPropertyName("TASK_PARENT_ID")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("assigned_to")]
        public int assigned_to { get; set; }
        [JsonPropertyName("resposible_emp_mkey")]
        public int resposible_emp_mkey { get; set; }
        [JsonPropertyName("status_perc")]
        public decimal status_perc { get; set; }
        [JsonPropertyName("RESPONSIBLE")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("END_DATE")]
        public string? END_DATE { get; set; }
        [JsonPropertyName("START_DATE")]
        public string START_DATE { get; set; }
        [JsonPropertyName("ACTUAL_COMPLETION_DATE")]
        public string ACTUAL_COMPLETION_DATE { get; set; }
        [JsonPropertyName("RESPONE_STATUS")]
        public string? RESPONE_STATUS { get; set; }
        [JsonPropertyName("RESPONSE_MESSAGE")]
        public string? RESPONSE_MESSAGE { get; set; }
    }

    public class GET_TASK_TREEOutPut_List_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Table")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table { get; set; }
        [JsonPropertyName("Table1")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table1 { get; set; }
        [JsonPropertyName("Table2")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table2 { get; set; }
        [JsonPropertyName("Table3")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table3 { get; set; }
        [JsonPropertyName("Table4")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table4 { get; set; }
        [JsonPropertyName("Table5")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table5 { get; set; }
        [JsonPropertyName("Table6")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table6 { get; set; }
        [JsonPropertyName("Table7")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table7 { get; set; }
        [JsonPropertyName("Table8")]
        public IEnumerable<GET_TASK_TREEOutPut_NT> Table8 { get; set; }

        public IEnumerable<GetTaskTreeOutPut_NT> Data { get; set; }
    }

    public class GET_TASK_TREEOutPut_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Category")]
        public string? CATEGORY { get; set; }

        [JsonPropertyName("Task_Mkey")]
        public string TASK_MKEY { get; set; }

        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("Creator")]
        public string? CREATOR { get; set; }

        [JsonPropertyName("Statusval")]
        public string? StatusVal { get; set; }

        [JsonPropertyName("Actionable")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("Creation_Date")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("Completion_Date")]
        public string? COMPLETION_DATE { get; set; }
        [JsonPropertyName("Task_Name")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("Task_Description")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Responsible_Tag")]
        public string? RESPONSIBLE_TAG { get; set; }

        [JsonPropertyName("Assignee")]
        public string? ASSIGNEE { get; set; }

        [JsonPropertyName("Assignee_Department_Id")]
        public int? ASSIGNEE_DEPARTMENT_ID { get; set; }

        [JsonPropertyName("Project_Name")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("Task_History")]
        public int TASK_HISTORY { get; set; }
        [JsonPropertyName("Current_Emp_Mkey")]
        public string? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Tasktype")]
        public string? TASKTYPE { get; set; }
        [JsonPropertyName("Tasktype_Desc")]
        public string? TASKTYPE_DESC { get; set; }
        //[JsonPropertyName("mkey")]
        //public string? mkey { get; set; }
        [JsonPropertyName("Unique_Id")]
        public int unique_id { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public int TASK_PARENT_ID { get; set; }
        [JsonPropertyName("Assigned_To")]
        public int assigned_to { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int resposible_emp_mkey { get; set; }
        [JsonPropertyName("Status_Perc")]
        public decimal status_perc { get; set; }
        [JsonPropertyName("Responsible")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("End_Date")]
        public string? END_DATE { get; set; }
        [JsonPropertyName("Start_Date")]
        public string START_DATE { get; set; }
        [JsonPropertyName("Actual_Completion_Date")]
        public string ACTUAL_COMPLETION_DATE { get; set; }
        [JsonPropertyName("Respone_Status")]
        public string? RESPONE_STATUS { get; set; }
        [JsonPropertyName("Response_Message")]
        public string? RESPONSE_MESSAGE { get; set; }
    }

    public class GetTaskTreeOutPut_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Unique_Id")]
        public int unique_id { get; set; }
        [JsonPropertyName("Task_Parent_Id")]
        public int TASK_PARENT_ID { get; set; }

        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("Task_Name")]
        public string? TASK_NAME { get; set; }

        [JsonPropertyName("Task_Description")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Completion_Date")]
        public string? COMPLETION_DATE { get; set; }

        [JsonPropertyName("Assigned_To")]
        public int assigned_to { get; set; }
        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int resposible_emp_mkey { get; set; }
        [JsonPropertyName("Status_Perc")]
        public decimal status_perc { get; set; }

        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }
        [JsonPropertyName("Responsible_Tag")]
        public string? RESPONSIBLE_TAG { get; set; }

        [JsonPropertyName("Actionable")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("End_Date")]
        public string? END_DATE { get; set; }
        [JsonPropertyName("Start_Date")]
        public string START_DATE { get; set; }

        [JsonPropertyName("Actual_Completion_Date")]
        public string ACTUAL_COMPLETION_DATE { get; set; }
        [JsonPropertyName("Creator")]
        public string? CREATOR { get; set; }
        [JsonPropertyName("Progress_Perc")]
        public string? progress_perc { get; set; }

        [JsonPropertyName("Action_Type")]
        public string? ACTION_TYPE { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("File_Name")]
        public string? FILE_NAME { get; set; }

        [JsonPropertyName("File_Path")]
        public string? FILE_PATH { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Category")]
        public string? CATEGORY { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public string TASK_MKEY { get; set; }

        [JsonPropertyName("Statusval")]
        public string? StatusVal { get; set; }

        [JsonPropertyName("Creation_Date")]
        public string CREATION_DATE { get; set; }

        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Assignee")]
        public string? ASSIGNEE { get; set; }

        [JsonPropertyName("Assignee_Department_Id")]
        public int? ASSIGNEE_DEPARTMENT_ID { get; set; }

        [JsonPropertyName("Project_Name")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("Task_History")]
        public int TASK_HISTORY { get; set; }
        [JsonPropertyName("Current_Emp_Mkey")]
        public string? CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Tasktype")]
        public string? TASKTYPE { get; set; }
        [JsonPropertyName("Tasktype_Desc")]
        public string? TASKTYPE_DESC { get; set; }

        [JsonPropertyName("Responsible")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("Respone_Status")]
        public string? RESPONE_STATUS { get; set; }
        [JsonPropertyName("Response_Message")]
        public string? RESPONSE_MESSAGE { get; set; }
    }

}
