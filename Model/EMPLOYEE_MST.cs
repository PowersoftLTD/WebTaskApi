using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagement.API.Model
{
    public class EMPLOYEE_MST 
    {
        [Required]
        //[DataType(DataType.Text)]
        public string LOGIN_NAME { get; set; }
        [Required]
        //[DataType(DataType.Password)]
        public string LOGIN_PASSWORD { get; set; }
    }

    public class EMPLOYEE_MST_NT
    {
        
        [JsonPropertyName("Login_Name")]
        public string LOGIN_NAME { get; set; }
        
        [JsonPropertyName("Login_Password")]
        public string LOGIN_PASSWORD { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_ID { get; set; }
    }

    // Model Created  by Itemad Hyder 24-10-2025 
    public class EmployeeDetails_ByDepartmentIds
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
        public byte[] LoginPassword { get; set; }

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
        public byte[] TempPassword { get; set; }

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

    public class userDepartment
    {
        [JsonPropertyName("departmentId")]
        public string? departmentId { get; set; }
    }

    public class DesignationModel
    {
        public int MKEY { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? TYPE_DESC { get; set; }
        public string? TYPE_ABBR { get; set; }
        public int? PARENT_ID { get; set; }
        public int? MASTER_MKEY { get; set; }
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        public string? ENABLE_FLAG { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public decimal? CREATED_BY { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public string? DELETE_FLAG { get; set; }
    }

    public class Reported_managerList
    {
        public int MKEY { get; set; }
        public int? COMPANY_ID { get; set; }

        public string? EMP_CODE { get; set; }
        public string? EMP_FULL_NAME { get; set; }
        public string? FIRST_NAME { get; set; }
        public string? LAST_NAME { get; set; }

        public int? ROLE_ID { get; set; }
        //public int? PROJECT_ID { get; set; }
        public int? DESIGNATION_ID { get; set; }
        public int? DEPARTMENT_ID { get; set; }

        public string? CONTACT_NO { get; set; }
        public string? EMAIL_ID_OFFICIAL { get; set; }
        public string? EMAIL_ID_PERSONAL { get; set; }

        public DateTime? EFFECTIVE_START_DATE { get; set; }
        public DateTime? EFFECTIVE_END_DATE { get; set; }

        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }

        public decimal? CREATED_BY { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }

        public string? DELETE_FLAG { get; set; }
    }
}
