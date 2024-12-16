using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManagement.API.Model
{
    public class TASK_ACTION_TRL
    {
        public int MKEY { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public decimal? PROGRESS_PERC { get; set; }
        public string? ACTION_TYPE { get; set; }
        public string? COMMENT { get; set; }
        public string? FILE_NAME { get; set; }
        public string? FILE_PATH { get; set; }
        public int? TASK_MKEY { get; set; }
        public int? CURRENT_EMP_MKEY { get; set; }
        public string? CURR_ACTION { get; set; }

        public string? RESPONSE_STATUS { get; set; }
        public string? RESPONSE_MESSAGE { get; set; }
    }
}
