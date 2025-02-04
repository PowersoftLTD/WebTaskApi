using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class InsertApprovalTemplates
    {
        public int BUILDING_TYPE { get; set; }
        public int BUILDING_STANDARD { get; set; }
        public int STATUTORY_AUTHORITY { get; set; }
        public string MAIN_ABBR { get; set; }
        public string? SHORT_DESCRIPTION { get; set; }
        public string? LONG_DESCRIPTION { get; set; }
        public int? AUTHORITY_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public string? TAGS { get; set; }
        public int? DAYS_REQUIERD { get; set; }
        public string SEQ_ORDER { get; set; }
        public int CREATED_BY { get; set; }
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK>();
        [JsonPropertyName("SANCTIONING_DEPARTMENT_LIST")]
        public List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SUBTASK
    {
        public string? SEQ_NO { get; set; }
        public string? SUBTASK_ABBR { get; set; }
        public int? SUBTASK_MKEY { get; set; }
    }

    public class INSERT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
    {
        [JsonPropertyName("LEVEL")]
        public string LEVEL { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("END_DATE")]
        public DateTime? END_DATE { get; set; }
    }
}
