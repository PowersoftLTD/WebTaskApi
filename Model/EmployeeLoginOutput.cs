using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class EmployeeLoginOutput_LIST
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<EmployeeLoginOutput> Data { get; set; }
    }

    public class EmployeeLoginOutput
    {

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public int? COMPANY_ID { get; set; }
        [JsonPropertyName("EMP_CODE")]
        public string? EMP_CODE { get; set; }
        [JsonPropertyName("EMP_FULL_NAME")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("FIRST_NAME")]
        public string? FIRST_NAME { get; set; }
        [JsonPropertyName("LAST_NAME")]
        public string? LAST_NAME { get; set; }
        [JsonPropertyName("ROLE_ID")]
        public string? ROLE_ID { get; set; }
        [JsonPropertyName("PROJECT_ID")]
        public string? PROJECT_ID { get; set; }
        [JsonPropertyName("DESIGNATION_ID")]
        public int? DESIGNATION_ID { get; set; }
        [JsonPropertyName("DEPARTMENT_ID")]
        public int? DEPARTMENT_ID { get; set; }
        [JsonPropertyName("CONTACT_NO")]
        public string? CONTACT_NO { get; set; }
        [JsonPropertyName("EMAIL_ID_OFFICIAL")]
        public string? EMAIL_ID_OFFICIAL { get; set; }
        [JsonPropertyName("EMAIL_ID_PERSONAL")]
        public string? EMAIL_ID_PERSONAL { get; set; }
        [JsonPropertyName("LOGIN_NAME")]
        public string? LOGIN_NAME { get; set; }
        [JsonPropertyName("LOGIN_PASSWORD")]
        public object LOGIN_PASSWORD { get; set; }
        [JsonPropertyName("RA1_MKEY")]
        public int? RA1_MKEY { get; set; }
        [JsonPropertyName("RA2_MKEY")]
        public int? RA2_MKEY { get; set; }
        [JsonPropertyName("EFFECTIVE_START_DATE")]
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        [JsonPropertyName("EFFECTIVE_END_DATE")]
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        [JsonPropertyName("EMAIL_FREQUENCY")]
        public string? EMAIL_FREQUENCY { get; set; }
        [JsonPropertyName("BROWSER_NOTIFICATION")]
        public string? BROWSER_NOTIFICATION { get; set; }
        [JsonPropertyName("WEB_TOKEN")]
        public string? WEB_TOKEN { get; set; }
        [JsonPropertyName("MOBILE_TOKEN")]
        public string? MOBILE_TOKEN { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("ATTRIBUTE4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("ATTRIBUTE5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("LAST_UPDATE_DATE")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("ISFORGOTPASSWORD")]
        public bool? ISFORGOTPASSWORD { get; set; }
        //[JsonPropertyName("TEMPPASSWORD")]
        //public object? TEMPPASSWORD { get; set; }
        [JsonPropertyName("BUSINESS_GROUP_ID")]
        public int? BUSINESS_GROUP_ID { get; set; }
        [JsonPropertyName("BUSINESS_GROUPS_NAME")]
        public string? BUSINESS_GROUPS_NAME { get; set; }
        [JsonPropertyName("COMPANY_NAME")]
        public string? COMPANY_NAME { get; set; }
        [JsonPropertyName("RESSET_FLAG")]
        public char? RESSET_FLAG { get; set; }

    }

    public class EmployeeLoginOutput_LIST_Session_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<EmployeeLoginOutput_NT> Data { get; set; }
    }

    public class EmployeeLoginOutput_NT
    {

        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public int? COMPANY_ID { get; set; }
        [JsonPropertyName("EMP_CODE")]
        public string? EMP_CODE { get; set; }
        [JsonPropertyName("EMP_FULL_NAME")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("FIRST_NAME")]
        public string? FIRST_NAME { get; set; }
        [JsonPropertyName("LAST_NAME")]
        public string? LAST_NAME { get; set; }
        [JsonPropertyName("ROLE_ID")]
        public string? ROLE_ID { get; set; }
        [JsonPropertyName("PROJECT_ID")]
        public string? PROJECT_ID { get; set; }
        [JsonPropertyName("DESIGNATION_ID")]
        public int? DESIGNATION_ID { get; set; }
        [JsonPropertyName("DEPARTMENT_ID")]
        public int? DEPARTMENT_ID { get; set; }
        [JsonPropertyName("CONTACT_NO")]
        public string? CONTACT_NO { get; set; }
        [JsonPropertyName("EMAIL_ID_OFFICIAL")]
        public string? EMAIL_ID_OFFICIAL { get; set; }
        [JsonPropertyName("EMAIL_ID_PERSONAL")]
        public string? EMAIL_ID_PERSONAL { get; set; }
        [JsonPropertyName("LOGIN_NAME")]
        public string? LOGIN_NAME { get; set; }
        [JsonPropertyName("LOGIN_PASSWORD")]
        public object LOGIN_PASSWORD { get; set; }
        [JsonPropertyName("RA1_MKEY")]
        public int? RA1_MKEY { get; set; }
        [JsonPropertyName("RA2_MKEY")]
        public int? RA2_MKEY { get; set; }
        [JsonPropertyName("EFFECTIVE_START_DATE")]
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        [JsonPropertyName("EFFECTIVE_END_DATE")]
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        [JsonPropertyName("EMAIL_FREQUENCY")]
        public string? EMAIL_FREQUENCY { get; set; }
        [JsonPropertyName("BROWSER_NOTIFICATION")]
        public string? BROWSER_NOTIFICATION { get; set; }
        [JsonPropertyName("WEB_TOKEN")]
        public string? WEB_TOKEN { get; set; }
        [JsonPropertyName("MOBILE_TOKEN")]
        public string? MOBILE_TOKEN { get; set; }
        [JsonPropertyName("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("ATTRIBUTE4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("ATTRIBUTE5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("CREATION_DATE")]
        public DateTime CREATION_DATE { get; set; }
        [JsonPropertyName("LAST_UPDATED_BY")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("LAST_UPDATE_DATE")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("ISFORGOTPASSWORD")]
        public bool? ISFORGOTPASSWORD { get; set; }
        //[JsonPropertyName("TEMPPASSWORD")]
        //public object? TEMPPASSWORD { get; set; }
        [JsonPropertyName("BUSINESS_GROUP_ID")]
        public int? BUSINESS_GROUP_ID { get; set; }
        [JsonPropertyName("BUSINESS_GROUPS_NAME")]
        public string? BUSINESS_GROUPS_NAME { get; set; }
        [JsonPropertyName("COMPANY_NAME")]
        public string? COMPANY_NAME { get; set; }
        [JsonPropertyName("RESSET_FLAG")]
        public char? RESSET_FLAG { get; set; }
    }
    
    public class EmployeeLoginOutput_LIST_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Jwttoken")]
        public string JwtToken { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<EmployeeLoginOutput_Session_NT> Data { get; set; }
    }

    public class EmployeeMobile_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Jwttoken")]
        public string JwtToken { get; set; }
    }

    public class LoginMobileEmail_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Data")]
        public IEnumerable<EmployeeLoginOutput_Session_NT> Data { get; set; }
    }

    public class EmployeeLoginOutput_Session_NT
    {

        [JsonPropertyName("Mkey")]
        public int MKEY { get; set; }
        [JsonPropertyName("Company_Id")]
        public int? COMPANY_ID { get; set; }
        [JsonPropertyName("Emp_Code")]
        public string? EMP_CODE { get; set; }
        [JsonPropertyName("Emp_Full_Name")]
        public string? EMP_FULL_NAME { get; set; }
        [JsonPropertyName("First_Name")]
        public string? FIRST_NAME { get; set; }
        [JsonPropertyName("Last_Name")]
        public string? LAST_NAME { get; set; }
        [JsonPropertyName("Role_Id")]
        public string? ROLE_ID { get; set; }
        [JsonPropertyName("Project_Id")]
        public string? PROJECT_ID { get; set; }
        [JsonPropertyName("Designation_Id")]
        public int? DESIGNATION_ID { get; set; }
        [JsonPropertyName("Department_Id")]
        public int? DEPARTMENT_ID { get; set; }
        [JsonPropertyName("Contact_No")]
        public string? CONTACT_NO { get; set; }
        [JsonPropertyName("Email_Id_Official")]
        public string? EMAIL_ID_OFFICIAL { get; set; }
        [JsonPropertyName("Email_Id_Personl")]
        public string? EMAIL_ID_PERSONAL { get; set; }
        [JsonPropertyName("Login_Name")]
        public string? LOGIN_NAME { get; set; }
        [JsonPropertyName("Login_Password")]
        public object LOGIN_PASSWORD { get; set; }
        [JsonPropertyName("Ra1_Mkey")]
        public int? RA1_MKEY { get; set; }
        [JsonPropertyName("Ra2_Mkey")]
        public int? RA2_MKEY { get; set; }
        [JsonPropertyName("Effective_Start_Date")]
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        [JsonPropertyName("Effective_End_Date")]
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        [JsonPropertyName("Email_Frequency")]
        public string? EMAIL_FREQUENCY { get; set; }
        [JsonPropertyName("Browser_Notification")]
        public string? BROWSER_NOTIFICATION { get; set; }
        [JsonPropertyName("Web_Token")]
        public string? WEB_TOKEN { get; set; }
        [JsonPropertyName("Mobile_Token")]
        public string? MOBILE_TOKEN { get; set; }
        [JsonPropertyName("Attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("Attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("Attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("Attribute4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("Attribute5")]
        public string? ATTRIBUTE5 { get; set; }

        [JsonPropertyName("Project_View")]
        public string? Project_View { get; set; }
        [JsonPropertyName("Approval_View")]
        public string? Approval_View { get; set; }
        [JsonPropertyName("Compliance_View")]
        public string? Compliance_View { get; set; }
        [JsonPropertyName("Execution_View")]
        public string? Execution_View { get; set; }

        [JsonPropertyName("Attribute10")]
        public string? ATTRIBUTE10 { get; set; }

        [JsonPropertyName("Created_By")]
        public int? CREATED_BY { get; set; }
        [JsonPropertyName("Creation_Date")]
        public DateTime CREATION_DATE { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Last_Update_Date")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("Delete_Flag")]
        public char? DELETE_FLAG { get; set; }
        [JsonPropertyName("Isforgotpassword")]
        public bool? ISFORGOTPASSWORD { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_ID { get; set; }

        [JsonPropertyName("Business_Group_Id")]
        public int? BUSINESS_GROUP_ID { get; set; }
        [JsonPropertyName("Business_Groups_Name")]
        public string? BUSINESS_GROUPS_NAME { get; set; }
        [JsonPropertyName("Company_Name")]
        public string? COMPANY_NAME { get; set; }
        [JsonPropertyName("Resset_Flag")]
        public char? RESSET_FLAG { get; set; }

    }
}
