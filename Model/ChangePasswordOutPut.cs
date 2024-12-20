using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class ChangePasswordOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        public IEnumerable<ChangePasswordOutPut> Data { get; set; }
    }
    public class ChangePasswordOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public int? COMPANY_ID { get; set; }
        [JsonPropertyName("EMP_CODE")]
        public string? EMP_CODE { get; set; }
        [JsonPropertyName("EMP_FULL_NAME")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("FIRST_NAME")]
        public string? FIRST_NAME { get; set; }
        [JsonPropertyName("LAST_NAME")]
        public string? LAST_NAME { get; set; }
        [JsonPropertyName("CURRENT_EMP_MKEY")]
        public string CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("FILTER")]
        public string? FILTER { get; set; }
        [JsonPropertyName("ROLE_ID")]
        public string? ROLE_ID { get; set; }
        [JsonPropertyName("PROJECT_ID")]
        public string? PROJECT_ID { get; set; }
        [JsonPropertyName("Date_of_birth")]
        public string? Date_of_birth { get; set; }
        [JsonPropertyName("AssignNameLike")]
        public string? AssignNameLike { get; set; }
        [JsonPropertyName("CURR_ACTION")]
        public string? CURR_ACTION { get; set; }

        [JsonPropertyName("DESIGNATION_ID")]
        public int? DESIGNATION_ID { get; set; }
        [JsonPropertyName("DEPARTMENT_ID")]
        public int? DEPARTMENT_ID { get; set; }
        [JsonPropertyName("CONTACT_NO")]
        public decimal? CONTACT_NO { get; set; }
        [JsonPropertyName("EMAIL_ID_OFFICIAL")]
        public string? EMAIL_ID_OFFICIAL { get; set; }
        [JsonPropertyName("EMAIL_ID_PERSONAL")]
        public string? EMAIL_ID_PERSONAL { get; set; }
        [JsonPropertyName("Login_ID")]
        public string? Login_ID { get; set; }
        [JsonPropertyName("LOGIN_NAME")]
        public string? LOGIN_NAME { get; set; }
        //[JsonPropertyName("LOGIN_PASSWORD")]
        //public string? LOGIN_PASSWORD { get; set; }
        [JsonPropertyName("RA1_MKEY")]
        public int? RA1_MKEY { get; set; }
        [JsonPropertyName("RA2_MKEY")]
        public int? RA2_MKEY { get; set; }
        [JsonPropertyName("EFFECTIVE_START_DATE")]
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        [JsonPropertyName("EFFECTIVE_END_DATE")]
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        [JsonPropertyName("EMAIL_FREQUENCY")]
        public string? EMAIL_FREQUENCY { get; set; }
        [JsonPropertyName("BROWSER_NOTIFICATION")]
        public string? BROWSER_NOTIFICATION { get; set; }
        [JsonPropertyName("WEB_TOKEN")]
        public string? WEB_TOKEN { get; set; }
        [JsonPropertyName("MOBILE_TOKEN")]
        public string? MOBILE_TOKEN { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("ATTRIBUTE4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("ATTRIBUTE5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("LAST_UPDATE_DATE")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("ISFORGOTPASSWORD")]
        public bool? ISFORGOTPASSWORD { get; set; }
        //[JsonPropertyName("TEMPPASSWORD")]
        //public string? TEMPPASSWORD { get; set; }
        [JsonPropertyName("BUSINESS_GROUP_ID")]
        public int? BUSINESS_GROUP_ID { get; set; }
        [JsonPropertyName("BUSINESS_GROUPS_NAME")]
        public string? BUSINESS_GROUPS_NAME { get; set; }
        [JsonPropertyName("COMPANY_NAME")]
        public string? COMPANY_NAME { get; set; }
        [JsonPropertyName("RESSET_FLAG")]
        public char? RESSET_FLAG { get; set; }
        [JsonPropertyName("LoginName")]
        public string? LoginName { get; set; }
        [JsonPropertyName("Old_Password")]
        public string? Old_Password { get; set; }
        [JsonPropertyName("New_Password")]
        public string? New_Password { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("MessageText")]
        public string? MessageText { get; set; }
        
    }
}
