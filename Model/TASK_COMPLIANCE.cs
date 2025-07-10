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
        public int? USER_ID { get; set; }
    }


    public class TASK_COMPLIANCE_INPUT_NT
    {
        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("User_Id")]
        public int? USER_ID { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
    public class Task_Compliance_Input_NT
    {
        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("User_Id")]
        public int? USER_ID { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class TASK_SANCTIONING_INPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("LEVEL")]
        public int LEVEL { get; set; }
        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY_MKEY")]
        public string SANCTIONING_AUTHORITY_MKEY { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char DELETE_FLAG { get; set; }
    }


    public class TASK_SANCTIONING_TABLE_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        //[JsonPropertyName("Level")]
        //public int LEVEL { get; set; }
        //[JsonPropertyName("Sanctioning_Department")]
        //public string SANCTIONING_DEPARTMENT { get; set; }
        //[JsonPropertyName("Sanctioning_Authority_Mkey")]
        //public string SANCTIONING_AUTHORITY_MKEY { get; set; }
        //[JsonPropertyName("Mode")]
        //public string? Mode { get; set; }

        [JsonPropertyName("Authority_List")]
        public List<ARRAY_TASK_SANCTIONING_NT>? Authority_List { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char DELETE_FLAG { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }

    public class ARRAY_TASK_SANCTIONING_NT
    {
        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Level")]
        public int LEVEL { get; set; }
        [JsonPropertyName("Sanctioning_Department")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("Sanctioning_Authority_Mkey")]
        public string SANCTIONING_AUTHORITY_MKEY { get; set; }
        [JsonPropertyName("Mode")]
        public string Mode { get; set; }
    }

    public class TASK_SANCTIONING_MOVMENT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Sr_No")]
        public int? SR_NO { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
    }



    public class TASK_SANCTIONING_AUTHORITY_INPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("LEVEL")]
        public int LEVEL { get; set; }
        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
    }

    public class TASK_SANCTIONING_AUTHORITY_INPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Level")]
        public int? LEVEL { get; set; }
        [JsonPropertyName("Property_Mkey")]
        public int? PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("Mode")]
        public string? Mode { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
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
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

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

        [JsonIgnore]
        public string? OUT_STATUS { get; set; }
        [JsonIgnore]
        public string? OUT_MESSAGE { get; set; }
    }

    public class TASK_CHECKLIST_INPUT
    {
        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("DOC_MKEY")]
        public int DOC_MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }

        [JsonPropertyName("DOC_NAME")]
        public string? DOC_NAME { get; set; }

        [JsonPropertyName("APP_CHECK")]
        public char? APP_CHECK { get; set; }

        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }
    }
    public class TASK_CHECKLIST_CHECK_INPUT_NT
    {
        [JsonPropertyName("Task_Mkey")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("Doc_Mkey")]
        public int DOC_MKEY { get; set; }
        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("Doc_Name")]
        public string? DOC_NAME { get; set; }

        [JsonPropertyName("App_Check")]
        public char? APP_CHECK { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
    public class TaskCheckListOutputList
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_CHECKLIST_TABLE_OUTPUT> DATA { get; set; }
    }
    public class TASK_CHECKLIST_TABLE_INPUT
    {
        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("DOC_MKEY")]
        public int DOC_MKEY { get; set; }
        [JsonPropertyName("DOCUMENT_CATEGORY")]
        public int DOCUMENT_CATEGORY { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }

    }
    public class TASK_CHECKLIST_INPUT_NT
    {
        [JsonPropertyName("Task_Mkey")]
        public int? TASK_MKEY { get; set; }

        [JsonPropertyName("Check_Doc_List")]
        public Dictionary<string, string>? CHECKLIST_DOC_LST { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("Created_By")]
        public string? CREATED_BY { get; set; }
        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }
    }
    public class TaskCheckListNTOutputList
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_CHECKLIST_TABLE_NT_OUTPUT> DATA { get; set; }
    }
    public class TASK_CHECKLIST_TABLE_NT_OUTPUT
    {
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("Doc_Type_Mkey")]
        public int Doc_Type_Mkey { get; set; }

        [JsonPropertyName("Doc_Type_Name")]
        public string Doc_Type_Name { get; set; }

        [JsonPropertyName("Doc_Cat_Mkey")]
        public int Doc_Cat_Mkey { get; set; }

        [JsonPropertyName("Doc_Cat_Name")]
        public string Doc_Cat_Name { get; set; }

        //[JsonPropertyName("Document_Mkey")]
        //public int DOCUMENT_MKEY { get; set; }

        //[JsonPropertyName("Document_Category")]
        //public int DOCUMENT_CATEGORY { get; set; }

        //[JsonPropertyName("Type_Desc")]
        //public string TYPE_DESC { get; set; }

        //[JsonPropertyName("Document_Name")]
        //public string DOCUMENT_NAME { get; set; }

        [JsonPropertyName("App_Check")]
        public char APP_CHECK { get; set; }

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
        [JsonIgnore]
        public string? OUT_STATUS { get; set; }
        [JsonIgnore]
        public string? OUT_MESSAGE { get; set; }
    }
    public class TASK_CHECKLIST_TABLE_NT_INPUT
    {
        [JsonPropertyName("TASK_MKEY")]
        public int? TASK_MKEY { get; set; }
        [JsonPropertyName("DOC_MKEY")]
        public int DOC_MKEY { get; set; }
        [JsonPropertyName("DOCUMENT_CATEGORY")]
        public int DOCUMENT_CATEGORY { get; set; }
        //public Dictionary<string, object>? CHECKLIST_DOC_LST { get; set; }
        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }

    }
    public class TASK_CHECKLIST_TABLE_OUTPUT
    {
        [JsonPropertyName("TASK_MKEY")]
        public int TASK_MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOCUMENT_MKEY")]
        public int DOCUMENT_MKEY { get; set; }

        [JsonPropertyName("DOCUMENT_CATEGORY")]
        public int DOCUMENT_CATEGORY { get; set; }

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
        [JsonIgnore]
        public string? OUT_STATUS { get; set; }
        [JsonIgnore]
        public string? OUT_MESSAGE { get; set; }
    }
    public class TASK_ENDLIST_INPUT
    {
        [JsonPropertyName("PROJECT_DOC_FILES")]
        public IFormFile? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("DOC_MKEY")]
        public int DOC_MKEY { get; set; }

        [JsonPropertyName("DOC_NUMBER")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("DOC_DATE")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("VALIDITY_DATE")]
        public string? VALIDITY_DATE { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public string? DELETE_FLAG { get; set; }

        [JsonPropertyName("FILE_DELETE_FLAG")]
        public string? FILE_DELETE_FLAG { get; set; }
    }
    public class TASK_ENDLIST_INPUT_NT
    {
        [JsonPropertyName("Project_Doc_Files")]
        public IFormFile? PROJECT_DOC_FILES { get; set; }

        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("Doc_Mkey")]
        public int DOC_MKEY { get; set; }

        [JsonPropertyName("Doc_Number")]
        public string? DOC_NUMBER { get; set; }

        [JsonPropertyName("Doc_Date")]
        public string? DOC_DATE { get; set; }

        [JsonPropertyName("Validity_Date")]
        public string? VALIDITY_DATE { get; set; }
        [JsonPropertyName("Comment")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("Created_By")]
        public string? CREATED_BY { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public string? DELETE_FLAG { get; set; }

        [JsonPropertyName("File_Delete_Flag")]
        public string? FILE_DELETE_FLAG { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
    public class TASK_ENDLIST_TABLE_INPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }
        [JsonPropertyName("OUTPUT_DOC_LST")]
        public Dictionary<string, object>? OUTPUT_DOC_LST { get; set; }
        [JsonPropertyName("COMMENT")]
        public string? COMMENT { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public string? CREATED_BY { get; set; }

        [JsonPropertyName("DELETE_FLAG")]
        public string? DELETE_FLAG { get; set; }
    }
    public class TASK_OUTPUT_MEDIA
    {
        [JsonPropertyName("MKEY")]
        public int? MKEY { get; set; }
        [JsonPropertyName("SR_NO")]
        public int? SR_NO { get; set; }

        [JsonPropertyName("FILE_NAME")]
        public string? FILE_NAME { get; set; }
        [JsonPropertyName("FILE_PATH")]
        public string? FILE_PATH { get; set; }

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
    public class TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("Doc_Type_Mkey")]
        public int Doc_Type_Mkey { get; set; }

        [JsonPropertyName("Doc_Type_Name")]
        public string Doc_Type_Name { get; set; }

        [JsonPropertyName("Doc_Cat_Mkey")]
        public int Doc_Cat_Mkey { get; set; }

        [JsonPropertyName("Doc_Cat_Name")]
        public string Doc_Cat_Name { get; set; }

        //[JsonPropertyName("DOCUMENT_MKEY")]
        //public string DOCUMENT_MKEY { get; set; }

        //[JsonPropertyName("TYPE_CODE")]
        //public string TYPE_CODE { get; set; }

        //[JsonPropertyName("TYPE_DESC")]
        //public string TYPE_DESC { get; set; }

        //[JsonPropertyName("DOC_MKEY")]
        //public int? DOC_MKEY { get; set; }

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

        [JsonPropertyName("TASK_OUTPUT_ATTACHMENT")]
        public List<TASK_OUTPUT_MEDIA>? TASK_OUTPUT_ATTACHMENT { get; set; }

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

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("TYPE_DESC")]
        public string TYPE_DESC { get; set; }
        [JsonPropertyName("TYPE_CODE")]
        public string TYPE_CODE { get; set; }

        [JsonPropertyName("SANCTIONING_DEPARTMENT")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("SANCTIONING_AUTHORITY")]
        public string SANCTIONING_AUTHORITY { get; set; }

        [JsonPropertyName("SANCTIONING_AUTHORITY_MKEY")]
        public int SANCTIONING_AUTHORITY_MKEY { get; set; }

        [JsonPropertyName("LEVEL")]
        public int LEVEL { get; set; }

        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("SANCTIONING_AUTHORITY_NAME")]
        public string SANCTIONING_AUTHORITY_NAME { get; set; }

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

        [JsonIgnore]
        public string? OUT_STATUS { get; set; }
        [JsonIgnore]
        public string? OUT_MESSAGE { get; set; }
    }

    public class TASK_COMPLIANCE_CHECK_LIST_OUTPUT
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }

        [JsonPropertyName("SR_NO")]
        public int SR_NO { get; set; }

        [JsonPropertyName("PROPERTY_MKEY")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("BUILDING_MKEY")]
        public int BUILDING_MKEY { get; set; }

        //[JsonPropertyName("CHECK_DOC_LST")]
        //public Dictionary<string, object>? CHECK_DOC_LST { get; set; }

        [JsonPropertyName("DOCUMENT_MKEY")]
        public int DOCUMENT_MKEY { get; set; }

        [JsonPropertyName("DOCUMENT_CATEGORY")]
        public int DOCUMENT_CATEGORY { get; set; }

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
    public class TASK_COMPLIANCE_list
    {
        [JsonPropertyName("STATUS")]
        public string? STATUS { get; set; }
        [JsonPropertyName("MESSAGE")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("DATA")]
        public IEnumerable<TASK_COMPLIANCE_OUTPUT> DATA { get; set; }
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
        [JsonPropertyName("STATUS")]
        public string STATUS { get; set; }

        [JsonPropertyName("DISPLAY_STATUS")]
        public string DISPLAY_STATUS { get; set; }

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
    public class TASK_COMPLIANCE_list_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TASK_COMPLIANCE_OUTPUT_NT> DATA { get; set; }
    }

    public class TASK_COMPLIANCE_OUTPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        [JsonPropertyName("Short_Description")]
        public string SHORT_DESCRIPTION { get; set; }
        [JsonPropertyName("Long_Description")]
        public string LONG_DESCRIPTION { get; set; }
        [JsonPropertyName("Raised_At")]
        public string RAISED_AT { get; set; }

        [JsonPropertyName("Raised_At_Before")]
        public string RAISED_AT_BEFORE { get; set; }

        [JsonPropertyName("Responsible_Department")]
        public int RESPONSIBLE_DEPARTMENT { get; set; }

        [JsonPropertyName("Responsible_Department_Name")]
        public string RESPONSIBLE_DEPARTMENT_NAME { get; set; }

        [JsonPropertyName("Job_Role")]
        public int JOB_ROLE { get; set; }

        [JsonPropertyName("Job_Role_Name")]
        public string JOB_ROLE_NAME { get; set; }

        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Responsible_Person")]
        public int RESPONSIBLE_PERSON { get; set; }

        [JsonPropertyName("Responsible_Person_Name")]
        public string RESPONSIBLE_PERSON_NAME { get; set; }

        [JsonPropertyName("To_Be_Completed_By")]
        public DateTime TO_BE_COMPLETED_BY { get; set; }
        [JsonPropertyName("No_Days")]
        public int NO_DAYS { get; set; }
        [JsonPropertyName("Status")]
        public string STATUS { get; set; }

        [JsonPropertyName("Display_Status")]
        public string DISPLAY_STATUS { get; set; }

        [JsonPropertyName("Task_Type")]
        public int? TASK_TYPE { get; set; }

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


    public class TaskSanctioningMovmentOutputList
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TaskSanctioningMovmentOutput> DATA { get; set; }
    }

    public class TaskSanctioningMovmentOutput
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }
        [JsonPropertyName("Task_Mkey")]
        public int TASK_MKEY { get; set; }
        [JsonPropertyName("From_Sr_No")]
        public int TRLFROM_SRNO { get; set; }
        [JsonPropertyName("From_Level")]
        public int FROM_LEVEL { get; set; }
        [JsonPropertyName("From_Sanctioning_Department")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("From_Sanctioning_Authority_Mkey")]
        public int SANCTIONING_AUTHORITY_MKEY { get; set; }
        [JsonPropertyName("From_Sanctioning_Authority")]
        public string SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("From_Current_Status")]
        public string CURRENT_STATUS { get; set; }
        [JsonPropertyName("To_Sr_No")]
        public int TRLTO_SRNO { get; set; }
        [JsonPropertyName("To_Level")]
        public int TO_LEVEL { get; set; }
        [JsonPropertyName("To_Sanctioning_Department")]
        public string TO_SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("To_Sanctioning_Authority_Mkey")]
        public int TO_SANCTIONING_AUTHORITY_MKEY { get; set; }
        [JsonPropertyName("To_Sanctioning_Authority")]
        public string TO_SANCTIONING_AUTHORITY { get; set; }
        [JsonPropertyName("To_Updated_Status")]
        public string UPDATED_STATUS { get; set; }

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
        public string? OUT_STATUS { get; set; }
        [JsonIgnore]
        public string? OUT_MESSAGE { get; set; }
    }


    public class TASK_COMPLIANCE_CHECK_LIST_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TASK_COMPLIANCE_CHECK_LIST_OUTPUT_NT> DATA { get; set; }
    }

    public class TASK_COMPLIANCE_CHECK_LIST_OUTPUT_NT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }
        
        [JsonPropertyName("Doc_Type_Mkey")]
        public int Doc_Type_Mkey { get; set; }
        [JsonPropertyName("Doc_Type_Name")]
        public string Doc_Type_Name { get; set; }

        [JsonPropertyName("Doc_Cat_Mkey")]
        public int Doc_Cat_mkey { get; set; }

        [JsonPropertyName("Doc_Cat_Name")]
        public string Doc_Cat_Name { get; set; }

        [JsonPropertyName("App_Check")]
        public char APP_CHECK { get; set; }

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

    public class TASK_COMPLIANCE_END_CHECK_LIST_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TASK_COMPLIANCE_CHECK_END_LIST_OUTPUT_NT> DATA { get; set; }
    }

    public class TASK_ENDLIST_DETAILS_OUTPUT_LIST_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TASK_ENDLIST_DETAILS_OUTPUT_NT> DATA { get; set; }
    }

    public class TaskSanctioningDepartmentOutputList_NT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TaskSanctioningDepartmentOutputNT> DATA { get; set; }
    }

    public class TaskSanctioningDepartmentOutputNT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public int SR_NO { get; set; }

        [JsonPropertyName("Type_Desc")]
        public string TYPE_DESC { get; set; }
        [JsonPropertyName("Type_Code")]
        public string TYPE_CODE { get; set; }

        [JsonPropertyName("Sanctioning_Department")]
        public string SANCTIONING_DEPARTMENT { get; set; }
        [JsonPropertyName("Sanctioning_Authority")]
        public string SANCTIONING_AUTHORITY { get; set; }

        [JsonPropertyName("Sanctioning_Authority_Mkey")]
        public int SANCTIONING_AUTHORITY_MKEY { get; set; }

        [JsonPropertyName("Level")]
        public int LEVEL { get; set; }

        [JsonPropertyName("Status")]
        public string STATUS { get; set; }

        [JsonPropertyName("Mode")]
        public string MODE { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int BUILDING_MKEY { get; set; }

        [JsonPropertyName("Sanctioning_Authority_Name")]
        public string SANCTIONING_AUTHORITY_NAME { get; set; }

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
        public string? OUT_STATUS { get; set; }
        [JsonIgnore]
        public string? OUT_MESSAGE { get; set; }
    }

    public class TaskDashBoardFilterOutputListNT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("User_Filter")]
        public IEnumerable<TaskDashBoardUserFilterNT> User_Filter { get; set; }
        [JsonPropertyName("Project_Filter")]
        public IEnumerable<TaskDashBoardUserFilterNT> Project_Filter { get; set; }
    }


    public class TaskDashBoardUserFilterNT
    {
        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }

        [JsonPropertyName("Type_Code")]
        public string? TYPE_CODE { get; set; }

        [JsonPropertyName("Key")]
        public string? Key { get; set; }
        [JsonPropertyName("DisplayName")]
        public string? DisplayName { get; set; }
        [JsonPropertyName("PropertyMkey")]
        public int PropertyMkey { get; set; }

        [JsonPropertyName("PropertyName")]
        public string? PropertyName { get; set; }

        [JsonPropertyName("BuildingMkey")]
        public int? BuildingMkey { get; set; }

        [JsonPropertyName("BuilngName")]
        public string? BuilngName { get; set; }
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
    }

    public class TaskOverduePriorityOutputNT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TaskOverduePriorityNT> Data { get; set; }
    }
    public class TaskOverduePriorityNT
    {
        [JsonPropertyName("Priority")]
        public string Priority { get; set; }

        [JsonPropertyName("PriorityCount")]
        public string? PriorityCount { get; set; }
    }

    public class TaskStatusDistributionOutputNT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TaskStatusDistributionNT> Data { get; set; }
    }
    public class TaskStatusDistributionNT
    {
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        [JsonPropertyName("StatusCount")]
        public string? StatusCount { get; set; }
    }

    public class TaskProjectsDashboardOutputNT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<TaskProjectsDashboardNT> Data { get; set; }
        [JsonPropertyName("Data1")]
        public IEnumerable<TaskProjectsDashboardCountNT> Data1 { get; set; }

    }

    public class TaskProjectsDashboardNT
    {

        [JsonPropertyName("Mkey")]
        public string MKEY { get; set; }

        [JsonPropertyName("Sr_No")]
        public string Sr_No { get; set; }

        [JsonPropertyName("Task_No")]
        public string TASK_NO { get; set; }
        [JsonPropertyName("Category")]
        public string? CATEGORY { get; set; }
        [JsonPropertyName("Creator")]
        public string? CREATOR { get; set; }
        [JsonPropertyName("Responsible")]
        public string? RESPONSIBLE { get; set; }
        [JsonPropertyName("Dashboard_Status")]
        public string? Dashboard_Status { get; set; }
        [JsonPropertyName("Actionable")]
        public string? ACTIONABLE { get; set; }
        [JsonPropertyName("Creation_Date")]
        public string? CREATION_DATE { get; set; }
        [JsonPropertyName("Completion_Date")]
        public string COMPLETION_DATE { get; set; }
        [JsonPropertyName("Task_Name")]
        public string? TASK_NAME { get; set; }
        [JsonPropertyName("Task_Description")]
        public string? TASK_DESCRIPTION { get; set; }
        [JsonPropertyName("Tags")]
        public string? TAGS { get; set; }
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Responsible_Tag")]
        public string? RESPONSIBLE_TAG { get; set; }
        [JsonPropertyName("Project_Name")]
        public string? PROJECT_NAME { get; set; }
        [JsonPropertyName("Priority")]
        public string? Priority { get; set; }
        [JsonPropertyName("Progress_Percentage")]
        public string? Progress_Percentage { get; set; }
        [JsonPropertyName("Subtask_Count")]
        public string? Subtask_Count { get; set; }
        [JsonPropertyName("BuildingType")]
        public int? BUILDING_TYPE { get; set; }
        [JsonPropertyName("BuildingStandard")]
        public int? BUILDING_STANDARD { get; set; }
        [JsonPropertyName("StatutoryAuthority")]
        public int? STATUTORY_AUTHORITY { get; set; }
        [JsonPropertyName("ResponsibleDepartment")]
        public string? RESPONSIBLE_DEPARTMENT { get; set; }
        [JsonPropertyName("JobRole")]
        public string? JOB_ROLE { get; set; }
        [JsonPropertyName("ResponsiblePerson")]
        public string? RESPONSIBLE_PERSON { get; set; }
        [JsonPropertyName("ComplianceStatus")]
        public string? ComplianceSTATUS { get; set; }
        [JsonPropertyName("RaisedAt")]
        public string? RAISED_AT { get; set; }
        [JsonPropertyName("RaisedAtBefore")]
        public string? RAISED_AT_BEFORE { get; set; }

        [JsonPropertyName("Created_By")]
        public string? CREATED_BY { get; set; }
    }

    public class TaskProjectsDashboardCountNT
    {
        [JsonPropertyName("DurationFilter")]
        public string? DurationFilter { get; set; }
        [JsonPropertyName("DurationCount")]
        public int? DurationCount { get; set; }
    }
}
