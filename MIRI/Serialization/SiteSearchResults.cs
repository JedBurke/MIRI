using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI.Serialization
{
    public class SiteSearchResult : ISiteSearchResult
    {
        protected string _series;
        protected string _group;
        protected DateTime _date;
        protected string _volume;
        protected string _chapter;
        protected Uri _seriesUri;
        protected Uri _groupUri;
        
        public static SiteSearchResult CreateResult(string series, Uri seriesUri, DateTime date, string volume, string chapter, string group, Uri groupUri)
        {
            return new SiteSearchResult()
            {
                _series = series,
                _seriesUri = seriesUri,
                _volume = volume,
                _chapter = chapter,
                _group = group,
                _groupUri = groupUri,
                _date = date
            };
        }

        public string Series
        {
            get { return _series; }
        }

        public Uri SeriesUri
        {
            get
            {
                return _seriesUri;
            }
        }

        public string Group
        {
            get { return _group; }
        }

        public Uri GroupUri
        {
            get
            {
                return _groupUri;
            }
        }

        public DateTime Date
        {
            get { return _date; }
        }

        public string Volume
        {
            get { return _volume; }
        }

        public string Chapter
        {
            get { return _chapter; }
        }



    }
}
