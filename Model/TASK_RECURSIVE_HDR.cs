
using System.Diagnostics.CodeAnalysis;

namespace TaskManagement.API.Model
{
    public class TASK_RECURSIVE_HDR
    {
        public int MKEY { get; set; }
        public string TASK_NAME { get; set; }
        public string TASK_DESCRIPTION { get; set; }
        public string TERM { get; set; }
        public DateTime STARTS { get; set; }
        public string ENDS { get; set; }
        public decimal CREATED_BY { get; set; }
        public decimal LAST_UPDATED_BY { get; set; }
        [AllowNull]
        public string ATTRIBUTE1 { get; set; }
        [AllowNull]
        public string ATTRIBUTE2 { get; set; }
        [AllowNull]
        public string ATTRIBUTE3 { get; set; }
        [AllowNull]
        public string ATTRIBUTE4 { get; set; }
        [AllowNull]
        public string ATTRIBUTE5 { get; set; }
        [AllowNull]
        public string ATTRIBUTE6 { get; set; }
        [AllowNull]
        public string ATTRIBUTE7 { get; set; }
    }
}
