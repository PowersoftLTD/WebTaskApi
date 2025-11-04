using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class UserLocationInfo
    {
        public string? Ip { get; set; }
        public string? Hostname { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? Loc { get; set; }
        public string? Org { get; set; }
        public string? Postal { get; set; }
        public string? Timezone { get; set; }
        public string? Readme { get; set; }
        public string? CREATED_BY { get; set; }
        public string? CREATION_DATE { get; set; }
        public string? LAST_UPDATED_BY { get; set; }
        public string? LAST_UPDATE_DATE { get; set; }
        public string? DELETE_FLAG { get; set; }
    }
    public class User_Audit
    {
        public int MKEY { get; set; }
        public string User_Id { get; set; }
        public string User_IP { get; set; }
        public string User_Location { get; set; }
        public string Activity { get; set; }
        public string ATTRIBUTE1 { get; set; }
        public string ATTRIBUTE2 { get; set; }
        public string ATTRIBUTE3 { get; set; }
        public string ATTRIBUTE4 { get; set; }
        public string ATTRIBUTE5 { get; set; }
        public string ATTRIBUTE6 { get; set; }
        public string ATTRIBUTE7 { get; set; }
        public string ATTRIBUTE8 { get; set; }
        public string? CREATED_BY { get; set; }
        public string CREATION_DATE { get; set; }
        public string? LAST_UPDATED_BY { get; set; }
        public string? LAST_UPDATE_DATE { get; set; }
        public string DELETE_FLAG { get; set; }
    }
    public class SessionLogOut
    {
        public string Session_UserId { get; set; }
        public string? BusinessGroupId { get; set; }
    }

    public class LogOutoutPut_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Data")]
        public User_Audit Data { get; set; }
    }
}
