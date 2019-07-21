using EcCoach.Web.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcCoach.Web.Models
{
    public class Following
    {
        [Key]
        [Column(Order = 1)]
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FolloweeId { get; set; }
        public ApplicationUser Followee { get; set; }

    }
}
