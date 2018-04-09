using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI.Serialization
{
    public interface ISiteSearchResult
    {
        string Series { get; }
        string Group { get; }
        DateTime Date { get; }
        double Volume { get; }
        double Chapter { get; }
    }
}
