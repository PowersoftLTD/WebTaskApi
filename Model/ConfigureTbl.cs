using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskManagement.API.Model
{
    public class ConfigureTbl
    {

        public int ConfigID { get; set; }
        public string Configure { get; set; }
        public string ConfigureValue { get; set; }
    }
}
