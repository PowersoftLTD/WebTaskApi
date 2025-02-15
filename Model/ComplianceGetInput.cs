using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Model
{
    public class ComplianceOutput_LIST
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<ComplianceOutPut> DATA { get; set; }
    }

    public class ComplianceOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("LONG_DESCRIPTION")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("RAISED_AT")]
        public string RAISED_AT { get; set; }

        [JsonPropertyName("RAISED_AT_BEFORE")]
        public string RAISED_AT_BEFORE { get; set; }

        [JsonPropertyName("RESPONSIBLE_DEPARTMENT")]
        public int RESPONSIBLE_DEPARTMENT { get; set; }
        [JsonPropertyName("RESPONSIBLE_DEPARTMENT_NAME")]
        public string RESPONSIBLE_DEPARTMENT_NAME { get; set; }

        [JsonPropertyName("JOB_ROLE")]
        public int JOB_ROLE { get; set; }
        [JsonPropertyName("JOB_ROLE_NAME")]
        public string JOB_ROLE_NAME { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("RESPONSIBLE_PERSON")]
        public int RESPONSIBLE_PERSON { get; set; }

        [JsonPropertyName("RESPONSIBLE_PERSON_NAME")]
        public string RESPONSIBLE_PERSON_NAME { get; set; }

        [JsonPropertyName("TO_BE_COMPLETED_BY")]
        public DateTime TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("NO_DAYS")]
        public int NO_DAYS { get; set; }
        [JsonPropertyName("DISPLAY_STATUS")]
        public string DISPLAY_STATUS { get; set; }

        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }

        [JsonPropertyName("TASK_TYPE")]
        public int? TASK_TYPE { get; set; }

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

        [JsonIgnore]
        public string? ResponseStatus { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
    }
    public class ComplianceInsertUpdateInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("LONG_DESCRIPTION")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("RAISED_AT")]
        public int? RAISED_AT { get; set; }
        [JsonPropertyName("RAISED_AT_BEFORE")]
        public int? RAISED_AT_BEFORE { get; set; }
        [JsonPropertyName("RESPONSIBLE_DEPARTMENT")]
        public int? RESPONSIBLE_DEPARTMENT { get; set; }
        [JsonPropertyName("CAREGORY")]
        public int? CAREGORY { get; set; }
        [JsonPropertyName("TASK_TYPE")]
        public int? TASK_TYPE { get; set; }
        [JsonPropertyName("JOB_ROLE")]
        public int? JOB_ROLE { get; set; }
        [JsonPropertyName("RESPONSIBLE_PERSON")]
        public int? RESPONSIBLE_PERSON { get; set; }
        [JsonPropertyName("TO_BE_COMPLETED_BY")]
        public DateTime? TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("NO_DAYS")]
        public int? NO_DAYS { get; set; }
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }

        [JsonIgnore]
        public string? Response_Status { get; set; }

        [JsonIgnore]
        public string? Response_Message { get; set; }
    }
    public class ComplianceGetInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("USER_ID")]
        public int USER_ID { get; set; }
    }
    public class ComplianceTask
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string? TASK_NO { get; set; }  // SEQ NO
    }
}
