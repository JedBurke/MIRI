using MIRI.Serialization;
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

namespace MIRI
{
    /// <summary>
    /// Provides methods used to search and scrape data from Manga-Updates.
    /// </summary>
    public class MangaUpdates : IMangaUpdates
    {
        /// <summary>
        /// Initializes a new instance of the MangaUpdatesSearch class.
        /// </summary>
        public MangaUpdates()
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
                param.Add("session", string.Empty);
                param.Add("search", seriesSanitized);
                param.Add("x", "0");
                param.Add("y", "0");

                // Todo: Check, probably not best practice to convert an enum to string like this.
                param.Add("output", outputType.ToString().ToLower());

                response = Helpers.Downloader.Instance.UploadValues(new Uri(Properties.Resources.SeriesSearchUri), param);

                if (response != null && response.Length > 0)
                {
                    using (var serializeResults = new SerializeResults())
                    {
                        results = serializeResults.Serialize(response);
                    }

                    // Todo: Do a proper word-boundary comparison.
                    var item = results.Items.FirstOrDefault(i => string.Compare(i.Title, series, true) == 0);
                    
                    var info = FetchSeriesData(item.Id);

                    return info;
                }
                else
                {
                    return null;
                }
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

        /// Todo: Move Search(string) code to FetchSeriesData
        /// Search will do a literal search and return the results as IResults.
        /// FetchSeriesData is intended to do what Search(string) currently does.

        public ISeriesData FetchSeriesData(string series)
        {
            throw new NotImplementedException();
        }


        public IResults SearchSite(string query)
        {
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("search", Helpers.Search.FormatParameters(query));
            parameters.Add("x", "0");
            parameters.Add("y", "0");

            byte[] response = Helpers.Downloader.Instance.UploadValues(new Uri("https://www.mangaupdates.com/search.html"), parameters);

            IResults results = null;

            if (response.Length > 0)
            {
                ////string resp = Encoding.UTF8.GetString(response);


                ////results.StartIndex = 0;
                ////results.TotalResults = 0;
                ////results.itemsPerPage = 0;

                ////IResultItem item = new Item()
                ////{
                ////    Id = 5
                ////};

                // Todo: Uncomment
                //results = new SerializeResultsSiteSearch().Serialize(response);

            }


            return results;
        }

        public ISiteSearchResult[] PerformSiteSearch(string query)
        {
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("search", query);
            parameters.Add("x", "0");
            parameters.Add("y", "0");

            byte[] response = Helpers.Downloader.Instance.UploadValues(new Uri("https://www.mangaupdates.com/search.html"), parameters);

            ISiteSearchResult[] results = null;

            if (response.Length > 0)
            {
                results = new SiteSearchResultsSerializer().Serialize(response);
            }


            return results;
        }

        public string GetSearchByOptionParameter(SearchByOption option)
        {
            switch (option)
            {
                case SearchByOption.Description:
                    return "description";
                    
                case SearchByOption.Title:
                default:
                    return "title";
            }
        }
    }

    public enum SearchByOption : int
    {
        Title,
        Description
    }

    public class SearchResult : ISeriesResult
    {
        public SearchResult()
        {

        }

        public string[] Genre
        {
            get { throw new NotImplementedException(); }
        }

        public int Year
        {
            get { throw new NotImplementedException(); }
        }

        public double Rating
        {
            get { throw new NotImplementedException(); }
        }

        public string Series
        {
            get { throw new NotImplementedException(); }
        }

        public Uri SeriesUri
        {
            get { throw new NotImplementedException(); }
        }
    }

    public interface IBasicResult
    {
        string Series { get; }
        Uri SeriesUri { get; }
    }

    public interface IReleaseResult : IBasicResult
    {
        DateTime Date { get; }
        string Group { get; }
        Uri GroupUri { get; }

        string Chapter { get; }
        string Volume { get; }
    }

    public interface ISeriesResult : IBasicResult
    {
        string[] Genre { get; }
        int Year { get; }
        double Rating { get; }
    }

}
