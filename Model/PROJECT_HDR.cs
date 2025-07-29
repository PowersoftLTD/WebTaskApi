using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{

    public class ProjectHdrNT
    {
        [JsonPropertyName("ProjectMkey")]
        public int ProjectMkey { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class PROJECT_HDR_NT_OUTPUT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<PROJECT_HDR_NT> Data { get; set; }
    }

    public class PROJECT_HDR_NT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Project_Name")]
        public int? PROJECT_NAME { get; set; }  // BUILDING_MKEY
        [JsonPropertyName("Building_Name")]
        public string? BUILDING_NAME { get; set; }
        [JsonPropertyName("Project_Abbr")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        [JsonPropertyName("Property")]
        public int? PROPERTY { get; set; }
        [JsonPropertyName("Property_Name")]
        public string? PROPERTY_NAME { get; set; }
        [JsonPropertyName("Legal_Entity")]
        public string? LEGAL_ENTITY { get; set; }
        [JsonPropertyName("Project_Address")]
        public string? PROJECT_ADDRESS { get; set; }
        [JsonPropertyName("Building_Classification")]
        public int? BUILDING_CLASSIFICATION { get; set; }
        [JsonPropertyName("Building_Type_Name")]
        public string? BUILDING_TYPE_NAME { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Building_Standard_Name")]
        public string? BUILDING_STANDARD_NAME { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Statutory_Authority_Name")]
        public string? STATUTORY_AUTHORITY_NAME { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Approvals_Abbr_List")]
        public List<PROJECT_TRL_APPROVAL_ABBR>? APPROVALS_ABBR_LIST { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
    }

    public class PROJECT_HDR_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Project_Name")]
        public int? PROJECT_NAME { get; set; }  // BUILDING_MKEY
        [JsonPropertyName("Building_Name")]
        public string? BUILDING_NAME { get; set; }
        [JsonPropertyName("Project_Abbr")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        [JsonPropertyName("Property")]
        public int? PROPERTY { get; set; }
        [JsonPropertyName("Property_Name")]
        public string? PROPERTY_NAME { get; set; }
        [JsonPropertyName("Legal_Entity")]
        public string? LEGAL_ENTITY { get; set; }
        [JsonPropertyName("Project_Address")]
        public string? PROJECT_ADDRESS { get; set; }
        [JsonPropertyName("Building_Classification")]
        public int? BUILDING_CLASSIFICATION { get; set; }
        [JsonPropertyName("Building_Type_Name")]
        public string? BUILDING_TYPE_NAME { get; set; }
        [JsonPropertyName("Building_Standard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("Building_Standard_Name")]
        public string? BUILDING_STANDARD_NAME { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("Statutory_Authority_Name")]
        public string? STATUTORY_AUTHORITY_NAME { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Approvals_Abbr_List")]
        public List<PROJECT_TRL_APPROVAL_ABBR>? APPROVALS_ABBR_LIST { get; set; }
        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
    }

    public class PROJECT_HDR
    {
        //[JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }
        //[JsonPropertyName("PROJECT_NAME")]
        public int? PROJECT_NAME { get; set; }  // BUILDING_MKEY
        public string? BUILDING_NAME { get; set; }
        //[JsonPropertyName("PROJECT_ABBR")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        //[JsonPropertyName("PROPERTY")]
        public int? PROPERTY { get; set; } // Property
        public string? PROPERTY_NAME { get; set; }
        public string? LEGAL_ENTITY { get; set; }
        public string? PROJECT_ADDRESS { get; set; }
        public int? BUILDING_CLASSIFICATION { get; set; }
        public string? BUILDING_TYPE_NAME { get; set; }
        public int? BUILDING_STANDARD { get; set; }
        public string? BUILDING_STANDARD_NAME { get; set; }
        public int? STATUTORY_AUTHORITY { get; set; }
        public string? STATUTORY_AUTHORITY_NAME { get; set; }

        public string? ATTRIBUTE1 { get; set; }

        public string? ATTRIBUTE2 { get; set; }

        public string? ATTRIBUTE3 { get; set; }

        public int? CREATED_BY { get; set; }

        public int LAST_UPDATED_BY { get; set; }

        public List<PROJECT_TRL_APPROVAL_ABBR>? APPROVALS_ABBR_LIST { get; set; }

        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
