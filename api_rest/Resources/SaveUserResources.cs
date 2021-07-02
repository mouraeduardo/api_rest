using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Resources
{
    public class SaveUserResources
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(12)]
        public string Username { get; set; }

        [Required]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required]
        public int TypeUser { get; set; }
    }
}
