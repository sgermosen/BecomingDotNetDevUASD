using System.ComponentModel.DataAnnotations;

namespace MyVet.Common.Models
{
    public class AssignRequest
    {
        [Required]
        public int AgendaId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public int PetId { get; set; }

        public string Remarks { get; set; }
    }
}
