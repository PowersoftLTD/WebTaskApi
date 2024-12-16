namespace TaskManagement.API.Model
{
    public class EmployeeCompanyMST
    {
        public int MKEY { get; set; }
        public int? COMPANY_ID { get; set; }
        public string? EMP_CODE { get; set; }
        public string? EMP_FULL_NAME { get; set; }
        public string? FIRST_NAME { get; set; }
        public string? LAST_NAME { get; set; }
        public int CURRENT_EMP_MKEY { get; set; }
        public string? FILTER { get; set; }
        public string? ROLE_ID { get; set; }
        public string? PROJECT_ID { get; set; }

        public string? AssignNameLike { get; set; }
        
        public int? DESIGNATION_ID { get; set; }
        public int? DEPARTMENT_ID { get; set; }
        public int? CONTACT_NO { get; set; }
        public string? EMAIL_ID_OFFICIAL { get; set; }
        public string? EMAIL_ID_PERSONAL { get; set; }
        public string? Login_ID { get; set; }
        public string? LOGI_NAME { get; set; }
        public string? LOGIN_PASSWORD { get; set; }
        public int? RA1_MKEY { get; set; }
        public int? RA2_MKEY { get; set; }
        public DateTime? EFFECTIVE_START_DATE { get; set; }
        public DateTime? EFFECTIVE_END_DATE { get; set; }
        public string? EMAIL_FREQUENCY { get; set; }
        public string? BROWSER_NOTIFICATION { get; set; }
        public string? WEB_TOKEN { get; set; }
        public string? MOBILE_TOKEN { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public char? DELETE_FLAG { get; set; }
        public bool? ISFORGOTPASSWORD { get; set; }
        public string? TEMPPASSWORD { get; set; }
        public int? BUSINESS_GROUP_ID { get; set; }
        public string? BUSINESS_GROUPS_NAME { get; set; }
        public string? COMPANY_NAME { get; set; }
        public char? RESSET_FLAG { get; set; }
        public string? STATUS { get; set }
        public string? MESSAGE { get; set }

    }
}
