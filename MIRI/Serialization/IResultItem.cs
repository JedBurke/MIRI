using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI.Serialization
{
    public interface IResultItem : IResultItemSimple
    {
        int? Year { get; set; }

        string Genre { get; set; }

        double? Rating { get; set; }

        object VInc { get; set; }

        object CInc { get; set; }

        object ListType { get; set; }

        bool IsAssociatedName { get; set; }
    }
}
