using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    /// <summary>
    /// Represents the type of a series.
    /// </summary>
    public enum SeriesType : int
    {
        /// <summary>
        /// Represents Korean comic.
        /// </summary>
        Manhwa,

        /// <summary>
        /// Represents a Japanese comic.
        /// </summary>
        Manga,

        /// <summary>
        /// Represents a written work of ambiguous origin.
        /// </summary>
        Novel,

        /// <summary>
        /// Reserved.
        /// </summary>
        None
    }
}
