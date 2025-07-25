using System.Text.Json.Serialization;

namespace TaskManagement.API.Model
{
    public class DocCategoryOutPut_List
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }

        public IEnumerable<V_Building_Classification> Data { get; set; }
    }
    public class DocCategoryInput
    {
        [JsonPropertyName("DOC_CATEGORY")]
        public string DOC_CATEGORY { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public int COMPANY_ID { get; set; }
    }

    public class InsertInstructionInput
    {
        [JsonPropertyName("DOC_INSTR")]
        public string DOC_INSTR { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("COMPANY_ID")]
        public int COMPANY_ID { get; set; }
    }

    public class InsertInstructionInputNT
    {
        [JsonPropertyName("DOC_INSTR")]
        public string DOC_INSTR { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        //[JsonPropertyName("COMPANY_ID")]
        //public int COMPANY_ID { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class DocTypeInputNT
    {
        [JsonPropertyName("Doc_Name")]
        public string DOC_INSTR { get; set; }
        [JsonPropertyName("Created_By")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("Session_User_Id")]
        public int Session_User_Id { get; set; }
        [JsonPropertyName("Business_Group_Id")]
        public int Business_Group_Id { get; set; }
    }

    public class DocCategoryUpdateInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("DOC_CATEGORY")]
        public string? DOC_CATEGORY { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char DELETE_FLAG { get; set; }
    }

    public class UpdateInstructionInput
    {
        [JsonPropertyName("MKEY")]
        public int MKEY { get; set; }
        [JsonPropertyName("DOC_INSTR")]
        public string? DOC_INSTR { get; set; }
        [JsonPropertyName("CREATED_BY")]
        public int CREATED_BY { get; set; }
        [JsonPropertyName("DELETE_FLAG")]
        public char DELETE_FLAG { get; set; }
    }

    public class DocCategoryOutPut
    {
        [JsonPropertyName("DOC_CATEGORY")]
        public string DOC_CATEGORY { get; set; }
    }

    public class DocCategoryOutPutNT
    {
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("Message")]
        public string? Message { get; set; }
        [JsonPropertyName("Data")]
        public IEnumerable<GetTaskTypeOutPutNT> Data { get; set; }
    }
}
