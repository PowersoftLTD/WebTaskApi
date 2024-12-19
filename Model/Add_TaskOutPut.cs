using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Add_TaskOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message ")]
        public string? Message { get; set; }
        public IEnumerable<Add_TaskOutPut> Data { get; set; }
    }

    public class Add_TaskOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("TASK_PARENT_ID")]
        public string TASK_PARENT_ID { get; set; }
        [JsonPropertyName("TASK_MAIN_NODE_ID")]
        public string TASK_MAIN_NODE_ID { get; set; }
        [JsonPropertyName("TASK_NO")]
        public string TASK_NO { get; set; }
    }
}
