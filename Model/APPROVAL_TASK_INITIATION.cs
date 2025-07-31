using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class APPROVAL_TASK_INITIATION
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("HEADER_MKEY")]
        public int HEADER_MKEY { get; set; }

        [JsonPropertyName("TASK_NO")]
        public string? TASK_NO { get; set; }  // SEQ NO
        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("CAREGORY")]
        public int? CAREGORY { get; set; }
        [JsonPropertyName("TAGS")]
        public string? TAGS { get; set; }

        [JsonPropertyName("MAIN_ABBR")]
        public string? MAIN_ABBR { get; set; }

        [JsonPropertyName("SHORT_DESCRIPTION")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("LONG_DESCRIPTION")]
        public string? LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("AUTHORITY_DEPARTMENT")]
        public int? AUTHORITY_DEPARTMENT { get; set; }

        [JsonPropertyName("RESPOSIBLE_EMP_MKEY")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [JsonPropertyName("RESPOSIBLE_EMP_NAME")]
        public string? RESPOSIBLE_EMP_NAME { get; set; }

        [JsonPropertyName("JOB_ROLE")]
        public int? JOB_ROLE { get; set; }

        [JsonPropertyName("SANCTION_AUTHORITY")]
        public int? SANCTION_AUTHORITY { get; set; }

        [JsonPropertyName("SANCTION_DEPARTMENT")]
        public string? SANCTION_DEPARTMENT { get; set; }

        [JsonPropertyName("COMPLITION_DATE")]
        public string? COMPLITION_DATE { get; set; }
        
        [JsonPropertyName("DAYS_REQUIERD")]
        public string? DAYS_REQUIERD { get; set; }

        [JsonPropertyName("PROPERTY")]
        public int? PROPERTY { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }

        [JsonPropertyName("TENTATIVE_START_DATE")]
        public DateTime? TENTATIVE_START_DATE { get; set; }
        [JsonPropertyName("TENTATIVE_END_DATE")]
        public DateTime? TENTATIVE_END_DATE { get; set; }

        [JsonPropertyName("SUBTASK_LIST")]
        public List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();
        [JsonPropertyName("SANCTIONING_DEPARTMENT_LIST")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();
        
        [JsonPropertyName("INITIATOR")]
        public int INITIATOR { get; set; }

        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }
    }

    public class APPROVAL_TASK_INITIATION_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Header_Mkey")]
        public int HEADER_MKEY { get; set; }

        [JsonPropertyName("Task_No")]
        public string? TASK_NO { get; set; }  // SEQ NO
        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("Caregory")]
        public int? CAREGORY { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }

        [JsonPropertyName("Main_Abbr")]
        public string? MAIN_ABBR { get; set; }

        [JsonPropertyName("Short_Description")]
        public string? SHORT_DESCRIPTION { get; set; }

        [JsonPropertyName("Long_Description")]
        public string? LONG_DESCRIPTION { get; set; }

        [JsonPropertyName("Authority_Department")]
        public int? AUTHORITY_DEPARTMENT { get; set; }

        [JsonPropertyName("Resposible_Emp_Mkey")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [JsonPropertyName("Resposible_Emp_Name")]
        public string? RESPOSIBLE_EMP_NAME { get; set; }

        [JsonPropertyName("Job_Role")]
        public int? JOB_ROLE { get; set; }

        [JsonPropertyName("Sanction_Authority")]
        public int? SANCTION_AUTHORITY { get; set; }

        [JsonPropertyName("Sanction_Department")]
        public string? SANCTION_DEPARTMENT { get; set; }

        [JsonPropertyName("Complition_Date")]
        public string? COMPLITION_DATE { get; set; }

        [JsonPropertyName("Days_Requierd")]
        public string? DAYS_REQUIERD { get; set; }

        [JsonPropertyName("Property")]
        public int? PROPERTY { get; set; }

        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }

        [JsonPropertyName("Tentative_Start_Date")]
        public DateTime? TENTATIVE_START_DATE { get; set; }
        [JsonPropertyName("Tentative_End_Date")]
        public DateTime? TENTATIVE_END_DATE { get; set; }

        [JsonPropertyName("Subtask_List")]
        public List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>? SUBTASK_LIST { get; set; } = new List<APPROVAL_TASK_INITIATION_TRL_SUBTASK>();
        [JsonPropertyName("Sanctioning_Department_List")]
        public List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>? SANCTIONING_DEPARTMENT_LIST { get; set; } = new List<OUTPUT_APPROVAL_TEMPLATE_TRL_SANCTIONING_DEPARTMENT>();

        [JsonPropertyName("Initiator")]
        public int INITIATOR { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }
    }

}
