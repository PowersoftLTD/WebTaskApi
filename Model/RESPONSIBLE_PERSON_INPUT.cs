using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class RESPONSIBLE_PERSON_INPUT
    {
        [JsonPropertyName("DEPARTMENT_MKEY")]
        public int DEPARTMENT_MKEY { get; set; }
        [JsonPropertyName("JOB_ROLE_MKEY")]
        public int JOB_ROLE_MKEY { get; set; }
        [JsonPropertyName("USER_ID")]
        public int USER_ID { get; set; }
    }
}
