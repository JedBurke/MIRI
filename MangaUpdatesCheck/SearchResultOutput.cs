using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    /// <summary>
    /// The format in which the server should return the search results.
    /// </summary>
    public enum SearchResultOutput : int
    {
        Json,
        JsonP,
        Xml = 0
    }

}
