using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class GetSubProjectInput
    {
        [JsonPropertyName("Project_Mkey")]
        public string Project_Mkey { get; set; }
    }

    public class GetSubProjectInput_NT
    {
        [JsonPropertyName("Project_Mkey")]
        public string Project_Mkey { get; set; }

        [JsonPropertyName("Session_User_ID")]
        public string Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public string Business_Group_ID { get; set; }
    }
}
