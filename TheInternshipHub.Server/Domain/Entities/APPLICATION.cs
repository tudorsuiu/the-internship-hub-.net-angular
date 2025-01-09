using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheInternshipHub.Server.Domain.Entities
{
    public class APPLICATION
    {
        [Key]
        public Guid AP_ID { get; set; }

        [Required]
        public Guid AP_INTERNSHIP_ID { get; set; }

        [ForeignKey("AP_INTERNSHIP_ID")]
        public INTERNSHIP Internship { get; set; }

        [Required]
        public Guid AP_STUDENT_ID { get; set; }

        [ForeignKey("AP_STUDENT_ID")]
        public USER Student { get; set; }

        [Required]
        public DateTime AP_APPLIED_DATE { get; set; }

        [Required, MaxLength(50)]
        public string AP_STATUS { get; set; }

        [Required]
        public string AP_CV_FILE_PATH { get; set; }

        public string AP_UNIVERSITY_DOCS_FILE_PATH { get; set; }

        [Required]
        public bool AP_IS_DELETED { get; set; }
    }
}
