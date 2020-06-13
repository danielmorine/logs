using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scaffolds
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public Guid ID
        {
            get
            {
                return Guid.Parse(this.Id);
            }
            set
            {
                this.Id = value.ToString();
            }
        }

        public string Name { get; set; }

        [NotMapped]
        public string PasswordComparer { get; set; }

    }
}
