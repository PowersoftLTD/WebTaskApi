using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class CompanyModel
    {
        public decimal COMPANY_ID { get; set; }
        public string NAME { get; set; }
        public decimal? BUSINESS_GROUP_ID { get; set; }
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        public string ADDRESS_LINE1 { get; set; }
        public string ADDRESS_LINE2 { get; set; }
        public string ADDRESS_LINE3 { get; set; }
        public decimal? CITY_MKEY { get; set; }
        public decimal? STATE_MKEY { get; set; }
        public string TEL_NO { get; set; }
        public string FAX_NO { get; set; }
        public string ZIP_CODE { get; set; }
        public string PAN_NO { get; set; }
        public string EMAIL_ID { get; set; }
        public string FA_YEAR { get; set; }
        public DateTime? FA_START_DATE { get; set; }
        public DateTime? FA_END_DATE { get; set; }
        public string ENABLE_FLAG { get; set; }
        //public string? ATTRIBUTE1 { get; set; }
        //public string? ATTRIBUTE2 { get; set; }
        //public string? ATTRIBUTE3 { get; set; }
        //public string? ATTRIBUTE4 { get; set; }
        //public string? ATTRIBUTE5 { get; set; }
        public decimal? CREATED_BY { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public decimal? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public string DELETE_FLAG { get; set; }
    }

    public class Common_UserManagement_OutResponse
    {

        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public Employee_MST_OUtPutResponse employee_Mst { get; set; }
        //public RoleManagementDetailModel employee_Mst { get; set; }
        
    }

    public class Employee_MST_OUtPutResponse    
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

        [JsonPropertyName("roleCompanyList")]
        public List<EmployeeRoleCompanyModel>? RoleCompanyList { get; set; }

        public List<EMP_Project_Building_Matrix_Model>? ProjectBuildingMatrix { get; set; }

    }
    public class EmployeeRoleCompanyModel
    {
        [JsonPropertyName("employeeMkey")]
        public int Employee_Mkey { get; set; }
        [JsonPropertyName("roleMkey")]
        public int Role_Mkey { get; set; }
        [JsonPropertyName("employeeFullName")]
        public string EmployeeFullName { get; set; }
        [JsonPropertyName("roleName")]
        public string Role_Name { get; set; }
        [JsonPropertyName("roleDescription")]
        public string Role_Description { get; set; }
        [JsonPropertyName("companyId")]
        public int? Company_Id { get; set; }
        [JsonPropertyName("companyName")]
        public string? Company_Name { get; set; }
    }

    public class EmailVerified_InputResponse
    {
        [JsonPropertyName("Session_User_ID")]
        public int? Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int? Business_Group_ID { get; set; }

        [JsonPropertyName("emailId")]
        public string? UserEmailID { get; set; }
        //public bool IsEmailVerified { get; set; }
    }

    public class ProjectClass_PS_Input_NT
    {
        [JsonPropertyName("Type_Code")]
        public string Type_Code { get; set; }
        [JsonPropertyName("Master_Mkey")]
        public string Master_mkey { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

        [JsonPropertyName("companyId")]
        public int COMPANY_ID { get; set; }

    }
}
