using MangaUpdatesCheck.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    /// <summary>
    /// Provides methods used to search and scrape manga and novel series information from Manga-Updates.
    /// </summary>
    public interface IMangaUpdatesSearch
    {
        /// <summary>
        /// Asyncronously performs a search for the specified series and returns the parsed result.
        /// </summary>
        /// <param name="series">The search for which to search.</param>
        /// <returns>Returns an ISeriesData object representing the parsed series page.</returns>
        Task<ISeriesData> SearchAsync(string series);

        /// <summary>
        /// Asyncronously performs a search for the specified series and returns the parsed result.
        /// </summary>
        /// <param name="series">The search for which to search.</param>
        /// <param name="outputType">The format in which the search results data must be sent by the server.</param>
        /// <returns>Returns an ISeriesData object representing the parsed series page.</returns>
        Task<ISeriesData> SearchAsync(string series, SearchResultOutput outputType);

        /// <summary>
        /// Performs a search for the specified series and returns the parsed result.
        /// </summary>
        /// <param name="series">The search for which to search.</param>
        /// <returns>Returns an ISeriesData object representing the parsed series page.</returns>
        ISeriesData Search(string series);

        /// <summary>
        /// Performs a search for the specified series and returns the parsed result.
        /// </summary>
        /// <param name="series">The search for which to search.</param>
        /// <param name="outputType">The format in which the search results data must be sent by the server.</param>
        /// <returns>Returns an ISeriesData object representing the parsed series page.</returns>
        ISeriesData Search(string series, SearchResultOutput outputType);

        // IResults Search(string series, SearchResultOutput outputType);

        /// <summary>
        /// Does what Search(string) does.
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        ISeriesData FetchSeriesData(string series);

        ISeriesData FetchSeriesData(int id);

        ISeriesData FetchSeriesData(Uri uri);
    }
}
