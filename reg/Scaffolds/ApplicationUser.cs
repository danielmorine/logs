using Microsoft.AspNetCore.Identity;
using reg.Scaffolds;
using reg.Scaffolds.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scaffolds
{
    public class ApplicationUser : IdentityUser, IScaffold<Guid>
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

        public DateTimeOffset CreatedDate { get; set; }

        [NotMapped]
        public string PasswordComparer { get; set; }

        public virtual RegistrationProcess RegistrationProcess { get; set; }

    }
}
