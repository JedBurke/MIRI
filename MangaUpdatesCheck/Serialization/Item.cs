using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MangaUpdatesCheck.Serialization
{
    [DataContract(Name = "item")]
    public class Item
    {
        [DataMember(Name = "id")]
        public int Id { get; set;}
        
        [DataMember(Name = "title")]
        public string Title { get; set;}

        [DataMember(Name = "year")]
        public int? Year { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "rating")]
        public double? Rating { get; set;}

        [DataMember(Name = "v_inc")]
        public object VInc { get; set; }

        [DataMember(Name = "c_inc")]
        public object CInc { get; set; }

        [DataMember(Name = "list_type")]
        public object ListType { get; set; }
            
        [DataMember(Name = "is_associated_name")]
        public bool IsAssociatedName { get; set; }
    }
}
