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
        public int? BUSINESS_GROUP_ID { get; set; }
        public string? Business_Group_Name { get; set; }
        public int? COMPANY_ID { get; set; }
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

        public RoleManagement roleManagement { get; set; }
    }

    public class personalInformation
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Mobile_No { get; set; }
        public string? EmployeeId { get; set; }
        public string? UserName { get; set; }
        public string? Status { get; set; }

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
    }
}
