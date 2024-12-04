using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.API.Model
{
    public class PROJECT_TRL_APPROVAL_ABBR
    {
        [Column("HEADER_MKEY")]
        public int? HEADER_MKEY { get; set; }

        [Column("SEQ_NO")]
        public string? TASK_NO { get; set; }

        [Column("APPROVAL_MKEY")]
        public int? APPROVAL_MKEY { get; set; }

        [Column("APPROVAL_ABBRIVATION")]
        public string? APPROVAL_ABBRIVATION { get; set; }

        [Column("APPROVAL_DESCRIPTION")]
        public string? APPROVAL_DESCRIPTION { get; set; }

        [Column("DAYS_REQUIRED")]
        public int? DAYS_REQUIRED { get; set; }

        [Column("DEPARTMENT")]
        public int? DEPARTMENT { get; set; }

        [Column("JOB_ROLE")]
        public int? JOB_ROLE { get; set; }

        [Column("RESPOSIBLE_EMP_MKEY")]
        public int? RESPOSIBLE_EMP_MKEY { get; set; }

        [Column("OUTPUT_DOCUMENT")]
        public string? OUTPUT_DOCUMENT { get; set; }

        [Column("TENTATIVE_START_DATE")]
        public DateTime? TENTATIVE_START_DATE { get; set; }

        [Column("TENTATIVE_END_DATE")]
        public DateTime? TENTATIVE_END_DATE { get; set; }

        [Column("STATUS")]
        public string? STATUS { get; set; }

        [Column("CREATED_BY")]
        public int CREATED_BY { get; set; }

        [Column("ATTRIBUTE1")]
        public string? ATTRIBUTE1 { get; set; }

        [Column("ATTRIBUTE2")]
        public string? ATTRIBUTE2 { get; set; }

        [Column("ATTRIBUTE3")]
        public string? ATTRIBUTE3 { get; set; }

        [Column("ATTRIBUTE4")]
        public string? ATTRIBUTE4 { get; set; }

        [Column("ATTRIBUTE5")]
        public string? ATTRIBUTE5 { get; set; }
        public string? ResponseStatus { get; set; }
        public string? Message { get; set; }

    }
}
