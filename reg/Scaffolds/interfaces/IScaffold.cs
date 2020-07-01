using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reg.Scaffolds.interfaces
{
    public interface IScaffold { }

    public interface IScaffold<T> : IScaffold where T : struct
    {
        DateTimeOffset CreatedDate { get; set; }
        T ID { get; set; }

    }   
}
