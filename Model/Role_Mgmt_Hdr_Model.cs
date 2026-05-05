using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Role_Mgmt_Hdr_Model
    {
        [JsonPropertyName("mkey")]
        public int Mkey { get; set; }

        [JsonPropertyName("roleName")]
        public string Role_Name { get; set; } = string.Empty;

        [JsonPropertyName("roleDescription")]
        public string? Role_Description { get; set; }

        [JsonPropertyName("companyId")]
        public string? Company_Id { get; set; }

        [JsonPropertyName("remarks")]
        public string? Remarks { get; set; }

        [JsonPropertyName("attribute1")]
        public string? ATTRIBUTE1 { get; set; }

        [JsonPropertyName("attribute2")]
        public string? ATTRIBUTE2 { get; set; }

        [JsonPropertyName("attribute3")]
        public string? ATTRIBUTE3 { get; set; }

        [JsonPropertyName("attribute4")]
        public string? ATTRIBUTE4 { get; set; }

        [JsonPropertyName("attribute5")]
        public string? ATTRIBUTE5 { get; set; }

        [JsonPropertyName("createdBy")]
        public int CREATED_BY { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime CREATION_DATE { get; set; }

        [JsonPropertyName("lastUpdatedBy")]
        public int? LAST_UPDATED_BY { get; set; }

        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        [JsonPropertyName("deleteFlag")]
        [DefaultValue("N")]
        public string DELETE_FLAG { get; set; } = "N";

        [JsonPropertyName("sessionUserId")]
        public int? Session_userId { get; set; }

        [JsonPropertyName("businessGroupId")]
        public int? Business_groupId { get; set; }
    }

    public class Role_UserManagement_Model
    {
        public decimal Mkey { get; set; }
        [JsonPropertyName("RoleName")]
        public string? Role_Name { get; set; }
        [JsonPropertyName("RoleDescription")]
        public string? Role_Description { get; set; }
        public string? Status { get; set; }
        [JsonPropertyName("companyId")]
        public string? Company_Id { get; set; }
        public string? Remarks { get; set; }
        [JsonPropertyName("SessionUserId")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("BusinessGroupId")]
        public int? Business_Group_Id { get; set; }
        public List<Role_Module_Assignment_TRL> role_Module_Assignment_TRL { get; set; }
        
        //public string? ATTRIBUTE1 { get; set; }
        //public string? ATTRIBUTE2 { get; set; }
        //public string? ATTRIBUTE3 { get; set; }
        //public string? ATTRIBUTE4 { get; set; }
        //public string? ATTRIBUTE5 { get; set; }
        //public int CREATED_BY { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        //public int? LAST_UPDATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        //public string DELETE_FLAG { get; set; } = "N";
    }

    public class Module_HDR
    {
        public int Mkey { get; set; }

        public string? Module_Name { get; set; }

        public string? Description { get; set; }

        public int? Parent_Id { get; set; }

        public string? ATTRIBUTE1 { get; set; }

        public string? ATTRIBUTE2 { get; set; }

        public string? ATTRIBUTE3 { get; set; }

        public string? ATTRIBUTE4 { get; set; }

        public string? ATTRIBUTE5 { get; set; }

        public int CREATED_BY { get; set; }

        public DateTime CREATION_DATE { get; set; }

        public int? LAST_UPDATED_BY { get; set; }

        public DateTime? LAST_UPDATE_DATE { get; set; }

        public string DELETE_FLAG { get; set; } = "N";
    }


    public class Role_Module_Assignment_TRL
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("srno")]
        public int Sr_No { get; set; }
        //public int Role_Mkey { get; set; }
        [JsonPropertyName("ModuleId")]
        public int Module_Id { get; set; }
        [JsonPropertyName("Can_View")]
        [DefaultValue("N")]
        public char? Can_View { get; set; }
        [JsonPropertyName("canAdd")]
        [DefaultValue("N")]
        public char? Can_Add { get; set; }
        [JsonPropertyName("canModify")]
        [DefaultValue("N")]
        public char? Can_Modify { get; set; }
        [JsonPropertyName("canDelete")]
        [DefaultValue("N")]
        public char? Can_Delete { get; set; }
        [JsonPropertyName("attribute1")]
        public string? ATTRIBUTE1 { get; set; }
        [JsonPropertyName("attribute2")]
        public string? ATTRIBUTE2 { get; set; }
        [JsonPropertyName("attribute3")]
        public string? ATTRIBUTE3 { get; set; }
        [JsonPropertyName("attribute4")]
        public string? ATTRIBUTE4 { get; set; }
        [JsonPropertyName("attribute5")]
        public string? ATTRIBUTE5 { get; set; }
        [JsonPropertyName("CreatedBy")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("creationDate")]
        public DateTime CREATION_DATE { get; set; }
        [JsonPropertyName("lastUpdatedBy")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("lastUpdateDate")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("deleteFlag")]
        [DefaultValue("N")]
        public string DELETE_FLAG { get; set; } = "N";
    }


    public class Role_ManagementPayload_Model
    {
        public decimal Mkey { get; set; }
        [JsonPropertyName("RoleName")]
        public string? Role_Name { get; set; }
        [JsonPropertyName("RoleDescription")]
        public string? Role_Description { get; set; }
        public string? Status { get; set; }
        [JsonPropertyName("companyId")]
        public string? Company_Id { get; set; }
        public string? Remarks { get; set; }
        [JsonPropertyName("SessionUserId")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("BusinessGroupId")]
        public int? Business_Group_Id { get; set; }
        public List<Role_Module_Assignment_TRL> role_Module_Assignment_TRL { get; set; }

        //public string? ATTRIBUTE1 { get; set; }
        //public string? ATTRIBUTE2 { get; set; }
        //public string? ATTRIBUTE3 { get; set; }
        //public string? ATTRIBUTE4 { get; set; }
        //public string? ATTRIBUTE5 { get; set; }
        //public int CREATED_BY { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        //public int? LAST_UPDATED_BY { get; set; }
        //public DateTime? LAST_UPDATE_DATE { get; set; }
        //public string DELETE_FLAG { get; set; } = "N";
    }

    public class RoleManagementDetailModel
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
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
        public DateTime? CREATION_DATE { get; set; }
        [JsonPropertyName("LastUpdateBY")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("ladtUpdateDate")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
        [JsonPropertyName("Delete_flag")]
        public string DELETE_FLAG { get; set; }
        [JsonPropertyName("srNo")]

        public int Sr_No { get; set; }
        [JsonPropertyName("moduleId")]
        public int Module_Id { get; set; }
        [JsonPropertyName("canView")]
        public string Can_View { get; set; }
        [JsonPropertyName("canAdd")]
        public string Can_Add { get; set; }
        [JsonPropertyName("canModify")]
        public string Can_Modify { get; set; }
        [JsonPropertyName("canDelete")]
        public string Can_Delete { get; set; }
        // 🔥 Child List
       // public List<RoleModuleModel> Modules { get; set; } = new List<RoleModuleModel>();
    }
    
    public class Emp_Role_Comp_Matrix_Model
    {
        public int Mkey { get; set; }
        public int? Sr_No { get; set; }
        public int? Role_Mkey { get; set; }
        public int? Comp_Mkey { get; set; }
        public int? Business_Group_id { get; set; }
        public string? ATTRIBUTE1 { get; set; }
        public string? ATTRIBUTE2 { get; set; }
        public string? ATTRIBUTE3 { get; set; }
        public string? ATTRIBUTE4 { get; set; }
        public string? ATTRIBUTE5 { get; set; }
        public int? CREATED_BY { get; set; }
        public string? CREATION_DATE { get; set; }
        public int? LAST_UPDATED_BY { get; set; }
        public string? LAST_UPDATE_DATE { get; set; }
        public string? DELETE_FLAG { get; set; }
    }



}
