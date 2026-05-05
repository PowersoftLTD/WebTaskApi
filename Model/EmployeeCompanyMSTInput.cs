using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class EmployeeCompanyMSTInput
    {
        [JsonPropertyName("Login_ID")]
        public string Login_ID { get; set; }
        [JsonPropertyName("Login_Password")]
        public string Login_Password { get; set; }
    }

    public class EmployeeMobileMSTInput
    {
        [JsonPropertyName("Login_ID")]
        public string Login_ID { get; set; }
    }
    public class EmployeeMobileMSTInput_NT
    {
        [JsonPropertyName("Login_Name")]
        public string Login_ID { get; set; }
    }

    public class EmployeeCompanyMSTInput_NT
    {
        [JsonPropertyName("Login_Name")]
        public string Login_ID { get; set; }
        [JsonPropertyName("Login_Password")]
        public string Login_Password { get; set; }
    }


    //Model Created by Itemad Hyder 14-04-2026

    public class GlobalSearchInput
    {
        [JsonPropertyName("SearchText")]
        public string? SearchText { get; set; }
        [JsonPropertyName("session_User_ID")]
        public int? session_User_ID { get; set; }
        [JsonPropertyName("business_Group_ID")]
        public int? BusinessGroupId { get; set; }
    }

    public class BusinessGroup
    {
        [JsonPropertyName("BUSINESS_GROUP_ID")]
        public int? BusinessGroupId { get; set; }
        [JsonPropertyName("Business_Group_Name")]
        public string? BusinessGroupName { get; set; }
        [JsonPropertyName("Companies")]
        public List<BusinessGroupName> Companies { get; set; }
    }

    public class BusinessGroupName
    {
        [JsonPropertyName("COMPANY_ID")]
        public int? CompanyId { get; set; }
        [JsonPropertyName("Company_name")]
        public string? Company_Name { get; set; }
    }

    public class BusinessGroupFlat
    {
        [JsonPropertyName("businessGroupId")]
        public int? BUSINESS_GROUP_ID { get; set; }

        [JsonPropertyName("businessGroupName")]
        public string? Business_Group_Name { get; set; }

        [JsonPropertyName("companyId")]
        public int? COMPANY_ID { get; set; }

        [JsonPropertyName("companyName")]
        public string? Company_Name { get; set; }
    }

    public class BusinessGroup_OutPutResponse
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        [JsonPropertyName("Data")]
        public dynamic Data { get; set; }
    }


    public class UserManagement_Model
    {
        public personalInformation personalInformation { get; set; }
        public List<Business_Group> businessGroup { get; set; }

        public List<RoleManagement> roleManagement { get; set; }

        public int? session_User_ID { get; set; }
        public int? BusinessGroupId { get; set; }
    }

    public class personalInformation
    {

        public decimal Mkey { get; set; }
        public string? FullName { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? EMAIL_ID_OFFICIAL { get; set; }
        public string? EMAIL_ID_PERSONAL { get; set; }
        public string? Mobile_No { get; set; }
        public string? EmployeeId { get; set; }
        public string? UserName { get; set; }
        public string? Status { get; set; }
        public string? EFFECTIVE_START_DATE { get; set; }

    }
    public class Business_Group
    {
        public string BusinessGroup_Name { get; set;}
    }

    public class RoleManagement
    {
        public int? Role { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? ReportingManager { get; set; }
        public string? LastLogin { get; set; }
        public string? Remarks { get; set; }
        public string? Delete_flag { get; set; }
    }

    public class ResponseObject<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }


    public class Employee_MST_Details_Model 
    {
        // Primary Key
        [JsonPropertyName("mkey")]
        public decimal Mkey { get; set; }

        // Basic Information
        [JsonPropertyName("companyId")]
        public decimal CompanyId { get; set; }

        [JsonPropertyName("empCode")]
        public string EmpCode { get; set; }

        [JsonPropertyName("empFullName")]
        public string EmpFullName { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        // Role and Project
        [JsonPropertyName("roleId")]
        public string RoleId { get; set; }

        [JsonPropertyName("projectId")]
        public string ProjectId { get; set; }

        // Job Information
        [JsonPropertyName("designationId")]
        public decimal? DesignationId { get; set; }

        [JsonPropertyName("departmentId")]
        public decimal? DepartmentId { get; set; }

        // Contact Info
        [JsonPropertyName("contactNo")]
        public decimal ContactNo { get; set; }

        [JsonPropertyName("emailIdOfficial")]
        public string EmailIdOfficial { get; set; }

        [JsonPropertyName("emailIdPersonal")]
        public string EmailIdPersonal { get; set; }

        // Login Info
        [JsonPropertyName("loginName")]
        public string LoginName { get; set; }

        [JsonPropertyName("loginPassword")]
        //public byte[] LoginPassword { get; set; }
        public string? LoginPassword { get; set; }

        // Reporting Authorities
        [JsonPropertyName("ra1Mkey")]
        public decimal Ra1Mkey { get; set; }

        [JsonPropertyName("ra2Mkey")]
        public decimal? Ra2Mkey { get; set; }

        // Effective Dates
        [JsonPropertyName("effectiveStartDate")]
        public DateTime EffectiveStartDate { get; set; }

        [JsonPropertyName("effectiveEndDate")]
        public DateTime? EffectiveEndDate { get; set; }

        // Notifications
        [JsonPropertyName("emailFrequency")]
        public string EmailFrequency { get; set; }

        [JsonPropertyName("browserNotification")]
        public string BrowserNotification { get; set; }

        // Tokens
        [JsonPropertyName("webToken")]
        public string WebToken { get; set; }

        [JsonPropertyName("mobileToken")]
        public string MobileToken { get; set; }

        // Attributes (Custom fields)
        [JsonPropertyName("attribute1")]
        public string Attribute1 { get; set; }

        [JsonPropertyName("attribute2")]
        public string Attribute2 { get; set; }

        [JsonPropertyName("attribute3")]
        public string Attribute3 { get; set; }

        [JsonPropertyName("attribute4")]
        public string Attribute4 { get; set; }

        [JsonPropertyName("attribute5")]
        public string Attribute5 { get; set; }

        [JsonPropertyName("attribute6")]
        public string Attribute6 { get; set; }

        [JsonPropertyName("attribute7")]
        public string Attribute7 { get; set; }

        [JsonPropertyName("attribute8")]
        public string Attribute8 { get; set; }

        [JsonPropertyName("attribute9")]
        public string Attribute9 { get; set; }

        [JsonPropertyName("attribute10")]
        public string Attribute10 { get; set; }

        // Audit Columns
        [JsonPropertyName("createdBy")]
        public decimal CreatedBy { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public decimal? LastUpdatedBy { get; set; }

        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LastUpdateDate { get; set; }

        // Status
        [JsonPropertyName("deleteFlag")]
        public char DeleteFlag { get; set; }

        // Security and Password Handling
        [JsonPropertyName("isForgotPassword")]
        public bool? IsForgotPassword { get; set; }

        [JsonPropertyName("tempPassword")]
        //public byte[] TempPassword { get; set; }
        public string? TempPassword { get; set; }

        [JsonPropertyName("resetFlag")]
        public char? ResetFlag { get; set; }

        // Personal Info
        [JsonPropertyName("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        // External System References
        [JsonPropertyName("erpEmpMkey")]
        public decimal? ErpEmpMkey { get; set; }

        [JsonPropertyName("oracleId")]
        public decimal? OracleId { get; set; }

        // Additional Info
        [JsonPropertyName("jobRole")]
        public int? JobRole { get; set; }
        public int? businessGroupId { get; set; }
    }


    public class  CommonInput_UserManagement_Model
    {
        [JsonPropertyName("Session_User_ID")]
        public int? Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int? Business_Group_ID { get; set; }

        //[JsonPropertyName("Current_Emp_Mkey")]
        //public int? CURRENT_EMP_MKEY { get; set; }
    }


    public class CommonInput_ViewUserManagement_Model
    {
        [JsonPropertyName("Session_User_ID")]
        public int? Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int? Business_Group_ID { get; set; }

        [JsonPropertyName("mkey")]
        public int? Mkey { get; set; }

        //[JsonPropertyName("Current_Emp_Mkey")]
        //public int? CURRENT_EMP_MKEY { get; set; }
    }
    public class ResponseResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<RoleManagementModel> Data { get; set; }
        public int TotalCount { get; set; }
    }

    public class RoleManagementModel
    {
        [JsonPropertyName("mkey")]
        public decimal MKEY { get; set; }

        [JsonPropertyName("roleName")]
        public string Role_Name { get; set; }

        [JsonPropertyName("roleDescription")]
        public string Role_Description { get; set; }

        [JsonPropertyName("companyId")]
        public string Company_Id { get; set; }

        [JsonPropertyName("remarks")]
        public string Remarks { get; set; }

        [JsonPropertyName("attribute1")]
        public string ATTRIBUTE1 { get; set; }

        [JsonPropertyName("attribute2")]
        public string ATTRIBUTE2 { get; set; }

        [JsonPropertyName("attribute3")]
        public string ATTRIBUTE3 { get; set; }

        [JsonPropertyName("attribute4")]
        public string ATTRIBUTE4 { get; set; }

        [JsonPropertyName("attribute5")]
        public string ATTRIBUTE5 { get; set; }

        [JsonPropertyName("createdBy")]
        public int CREATED_BY { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CREATION_DATE { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public int? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        [JsonPropertyName("deleteFlag")]
        public string DELETE_FLAG { get; set; }
        public int? PermissionCount { get; set; }
        [JsonPropertyName("createdByName")]
        public string? Created_By_Name { get; set; }
    }

    public class Response_UserManagementResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<UserManagment_EmployeeModel> Data { get; set; }
        public int TotalCount { get; set; }
    }



    public class  UserManagment_EmployeeModel
    {
        // Primary Key
        [JsonPropertyName("mkey")]
        public decimal MKEY { get; set; }

        // Basic Information
        [JsonPropertyName("companyId")]
        public decimal COMPANY_ID { get; set; }

        [JsonPropertyName("empCode")]
        public string EMP_CODE { get; set; }

        [JsonPropertyName("empFullName")]
        public string EMP_FULL_NAME { get; set; }

        [JsonPropertyName("firstName")]
        public string FIRST_NAME { get; set; }

        [JsonPropertyName("lastName")]
        public string LAST_NAME { get; set; }

        // Role and Project
        [JsonPropertyName("roleId")]
        public string ROLE_ID { get; set; }

        [JsonPropertyName("projectId")]
        public string PROJECT_ID { get; set; }

        // Job Information
        [JsonPropertyName("designationId")]
        public decimal? DESIGNATION_ID { get; set; }

        [JsonPropertyName("departmentId")]
        public decimal? DEPARTMENT_ID { get; set; }

        // Contact Info
        [JsonPropertyName("contactNo")]
        public decimal CONTACT_NO { get; set; }

        [JsonPropertyName("emailIdOfficial")]
        public string EMAIL_ID_OFFICIAL { get; set; }

        [JsonPropertyName("emailIdPersonal")]
        public string EMAIL_ID_PERSONAL { get; set; }

        // Login Info
        [JsonPropertyName("loginName")]
        public string LOGIN_NAME { get; set; }

        //[JsonPropertyName("loginPassword")]
        ////public byte[] LoginPassword { get; set; }
        //public string? LOGIN_PASSWORD { get; set; }

        // Reporting Authorities
        [JsonPropertyName("ra1Mkey")]
        public decimal RA1_MKEY { get; set; }

        [JsonPropertyName("ra2Mkey")]
        public decimal? RA2_MKEY { get; set; }

        // Effective Dates
        [JsonPropertyName("effectiveStartDate")]
        public DateTime EFFECTIVE_START_DATE { get; set; }

        [JsonPropertyName("effectiveEndDate")]
        public DateTime? EFFECTIVE_END_DATE { get; set; }

        // Notifications
        [JsonPropertyName("emailFrequency")]
        public string EMAIL_FREQUENCY { get; set; }

        [JsonPropertyName("browserNotification")]
        public string BROWSER_NOTIFICATION { get; set; }

        // Tokens
        [JsonPropertyName("webToken")]
        public string WEB_TOKEN { get; set; }

        [JsonPropertyName("mobileToken")]
        public string MOBILE_TOKEN { get; set; }

        // Attributes (Custom fields)
        [JsonPropertyName("attribute1")]
        public string ATTRIBUTE1 { get; set; }

        [JsonPropertyName("attribute2")]
        public string ATTRIBUTE2 { get; set; }

        [JsonPropertyName("attribute3")]
        public string ATTRIBUTE3 { get; set; }

        [JsonPropertyName("attribute4")]
        public string ATTRIBUTE4 { get; set; }

        [JsonPropertyName("attribute5")]
        public string ATTRIBUTE5 { get; set; }

        [JsonPropertyName("attribute6")]
        public string ATTRIBUTE6 { get; set; }

        [JsonPropertyName("attribute7")]
        public string ATTRIBUTE7 { get; set; }

        [JsonPropertyName("attribute8")]
        public string ATTRIBUTE8 { get; set; }

        [JsonPropertyName("attribute9")]
        public string ATTRIBUTE9 { get; set; }

        [JsonPropertyName("attribute10")]
        public string ATTRIBUTE10 { get; set; }

        // Audit Columns
        [JsonPropertyName("createdBy")]
        public decimal CREATED_BY { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public decimal? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LastUpdateDate { get; set; }

        // Status
        [JsonPropertyName("deleteFlag")]
        public char DeleteFlag { get; set; }

        // Security and Password Handling
        //[JsonPropertyName("isForgotPassword")]
        //public bool? IsForgotPassword { get; set; }

        //[JsonPropertyName("tempPassword")]
        ////public byte[] TempPassword { get; set; }
        //public string? TempPassword { get; set; }

        //[JsonPropertyName("resetFlag")]
        //public char? ResetFlag { get; set; }

        // Personal Info
        [JsonPropertyName("dateOfBirth")]
        public DateTime? Date_of_birth { get; set; }

        // External System References
        //[JsonPropertyName("erpEmpMkey")]
        //public decimal? ErpEmpMkey { get; set; }

        //[JsonPropertyName("oracleId")]
        //public decimal? OracleId { get; set; }

        //// Additional Info
        //[JsonPropertyName("jobRole")]
        //public int? JobRole { get; set; }

        public int? PermissionCount { get; set; }
        [JsonPropertyName("createdByName")]
        public string? Created_By_Name { get; set; }
    }


    public class Module_Hdr
    {
        [JsonPropertyName("mkey")]
        public decimal mkey { get; set; }

        [JsonPropertyName("moduleName")]
        public string? moduleName { get; set; }

        [JsonPropertyName("description")]
        public string? description { get; set; }

        [JsonPropertyName("parentId")]
        public decimal? parentId { get; set; }

        [JsonPropertyName("attribute1")]
        public string? attribute1 { get; set; }

        [JsonPropertyName("attribute2")]
        public string? attribute2 { get; set; }

        [JsonPropertyName("attribute3")]
        public string? attribute3 { get; set; }

        [JsonPropertyName("attribute4")]
        public string? attribute4 { get; set; }

        [JsonPropertyName("attribute5")]
        public string? attribute5 { get; set; }

        [JsonPropertyName("createdBy")]
        public decimal? createdBy { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime? creationDate { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public decimal? lastUpdatedBy { get; set; }

        [JsonPropertyName("lastUpdateDate")]
        public DateTime? lastUpdateDate { get; set; }

        [JsonPropertyName("deleteFlag")]
        public string deleteFlag { get; set; } = "N";
    }
}
