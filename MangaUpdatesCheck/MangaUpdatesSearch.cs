using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MangaUpdatesCheck
{
    /// <summary>
    /// Provides methods used to search and scrape data from Manga-Updates.
    /// </summary>
    public class MangaUpdatesSearch : IMangaUpdatesSearch
    {
        /// <summary>
        /// Initializes a new instance of the MangaUpdatesSearch class.
        /// </summary>
        public MangaUpdatesSearch()
        {
        }

        public async Task<ISeriesData> SearchAsync(string series)
        {
            return await Task.Run(() => Search(series));
        }

        public async Task<ISeriesData> SearchAsync(string series, SearchResultOutput outputType)
        {
            return await Task.Run(() => Search(series, outputType));
        }

        public ISeriesData Search(string series)
        {
            return Search(series, SearchResultOutput.Json);
        }

        public ISeriesData Search(string series, SearchResultOutput outputType)
        {
            byte[] response = null;
            string seriesSanitized = null;
            Serialization.IResults results = null;
            System.Collections.Specialized.NameValueCollection param = null;
            
            try
            {
                seriesSanitized = Helpers.Search.FormatParameters(series);

                param = new NameValueCollection();
                param.Add("act", "series");
                param.Add("stype", "title");
                param.Add("search", seriesSanitized);
                param.Add("x", "0");
                param.Add("y", "0");

                // Todo: Check, probably not best practice to convert an enum to string like this.
                param.Add("output", outputType.ToString().ToLower());

                response = Helpers.Downloader.Instance.UploadValues(new Uri(Properties.Resources.SeriesSearchUri), param);

                using (var serializeResults = new SerializeResults())
                {
                    results = serializeResults.Serialize(response);
                }

                // Todo: Do a proper word-boundary comparison.
                var item = results.Items.FirstOrDefault(i => i.Title == series);
                var info = FetchSeriesData(item.Id);

                return info;
            }
            finally
            {
                response = null;
                seriesSanitized = null;

                // Todo: Implement IDisposable and dispose.
                results = null;

                if (param != null)
                {
                    param.Clear();
                    param = null;
                }
            }
        }
        
        public ISeriesData FetchSeriesData(int id)
        {
            return FetchSeriesData(new Uri(string.Format(Properties.Resources.SeriesUriFormat, id)));
        }

        public ISeriesData FetchSeriesData(Uri uri)
        {
            ISeriesData parsedContent = null;
            
            parsedContent = new SeriesData(Helpers.Downloader.Instance.DownloadString(uri));
            
            return parsedContent;

        }
        
        /// Todo: Move Search(string) code to 

        public ISeriesData FetchSeriesData(string series)
        {
            throw new NotImplementedException();
        }
    }
}
