using System.ComponentModel.DataAnnotations;

namespace TheInternshipHub.Server.Domain.Entities
{
    public class COMPANY
    {
        [Key]
        public Guid CO_ID { get; set; }

        [MaxLength(100)]
        public string CO_NAME { get; set; }

        [MaxLength(100)]
        public string CO_CITY { get; set; }

        [MaxLength(100)]
        public string CO_WEBSITE { get; set; }

        public string CO_LOGO_PATH {  get; set; }
    }
}
