using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Task_UserDepartmentDetailsInputNT
    {
        [JsonPropertyName("Current_Emp_Mkey")]
        public int CURRENT_EMP_MKEY { get; set; }
        [JsonPropertyName("Filter")]
        public string? FILTER { get; set; }
        //[JsonPropertyName("Duration_filter")]
        //public string? DURATION_FILTER { get; set; }
        //[JsonPropertyName("Status_Filter")]
        //public string? Status_Filter { get; set; }
        //[JsonPropertyName("PriorityFilter")]
        //public string? PriorityFilter { get; set; }
        //[JsonPropertyName("TypeFilter")]
        //public string? TypeFilter { get; set; }

        [JsonPropertyName("Session_User_ID")]
        public int Session_User_ID { get; set; }
        [JsonPropertyName("Business_Group_ID")]
        public int Business_Group_ID { get; set; }

        public string Department { get; set; }
        public string Type { get; set; }
    }
    public class Department_V
    {
        [JsonPropertyName("Mkey")]
        public int mkey { get; set; }

        [JsonPropertyName("Key")]
        public string Department_Name { get; set; }
    }

    public class Task_UserDepartmentOutPutNT_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<Department_V> Data { get; set; }
        //[JsonPropertyName("Data1")]
        //public IEnumerable<TaskDashboardCount_NT> Data1 { get; set; }
    }

    public class Task_userDepartMentResponseOutPUt_NT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<EmployeeDetails_ByDepartmentId> Data { get; set; }
    }

    public class TaskReportDashBoardFilterOutputListNT
    {
        [JsonPropertyName("Status")]
        public string? STATUS { get; set; }
        [JsonPropertyName("Message")]
        public string? MESSAGE { get; set; }
        [JsonPropertyName("User_Filter")]
        public IEnumerable<TaskDashBoardUserFilterNT> User_Filter { get; set; }
        [JsonPropertyName("Project_Filter")]
        public IEnumerable<TaskDashBoardUserFilterNT> Project_Filter { get; set; }
        [JsonPropertyName("Department_List")]
        public IEnumerable<Department_V> Department_List { get; set; }

    }
    public class EmployeeDetails_ByDepartmentId
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
}
