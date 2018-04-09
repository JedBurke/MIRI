using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MangaUpdatesCheck.Serialization
{
    [DataContract(Name = "results", Namespace = "http://www.mangaupdates.com/xml")]
    public class Results : IResults
    {
        [DataMember(Name = "totalResults")]
        public long TotalResults { get; set; }

        [DataMember(Name = "startIndex")]
        public int StartIndex { get; set;}

        [DataMember(Name = "itemsPerPage")]
        public int itemsPerPage { get; set; }

        [DataMember(Name = "items")]
        public IResultItemSimple[] Items { get; set; }

    }
}
