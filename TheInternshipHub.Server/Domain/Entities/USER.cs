using System.ComponentModel.DataAnnotations;

namespace TheInternshipHub.Server.Domain.Entities
{
    public class USER
    {
        [Key]
        public Guid US_ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string US_FIRST_NAME { get; set; }

        [Required]
        [MaxLength(100)]
        public string US_LAST_NAME { get; set; }

        [Required]
        [MaxLength(255)]
        public string US_EMAIL { get; set; }

        [Required]
        [MaxLength(10)]
        public string US_PHONE_NUMBER { get; set; }

        [Required]
        public string US_PASSWORD { get; set; }

        [Required]
        public Guid US_COMPANY_ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string US_ROLE { get; set; }

        [Required]
        [MaxLength(100)]
        public string US_CITY { get; set; }

        [Required]
        public bool US_IS_DELETED { get; set; }
    }
}