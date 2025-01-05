using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheInternshipHub.Server.Domain.Entities
{
    public class INTERNSHIP
    {
        [Key]
        public Guid IN_ID { get; set; }

        [Required, MaxLength(200)]
        public string IN_TITLE { get; set; }

        [MaxLength]
        public string IN_DESCRIPTION { get; set; }

        [Required]
        public Guid IN_COMPANY_ID { get; set; }

        [ForeignKey("IN_COMPANY_ID")]
        public COMPANY Company { get; set; }

        [Required]
        public Guid IN_RECRUITER_ID { get; set; }

        [ForeignKey("IN_RECRUITER_ID")]
        public USER Recruiter { get; set; }

        [Required]
        public DateTime IN_START_DATE { get; set; }

        [Required]
        public DateTime IN_END_DATE { get; set; }

        [Required]
        public int IN_POSITIONS_AVAILABLE { get; set; }

        [Required]
        public int IN_COMPENSATION { get; set; }

        [Required]
        public DateTime IN_DEADLINE { get; set; }

        [Required]
        public bool IN_IS_DELETED { get; set; }
    }
}
