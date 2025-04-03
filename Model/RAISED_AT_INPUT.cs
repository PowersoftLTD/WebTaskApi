using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
   

    public class RAISED_AT_INPUT
    {
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        
        [JsonPropertyName("USER_ID")]
        public int USER_ID { get; set; }
    }

    public class RAISED_AT_INPUT_NT
    {
        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("User_Id")]
        public int USER_ID { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
    public class RAISED_AT_OUTPUT_LIST
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<RAISED_AT_OUTPUT> Data { get; set; }
    }
    public class RAISED_AT_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SEQ_NO")]
        public string? SEQ_NO { get; set; }
        [JsonPropertyName("TYPE_CODE")]
        public string? TYPE_CODE { get; set; }
        [JsonPropertyName("TYPE_DESC")]
        public string? TYPE_DESC { get; set; }
        [JsonPropertyName("TYPE_ABBR")]
        public string? TYPE_ABBR { get; set; }

        [JsonPropertyName("CREATED_BY_ID")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("CREATED_BY_NAME")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("CREATION_DATE")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("LAST_UPDATED_BY")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("UPDATED_BY_NAME")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("LAST_UPDATE_DATE")]
        public string? LAST_UPDATE_DATE { get; set; }

    }

    public class RAISED_AT_OUTPUT_LIST_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<RAISED_AT_OUTPUT_NT> Data { get; set; }
    }
    public class RAISED_AT_OUTPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Seq_No")]
        public string? SEQ_NO { get; set; }
        [JsonPropertyName("Type_Code")]
        public string? TYPE_CODE { get; set; }
        [JsonPropertyName("Type_Desc")]
        public string? TYPE_DESC { get; set; }
        [JsonPropertyName("Type_Abbr")]
        public string? TYPE_ABBR { get; set; }

        [JsonPropertyName("Created_By_Id")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("Created_By_Name")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("Last_Updated_By")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("Updated_By_Name")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("Last_Update_Date")]
        public string? LAST_UPDATE_DATE { get; set; }

    }

}
