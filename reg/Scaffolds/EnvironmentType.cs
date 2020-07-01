using reg.Scaffolds.interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg.Scaffolds
{
    public class EnvironmentType : IScaffold<byte>
    {
        [NotMapped]
        public DateTimeOffset CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public byte ID { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public virtual RegistrationProcess RegistrationProcess { get; set; }
    }
}
