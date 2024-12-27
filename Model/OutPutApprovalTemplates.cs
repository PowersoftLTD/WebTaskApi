using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class OutPutApprovalTemplates
    {
        public int MKEY { get; set; }
        public int BUILDING_TYPE { get; set; }
        public int BUILDING_STANDARD { get; set; }
        public int STATUTORY_AUTHORITY { get; set; }
        public string MAIN_ABBR { get; set; }
        public string SHORT_DESCRIPTION { get; set; }
        public string LONG_DESCRIPTION { get; set; }
        public int? AUTHORITY_DEPARTMENT { get; set; }
        public int? RESPOSIBLE_EMP_MKEY { get; set; }
        public int? JOB_ROLE { get; set; }
        public string? TAGS { get; set; }
        public int? DAYS_REQUIERD { get; set; }
        public Dictionary<string, object>? END_RESULT_DOC_LST { get; set; }
        public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK>();
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class OUTPUT_APPROVAL_TEMPLATE_TRL_SUBTASK
    {
        public int HEADER_MKEY { get; set; }
        public string SEQ_NO { get; set; }
        public string SUBTASK_ABBR { get; set; }
        public int SUBTASK_MKEY { get; set; }
    }

    public class OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("LEVEL")]
        public string LEVEL { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("START_DATE")]
        public DateTime START_DATE { get; set; }
        [JsonPropertyName("END_DATE")]
        public DateTime END_DATE { get; set; }
    }
}
