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

        public int ItemsPerPage
        {
            get;
            set;
        }

        public IResultItem[] Items
        {
            get;
            set;
        }

    }
}
