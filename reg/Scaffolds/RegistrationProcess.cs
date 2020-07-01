using reg.Scaffolds.interfaces;
using Scaffolds;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Scaffolds
{
    public class RegistrationProcess : IScaffold<Guid>
    {
        public DateTimeOffset CreatedDate { get; set; }
        public Guid ID { get; set; }
        public bool IsActive { get; set; }
        public Guid ReportID { get; set; }
        public Guid ApplicationUserID { get; set; }
        public byte EnvironmentTypeID { get; set; }

        public virtual Report Report { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = new HashSet<ApplicationUser>();
        public ICollection<EnvironmentType> EnvironmentTypes { get; set; } = new HashSet<EnvironmentType>();
    }
}
