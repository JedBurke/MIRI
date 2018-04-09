using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI.Serialization
{
    public class ResultsGeneral : IResults
    {
        public long TotalResults
        {
            get;
            set;
        }

        public int StartIndex
        {
            get;
            set;
        }

        public int itemsPerPage
        {
            get;
            set;
        }

        public IResultItemSimple[] Items
        {
            get;
            set;
        }

    }
}
