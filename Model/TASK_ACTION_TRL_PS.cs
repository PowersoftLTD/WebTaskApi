using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class TASK_ACTION_TRL_PS
    {
        [JsonPropertyName("mkey")]
        public decimal MKEY { get; set; }

        [JsonPropertyName("srNo")]
        public decimal SR_NO { get; set; }

        [JsonPropertyName("taskMkey")]
        public decimal TASK_MKEY { get; set; }

        [JsonPropertyName("taskParentId")]
        public decimal? TASK_PARENT_ID { get; set; }

        [JsonPropertyName("taskMainNodeId")]
        public decimal? TASK_MAIN_NODE_ID { get; set; }

        [JsonPropertyName("actionType")]
        public string ACTION_TYPE { get; set; }

        [JsonPropertyName("descriptionComment")]
        public string? DESCRIPTION_COMMENT { get; set; }

        [JsonPropertyName("progressPerc")]
        public decimal PROGRESS_PERC { get; set; }

        [JsonPropertyName("status")]
        public string? STATUS { get; set; }

        [JsonPropertyName("fileName")]
        public string? FILE_NAME { get; set; }

        [JsonPropertyName("filePath")]
        public string? FILE_PATH { get; set; }

        [JsonPropertyName("attribute1")]
        public string? ATTRIBUTE1 { get; set; }

        [JsonPropertyName("attribute2")]
        public string? ATTRIBUTE2 { get; set; }

        [JsonPropertyName("attribute3")]
        public string? ATTRIBUTE3 { get; set; }

        [JsonPropertyName("attribute4")]
        public string? ATTRIBUTE4 { get; set; }

        [JsonPropertyName("attribute5")]
        public string? ATTRIBUTE5 { get; set; }

        [JsonPropertyName("createdBy")]
        public decimal CREATED_BY { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CREATION_DATE { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public decimal? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        [JsonPropertyName("deleteFlag")]
        public string DELETE_FLAG { get; set; }

        [JsonPropertyName("createdByName")]
        public string? CREATED_BYName { get; set; }

        [JsonPropertyName("assignedTo")]
        public string? ASSIGNED_TO { get; set; }

        [JsonPropertyName("assignedToName")]
        public string? ASSIGNED_ToName { get; set; }

        [JsonPropertyName("readFlag")]
        public string? ReadFlag { get; set; }

        [JsonPropertyName("sessionUserId")]
        public int? Session_User_ID { get; set; }

        [JsonPropertyName("businessGroupId")]
        public int? Business_Group_ID { get; set; }

        [JsonPropertyName("notificationMessage")]
        public string? NotificationMessage { get; set; }

        [JsonPropertyName("to")]
        public string? TO { get; set; }

        [JsonPropertyName("empFullName")]
        public string? EMP_FULL_NAME { get; set; }

        [JsonPropertyName("emailIdOfficial")]
        public string? EMAIL_ID_OFFICIAL { get; set; }

        [JsonPropertyName("contactNo")]
        public string? CONTACT_NO { get; set; }
    }
    public class TASK_ACTION_TRL_PS_LIST
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<TASK_ACTION_TRL_PS> Data { get; set; }
    }

    public class Task_CommonAction_TRL
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public TASK_ACTION_TRL_PS? Data { get; set; }
    }

    public class ReadFlag_UpdateModel
    {
        public decimal Mkey { get; set; }
        public decimal SrNo { get; set; }
        public decimal TaskMkey { get; set; }

        [DefaultValue("Y")]
        public string? ReadFlag { get; set; } = "Y";

        public int? SessionUserId { get; set; }
        public int? BusinessGroupId { get; set; }
    }
}
