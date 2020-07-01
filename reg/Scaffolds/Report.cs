using reg.Scaffolds.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Scaffolds
{
    public class Report : IScaffold<Guid>
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string ReportDescription { get; set; }
        public string ReportSource { get; set; }
        public byte LevelTypeID { get; set; }
        public int Events { get; set; }


        [NotMapped]
        public DateTimeOffset CreatedDate { get; set; }
        
        public virtual RegistrationProcess RegistrationProcess { get; set; }
    }
}
