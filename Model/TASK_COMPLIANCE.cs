using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskManagement.API.Model
{
    public class TASK_COMPLIANCE_INPUT
    {
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("USER_ID")]
        public int USER_ID { get; set; }
    }

    public class TASK_SANCTIONING_AUTHORITY_INPUT
    {
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("SAUTHORITY_MKEY")]
        public int SAUTHORITY_MKEY { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("LEVEL")]
        public string LEVEL { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
    }

    public class TASK_END_LIST_DETAILS
    {
        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("DOC_MKEY")]
        public int DOC_MKEY { get; set; }
        [JsonPropertyName("USER_ID")]
        public int USER_ID { get; set; }
    }

    public class TASK_COMPLIANCE_list
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_COMPLIANCE_OUTPUT> DATA { get; set; }
    }

    public class TASK_COMPLIANCE_END_CHECK_LIST
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT> DATA { get; set; }
    }
    public class TASK_ENDLIST_DETAILS_OUTPUT_LIST
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT> DATA { get; set; }
    }

    public class TASK_ENDLIST_DETAILS_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public string DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("TASK_OUTPUT_ATTACHMENT")]
        public List<TASK_OUTPUT_MEDIA>? TASK_OUTPUT_ATTACHMENT { get; set; }

        [JsonPropertyName("DOC_NUM_APP_FLAG")]
        public string? DOC_NUM_APP_FLAG { get; set; }
        [JsonPropertyName("DOC_NUM_VALID_FLAG")]
        public string? DOC_NUM_VALID_FLAG { get; set; }
        [JsonPropertyName("DOC_NUM_DATE_APP_FLAG")]
        public string? DOC_NUM_DATE_APP_FLAG { get; set; }
        [JsonPropertyName("DOC_ATTACH_APP_FLAG")]
        public string? DOC_ATTACH_APP_FLAG { get; set; }

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

    public class TASK_CHECKLIST_INPUT
    {
        [JsonPropertyName("DOC_MKEY")]
        public int DOC_MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_NAME")]
        public string DOC_NAME { get; set; }

        [JsonPropertyName("APP_CHECK")]
        public char? APP_CHECK { get; set; }

        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }

    }

    public class TASK_ENDLIST_INPUT
    {
        //[JsonPropertyName("PROJECT_DOC_FILES")]
        //public List<IFormFile>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("PROJECT_DOC_FILES")]
        public IFormFile? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public string DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }

        [JsonPropertyName("DELETE_FLAG")]
        public string DELETE_FLAG { get; set; }
    }


    public class TASK_OUTPUT_MEDIA
    {
        [JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }

        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }

    }

    public class TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("TYPE_CODE")]
        public string TYPE_CODE { get; set; }

        [JsonPropertyName("TYPE_DESC")]
        public string TYPE_DESC { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("DOC_NUM_APP_FLAG")]
        public string? DOC_NUM_APP_FLAG { get; set; }
        [JsonPropertyName("DOC_NUM_VALID_FLAG")]
        public string? DOC_NUM_VALID_FLAG { get; set; }
        [JsonPropertyName("DOC_NUM_DATE_APP_FLAG")]
        public string? DOC_NUM_DATE_APP_FLAG { get; set; }
        [JsonPropertyName("DOC_ATTACH_APP_FLAG")]
        public string? DOC_ATTACH_APP_FLAG { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }

        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }

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
    public class TASK_COMPLIANCE_CHECK_LIST
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_COMPLIANCE_CHECK_LIST_OUTPUT> DATA { get; set; }
    }
    public class TaskSanctioningDepartmentOutputList
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TaskSanctioningDepartmentOutput> DATA { get; set; }
    }

    public class TaskSanctioningDepartmentOutput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("TYPE_DESC")]
        public string TYPE_DESC { get; set; }
        [JsonPropertyName("TYPE_CODE")]
        public string TYPE_CODE { get; set; }

        [JsonPropertyName("LEVEL")]
        public String LEVEL { get; set; }

        [JsonPropertyName("STATUS")]
        public String STATUS { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public int SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("CREATED_BY_ID")]
        public int CREATED_BY_ID { get; set; }
        [JsonPropertyName("CREATED_BY_NAME")]
        public string CREATED_BY_NAME { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public string LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("UPDATED_BY_NAME")]
        public string UPDATED_BY_NAME { get; set; }
        [JsonPropertyName("LAST_UPDATE_DATE")]
        public string LAST_UPDATE_DATE { get; set; }
    }

    public class TASK_COMPLIANCE_CHECK_LIST_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("CHECK_DOC_LST")]
        public Dictionary<string, object>? CHECK_DOC_LST { get; set; }

        [JsonPropertyName("DOCUMENT_CATEGORY")]
        public string DOCUMENT_CATEGORY { get; set; }

        [JsonPropertyName("TYPE_DESC")]
        public string TYPE_DESC { get; set; }

        [JsonPropertyName("DOCUMENT_NAME")]
        public string DOCUMENT_NAME { get; set; }

        [JsonPropertyName("APP_CHECK")]
        public char APP_CHECK { get; set; }

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


    public class TASK_COMPLIANCE_OUTPUT
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
        [JsonPropertyName("JOB_ROLE")]
        public int JOB_ROLE { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }
        [JsonPropertyName("RESPONSIBLE_PERSON")]
        public int RESPONSIBLE_PERSON { get; set; }
        [JsonPropertyName("TO_BE_COMPLETED_BY")]
        public DateTime TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("NO_DAYS")]
        public int NO_DAYS { get; set; }
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
    }
}
