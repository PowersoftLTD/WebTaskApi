using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagement.API.Model
{
    public class AssignedToInput
    {
        [JsonPropertyName("AssignNameLike")]
        public string AssignNameLike { get; set; }
    }
}
