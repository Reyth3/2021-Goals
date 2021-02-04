using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Shared
{
    public class RegistrationVM
    {
        [Required, StringLength(64, MinimumLength = 12)]
        public string EmailAddress { get; set; }
        [Required, StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }
    }
}
