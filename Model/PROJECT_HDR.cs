using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class PROJECT_HDR
    {
        //[JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }
        //[JsonPropertyName("PROJECT_NAME")]
        public int? PROJECT_NAME { get; set; }  // BUILDING_MKEY
        //[JsonPropertyName("PROJECT_ABBR")]
        public string? PROJECT_ABBR { get; set; }  // Project Abbrivation // PROJECT_ABBR
        //[JsonPropertyName("PROPERTY")]
        public int? PROPERTY { get; set; } // Property
        public string? LEGAL_ENTITY { get; set; }
        public string? PROJECT_ADDRESS { get; set; }
        public int? BUILDING_CLASSIFICATION { get; set; }
        public string BUILDING_TYPE_NAME { get; set; }
        public int? BUILDING_STANDARD { get; set; }
        public string BUILDING_STANDARD_NAME { get; set; }
        public int? STATUTORY_AUTHORITY { get; set; }
        public string STATUTORY_AUTHORITY_NAME { get; set; }

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
