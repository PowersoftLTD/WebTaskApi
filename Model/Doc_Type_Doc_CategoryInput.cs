using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Doc_Type_Doc_CategoryInput
    {
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
}
