using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Model
{
    public class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }

        [MaxLength(80)]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
