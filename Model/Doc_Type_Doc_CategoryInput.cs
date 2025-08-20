using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class Doc_Type_Doc_CategoryInput
    {
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }

        [JsonPropertyName("PropertyMkey")]
        public string? PropertyMkey { get; set; }
    }

    public class Document_CategoryOutPutNT
    {
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class TaskProjectsFilterNT
    {
        [JsonPropertyName("TaskType")]
        public string? TaskType { get; set; }
        [JsonPropertyName("PropertyMkey")]
        public int? Property { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class UserProjectBuildingActivityNT
    {
        [JsonPropertyName("User_Mkey")]
        public string? USER_MKEY { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class UserProjectBuildingActivityPostNT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("User_Mkey")]
        public int? USER_MKEY { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int? PROPERTY_MKEY { get; set; }
        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public string? DELETE_FLAG { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }


    public class UserProjectBuildingActivityOutputNT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<UserProjectBuildingActivityInputNT> Data { get; set; }
    }

    public class UserProjectBuildingActivityInputNT
    {
        [JsonPropertyName("Mkey")]
        public int? MKEY { get; set; }
        [JsonPropertyName("User_Mkey")]
        public int? USER_MKEY { get; set; }

        [JsonPropertyName("Property_Mkey")]
        public int? PROPERTY_MKEY { get; set; }

        [JsonPropertyName("Property_Name")]
        public string? PROPERTY_Name { get; set; }

        [JsonPropertyName("Building_Mkey")]
        public int? BUILDING_MKEY { get; set; }

        [JsonPropertyName("Building_Name")]
        public string? BUILDING_NAME { get; set; }

        [JsonPropertyName("Created_By_Id")]
        public int CREATED_BY_ID { get; set; }
        [JsonPropertyName("Created_By_Name")]
        public string CREATED_BY_NAME { get; set; }
        [JsonPropertyName("Creation_Date")]
        public string CREATION_DATE { get; set; }
        [JsonPropertyName("Last_Updated_By")]
        public int? LAST_UPDATED_BY { get; set; }
        [JsonPropertyName("Updated_By_Name")]
        public string UPDATED_BY_NAME { get; set; }
        [JsonPropertyName("Last_Update_Date")]
        public string LAST_UPDATE_DATE { get; set; }

        [JsonPropertyName("Delete_Flag")]
        public string? DELETE_FLAG { get; set; }
    }

    public class TaskProjectDashboardInput
    {
        [JsonPropertyName("ProjectMkey")]
        public string? ProjectMkey { get; set; }
        [JsonPropertyName("BuildingMkey")]
        public string? BuildingMkey { get; set; }

        [JsonPropertyName("Task_Type")]
        public string? TASK_TYPE { get; set; }
        [JsonPropertyName("Filter")]
        public string? FILTER { get; set; }

        // Approval Filter
        [JsonPropertyName("Building_Type")]
        public string? Building_Type { get; set; }
        [JsonPropertyName("Building_Standard")]
        public string? Building_Standard { get; set; }
        [JsonPropertyName("Statutory_Authority")]
        public string? Statutory_Authority { get; set; }

        // Compliance Filter
        [JsonPropertyName("ResponsibleDepartment")]
        public string? ResponsibleDepart { get; set; }
        [JsonPropertyName("JobRole")]
        public string? JobRole { get; set; }
        [JsonPropertyName("ResponsiblePerson")]
        public string? ResponsiblePerson { get; set; }
        [JsonPropertyName("RaisedAt")]
        public string? RaisedAt { get; set; }
        [JsonPropertyName("RaisedAtBefore")]
        public string? RaisedAtBefore { get; set; }
        [JsonPropertyName("Status")]
        public string? Status { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class EmailDto
    {
        [JsonPropertyName("To")]
        public string To { get; set; }
        [JsonPropertyName("Subject")]
        public string Subject { get; set; }
        [JsonPropertyName("Body")]
        public string Body { get; set; }
        [JsonPropertyName("FromEmail")]
        public string FromEmail { get; set; }
        [JsonPropertyName("FromPassword")]
        public string FromPassword { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }
}
