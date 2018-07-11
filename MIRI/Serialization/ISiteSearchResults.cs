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
        string Volume { get; }
        string Chapter { get; }

        Uri SeriesUri { get; }
        Uri GroupUri { get; }
    }
}
