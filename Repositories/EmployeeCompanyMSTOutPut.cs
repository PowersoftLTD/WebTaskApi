using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Repositories
{
    public class EmployeeCompanyMSTOutPut
    {
        [JsonPropertyName("MKEY")]
        public string MKEY { get; set; }
        [JsonPropertyName("MKEY")]
        public string COMPANY_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public string EMP_CODE { get; set; }
        [JsonPropertyName("MKEY")]
        public string EMP_FULL_NAME { get; set; }
        [JsonPropertyName("MKEY")]
        public string FIRST_NAME { get; set; }
        [JsonPropertyName("MKEY")]
        public string LAST_NAME { get; set; }
        [JsonPropertyName("MKEY")]
        public string ROLE_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public string PROJECT_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public string DESIGNATION_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public string DEPARTMENT_ID { get; set; }
        [JsonPropertyName("MKEY")]
        public string CONTACT_NO { get; set; }
        [JsonPropertyName("MKEY")]
        public string EMAIL_ID_OFFICIAL { get; set; }
        [JsonPropertyName("MKEY")]
        public string EMAIL_ID_PERSONAL { get; set; }
        [JsonPropertyName("MKEY")]
        public string LOGIN_NAME { get; set; }
        [JsonPropertyName("MKEY")]
        public string LOGIN_PASSWORD { get; set; }
        [JsonPropertyName("MKEY")]
        public string RA1_MKEY { get; set; }
        [JsonPropertyName("MKEY")]
        public string RA2_MKEY { get; set; }
        [JsonPropertyName("MKEY")]
        public string EFFECTIVE_START_DATE { get; set; }
        [JsonPropertyName("MKEY")]
        public string EFFECTIVE_END_DATE { get; set; }
        [JsonPropertyName("MKEY")]
        public string EMAIL_FREQUENCY { get; set; }
        [JsonPropertyName("MKEY")]
        public string BROWSER_NOTIFICATION { get; set; }
        [JsonPropertyName("MKEY")]
        public string WEB_TOKEN { get; set; }
        [JsonPropertyName("MKEY")]
        public string MOBILE_TOKEN { get; set; }
        [JsonPropertyName("MKEY")]
        public string ATTRIBUTE1 { get; set; }
        [JsonPropertyName("MKEY")]
        public string ATTRIBUTE2 { get; set; }
        [JsonPropertyName("MKEY")]
        public string ATTRIBUTE3 { get; set; }
        [JsonPropertyName("MKEY")]
        public string ATTRIBUTE4 { get; set; }
        [JsonPropertyName("MKEY")]
        public string ATTRIBUTE5 { get; set; }
        [JsonPropertyName("MKEY")]
        public string CREATED_BY { get; set; }
        [JsonPropertyName("MKEY")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("MKEY")]
        public string LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("MKEY")]
        public string LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("MKEY")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("MKEY")]
        public string ISFORGOTPASSWORD { get; set; }
        [JsonPropertyName("MKEY")]
        public string TEMPPASSWORD { get; set; }
        [JsonPropertyName("MKEY")]
        public string BUSINESS_GROUP_ID { get; set; }
        [JsonPropertyName("BUSINESS_GROUPS_NAME")]
        public string BUSINESS_GROUPS_NAME { get; set; }
        [JsonPropertyName("COMPANY_NAME")]
        public string COMPANY_NAME { get; set; }
        [JsonPropertyName("RESSET_FLAG")]
        public string RESSET_FLAG { get; set; }
        [JsonIgnore]
        public string Status { get; set; }
        [JsonIgnore]
        public string Message { get; set; }

    }
}
