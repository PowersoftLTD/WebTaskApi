using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class BuildingClassInput
    {
        [JsonPropertyName("Type_Code")]
        public string Type_Code { get; set; }
        [JsonPropertyName("Master_mkey")]
        public string Master_mkey { get; set; }
        
    }


    public class BuildingClassInput_NT
    {
        [JsonPropertyName("Type_Code")]
        public string Type_Code { get; set; }
        [JsonPropertyName("Master_Mkey")]
        public string Master_mkey { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

    }
}
