using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class MSPUploadExcelOutPut
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<MSPUploadExcel> Data { get; set; }
    }

    public class MSPUploadExcel
    {
        [JsonPropertyName("Mkey")]
        public int Mkey { get; set; }
        [JsonPropertyName("WBS")]
        public string WBS { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Duration")]
        public string Duration { get; set; }
        [JsonPropertyName("Start_Date")]
        public DateTime Start_Date { get; set; }
        [JsonPropertyName("Finish_Date")]
        public DateTime Finish_Date { get; set; }
        [JsonPropertyName("Predecessors")]
        public string Predecessors { get; set; }
        [JsonPropertyName("Resource_Names")]
        public string Resource_Names { get; set; }
        [JsonPropertyName("Text1")]
        public string Text1 { get; set; }
        [JsonPropertyName("Outline_Level")]
        public int Outline_Level { get; set; }
        [JsonPropertyName("Number1")]
        public int Number1 { get; set; }
        [JsonPropertyName("Unique_ID")]
        public int Unique_ID { get; set; }
        [JsonPropertyName("Percent_Complete")]
        public string Percent_Complete { get; set; }
        [JsonPropertyName("Created_By")]
        public int Created_By { get; set; }
        [JsonPropertyName("Creation_Date")]
        public DateTime Creation_Date { get; set; }
        [JsonPropertyName("Updated_By")]
        public int Updated_By { get; set; }
        [JsonPropertyName("Updation_Date")]
        public DateTime Updation_Date { get; set; }
        [JsonPropertyName("Process_Flag")]
        public string Process_Flag { get; set; }
        [JsonPropertyName("Remarks")]
        public string Remarks { get; set; }
        [JsonPropertyName("FileName")]
        public string FileName { get; set; }
        [JsonPropertyName("Mpp_Name")]
        public string mpp_name { get; set; }

        [JsonPropertyName("Weightage")]
        public string? Weightage { get; set; }
        [JsonPropertyName("Resources_Dept")]
        public string? Resources_Dept { get; set; }
        [JsonPropertyName("Appr_Abbr")]
        public string? Appr_Abbr { get; set; }
    }


    public class MSPUploadExcelInput
    {
        [JsonPropertyName("Project")]
        public int Project { get; set; }
        [JsonPropertyName("Sub_Project")]
        public int Sub_Project { get; set; }
        [JsonPropertyName("WBS")]
        public string? WBS { get; set; }
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("Duration")]
        public string? Duration { get; set; }
        [JsonPropertyName("Start_Date")]
        public DateTime? Start_Date { get; set; }
        public DateTime? Finish_Date { get; set; }
        [JsonPropertyName("Predecessors")]
        public string? Predecessors { get; set; }
        [JsonPropertyName("Resource_Names")]
        public string? Resource_Names { get; set; }
        [JsonPropertyName("Text1")]
        public string? Text1 { get; set; }
        [JsonPropertyName("Outline_Level")]
        public int? Outline_Level { get; set; }
        [JsonPropertyName("Number1")]
        public int? Number1 { get; set; }
        [JsonPropertyName("Unique_ID")]
        public int? Unique_ID { get; set; }

        [JsonPropertyName("Percent_Complete")]
        public string? Percent_Complete { get; set; }

        [JsonPropertyName("Created_By")]
        public int? Created_By { get; set; }
        [JsonPropertyName("Creation_Date")]
        public DateTime? Creation_Date { get; set; }
        [JsonPropertyName("Updated_By")]
        public int? Updated_By { get; set; }
        [JsonPropertyName("Updation_Date")]
        public DateTime? Updation_Date { get; set; }
        [JsonPropertyName("Process_Flag")]
        public string? Process_Flag { get; set; }

        [JsonPropertyName("FileName")]
        public string? FileName { get; set; }
        [JsonPropertyName("Mpp_Name")]
        public string? mpp_name { get; set; }

        [JsonPropertyName("Weightage")]
        public string? Weightage { get; set; }
        [JsonPropertyName("Resources_Dept")]
        public string? Resources_Dept { get; set; }
        [JsonPropertyName("Appr_Abbr")]
        public string? Appr_Abbr { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int? Business_Group_Id { get; set; }
    }


    public class MSPTaskInput
    {
        [JsonPropertyName("Project")]
        public int? Project { get; set; }
        [JsonPropertyName("Sub_Project")]
        public int? Sub_Project { get; set; }

        [JsonPropertyName("Session_User_Id")]
        public int? Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int? Business_Group_Id { get; set; }
    }
}
