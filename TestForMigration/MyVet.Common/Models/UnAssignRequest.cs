using System.ComponentModel.DataAnnotations;

namespace MyVet.Common.Models
{
    public class UnAssignRequest
    {
        [Required]
        public int AgendaId { get; set; }
    }
}
