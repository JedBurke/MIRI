using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MIRI.Serialization
{
    [DataContract(Name = "results", Namespace = "http://www.mangaupdates.com/xml")]
    public class Results : IResults
    {
        [DataMember(Name = "totalResults")]
        public long TotalResults { get; set; }

        [DataMember(Name = "startIndex")]
        public int StartIndex { get; set;}

        [DataMember(Name = "itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [DataMember(Name = "items")]
        public Item[] Items { get; set; }
        
        IResultItem[] IResults.Items
        {
            get
            {
                return this.Items;
            }
            set
            {
                this.Items = (Item[])value;
            }
        }
    }
}
