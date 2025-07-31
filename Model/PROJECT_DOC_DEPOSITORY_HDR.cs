using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TaskManagement.API.Model
{
    public class UpdateProjectDocDepositoryHDROutput_List
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }

        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }

        [JsonPropertyName("DATA")]
        public IEnumerable<UpdateProjectDocDepositoryHDROutput> DATA { get; set; }
    }

    public class ProjectDocDepositoryInput
    {
        [JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }

        [JsonPropertyName("USER_ID")]
        public int? USER_ID { get; set; }
    }

    public class ProjectDocDepositoryInputNT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }

        [JsonPropertyName("User_Id")]
        public int? USER_ID { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }



    public class PROJECT_DOC_DEPOSITORY_HDR
    {
        [JsonPropertyName("PROJECT_DOC_FILES")]
        public List<IFormFile>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
    }


    public class PROJECT_DOC_DEPOSITORY_HDR_NT
    {
        [JsonPropertyName("Project_Doc_Files")]
        public List<IFormFile>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }

        [JsonPropertyName("Doc_Mkey")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("Doc_Number")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("Doc_Date")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("Validity_Date")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
    }


    public class UpdateProjectDocDepositoryHDRInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_NAME")]
        public int? DOC_NAME { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("DOC_ATTACHMENT")]
        public string? DOC_ATTACHMENT { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }

        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
    }
    public class UpdateProjectDocDepositoryHDROutput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }
        public string? propertY_NAME { get; set; }
        
        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }

        public string? buildinG_NAME { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }
        [JsonPropertyName("DOC_NAME")]
        public string? DOC_NAME { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("PROJECT_DOC_FILES")]
        public IEnumerable<DocFileUploadOutPut>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("CREATED_BY_ID")]
        public int? CREATED_BY_ID { get; set; }

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

    public class DocFileUploadOutPut
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("PROJECT_DOC_MKEY")]
        public int PROJECT_DOC_MKEY { get; set; }

        [JsonPropertyName("FILE_NAME")]
        public string FILE_NAME { get; set; }

        [JsonPropertyName("FILE_PATH")]
        public string FILE_PATH { get; set; }

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

    public class UpdateProjectDocDepositoryHDROutput_List_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }

        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }

        [JsonPropertyName("Data")]
        public IEnumerable<UpdateProjectDocDepositoryNT> DATA { get; set; }
    }

    public class UpdateProjectDocDepositoryNT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Property_Mkey")]
        public int? PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Property_Name")]
        public string? propertY_NAME { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("Building_Name")]
        public string? buildinG_NAME { get; set; }

        [JsonPropertyName("Doc_Mkey")]
        public int? DOC_MKEY { get; set; }

        [JsonPropertyName("Doc_Number")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("Doc_Date")]
        public string? DOC_DATE { get; set; }
        [JsonPropertyName("Doc_Name")]
        public string? DOC_NAME { get; set; }

        [JsonPropertyName("Validity_Date")]
        public string? VALIDITY_DATE { get; set; }

        [JsonPropertyName("Project_Doc_Files")]
        public IEnumerable<DocFileUploadNT>? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("Created_By_Id")]
        public int? CREATED_BY_ID { get; set; }

        [JsonPropertyName("Created_By_Name")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("Last_Updated_By")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("Updated_By_Name")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("Last_Update_Date")]
        public string? LAST_UPDATE_DATE { get; set; }
    }

    public class DocFileUploadNT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }

        [JsonPropertyName("Project_Doc_Mkey")]
        public int PROJECT_DOC_MKEY { get; set; }

        [JsonPropertyName("File_Name")]
        public string FILE_NAME { get; set; }

        [JsonPropertyName("File_Path")]
        public string FILE_PATH { get; set; }

        [JsonPropertyName("Created_By_Id")]
        public string? CREATED_BY_ID { get; set; }

        [JsonPropertyName("Created_By_Name")]
        public string? CREATED_BY_NAME { get; set; }

        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }

        [JsonPropertyName("Last_Updated_By")]
        public string? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("Updated_By_Name")]
        public string? UPDATED_BY_NAME { get; set; }

        [JsonPropertyName("Last_Update_Date")]
        public string? LAST_UPDATE_DATE { get; set; }
    }



}
