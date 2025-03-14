﻿using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_DETAILS_BY_MKEYInput
    {
        [JsonPropertyName("Mkey")]
        public string Mkey { get; set; }
        
    }

    public class TASK_DETAILS_BY_MKEYInput_NT
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

    }
}
