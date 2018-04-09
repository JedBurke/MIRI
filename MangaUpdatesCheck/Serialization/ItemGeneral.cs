using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck.Serialization
{
    public class ItemGeneral : IResultItemSimple
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
    }
}
