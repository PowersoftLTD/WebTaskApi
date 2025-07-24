using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class DocFileUploadOutput_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public DocFileUploadOutPut Data { get; set; }
    }
    public class DocFileUploadInput
    {
        public IFormFile files { get; set; }
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
    }


    public class DocTemplateGetInputNT
    {
        [JsonPropertyName("Id")]
        public int? id { get; set; }
        [JsonPropertyName("LoggedIN")]
        public int? LoggedIN { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
    public class DOC_TEMPLATE_HDR_OUTPUT_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<DOC_TEMPLATE_HDR_NT> Data { get; set; }
    }

    public class DOC_TEMPLATE_HDR_NT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Doc_Category")]
        public int? DOC_CATEGORY { get; set; }
        [JsonPropertyName("Doc_Name")]
        public string? DOC_NAME { get; set; }
        [JsonPropertyName("Doc_Abbr")]
        public string? DOC_ABBR { get; set; }
        [JsonPropertyName("Doc_Num_Field_Name")]
        public string? DOC_NUM_FIELD_NAME { get; set; }
        [JsonPropertyName("Doc_Num_Date_Name")]
        public string? DOC_NUM_DATE_NAME { get; set; }
        [JsonPropertyName("Doc_Num_App_Flag")]
        public char? DOC_NUM_APP_FLAG { get; set; }
        [JsonPropertyName("Doc_Num_Valid_Flag")]
        public char? DOC_NUM_VALID_FLAG { get; set; }
        [JsonPropertyName("Doc_Num_Date_App_Flag")]
        public char? DOC_NUM_DATE_APP_FLAG { get; set; }
        [JsonPropertyName("Doc_Attach_App_Flag")]
        public char? DOC_ATTACH_APP_FLAG { get; set; }
        [JsonPropertyName("Company_Id")]
        public int? COMPANY_ID { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        //[JsonPropertyName("Created_By")]
        //public int? CREATED_BY { get; set; }
        //[JsonPropertyName("Last_Updated_By")]
        //public int? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("Created_By_Id")]
        public int CREATED_BY_ID { get; set; }
        [JsonPropertyName("Created_By_Name")]
        public string CREATED_BY_NAME { get; set; }
        [JsonPropertyName("Creation_Date")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public string LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Updated_By_Name")]
        public string UPDATED_BY_NAME { get; set; }
        [JsonPropertyName("Last_Update_Date")]
        public string LAST_UPDATE_DATE { get; set; }

        [JsonIgnore]
        public string? Status { get; set; }
        [JsonIgnore]
        public string? Message { get; set; }
    }

    public class DOC_TEMPLATE_HDR_NT_INPUT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("Doc_Category")]
        public int? DOC_CATEGORY { get; set; }
        [JsonPropertyName("Doc_Name")]
        public string? DOC_NAME { get; set; }
        [JsonPropertyName("Doc_Abbr")]
        public string? DOC_ABBR { get; set; }
        [JsonPropertyName("Doc_Num_Field_Name")]
        public string? DOC_NUM_FIELD_NAME { get; set; }
        [JsonPropertyName("Doc_Num_Date_Name")]
        public string? DOC_NUM_DATE_NAME { get; set; }
        [JsonPropertyName("Doc_Num_App_Flag")]
        public char? DOC_NUM_APP_FLAG { get; set; }
        [JsonPropertyName("Doc_Num_Valid_Flag")]
        public char? DOC_NUM_VALID_FLAG { get; set; }
        [JsonPropertyName("Doc_Num_Date_App_Flag")]
        public char? DOC_NUM_DATE_APP_FLAG { get; set; }
        [JsonPropertyName("Doc_Attach_App_Flag")]
        public char? DOC_ATTACH_APP_FLAG { get; set; }
        [JsonPropertyName("Company_Id")]
        public int? COMPANY_ID { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public string Delete_Flag { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

    }

    public class DOC_TEMPLATE_HDR
    {
        public int? MKEY { get; set; }
        public int? DOC_CATEGORY { get; set; }
        public string? DOC_NAME { get; set; }
        public string? DOC_ABBR { get; set; }
        public string? DOC_NUM_FIELD_NAME { get; set; }
        public string? DOC_NUM_DATE_NAME { get; set; }
        public char? DOC_NUM_APP_FLAG { get; set; }
        public char? DOC_NUM_VALID_FLAG { get; set; }
        public char? DOC_NUM_DATE_APP_FLAG { get; set; }
        public char? DOC_ATTACH_APP_FLAG { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public int? CREATED_BY { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
