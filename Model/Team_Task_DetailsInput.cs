using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Team_Task_DetailsInput
    {
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("TASKTYPE")]
        public string TASKTYPE { get; set; }
        [JsonPropertyName("TASKTYPE_DESC")]
        public string TASKTYPE_DESC { get; set; }
        [JsonPropertyName("mKEY")]
        public string mKEY { get; set; }
    }
}
