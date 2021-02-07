using EcCoach.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcCoach.Web.Models
{
    public class Type: ISoftDeleted
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public bool Deleted { get ; set ; }
    }
}
