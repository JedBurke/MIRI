using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck.Serialization
{
    /// <summary>
    /// Represents an interface for storing simple data from search results.
    /// </summary>
    public interface IResultItemSimple
    {
        int Id { get; set; }
        string Title { get; set; }
    }
}
