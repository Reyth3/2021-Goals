using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Shared.Models
{
    public class UserProfile
    {
        public UserProfile()
        {
            Goals = new List<Goal>();
        }

        public int UserProfileId { get; set; }
        [Required, MaxLength(64)]
        public string EmailAddress { get; set; }
        [Required, MaxLength(48)]
        public string Password { get; set; }
        [Required, MaxLength(42)]
        public string Salt { get; set; }

        [Required, MaxLength(32)]
        public string FirstName { get; set; }
        [Required, MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(128)]
        public string AvatarUrl { get; set; }

        public virtual List<Goal> Goals { get; set; }
    }
}
