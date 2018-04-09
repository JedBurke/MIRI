using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI.Serialization
{
    public class SiteSearchResult : ISiteSearchResult
    {
        private readonly string _series;
        private readonly string _group;
        private readonly DateTime _date;
        private readonly double _volume;
        private readonly double _chapter;
        
        public SiteSearchResult(string series, string group, DateTime date, double volume, double chapter)
        {
            _series = series;
            _group = group;
            _date = date;
            _volume = volume;
            _chapter = chapter;
        }

        public string Series
        {
            get { return _series; }
        }

        public string Group
        {
            get { return _group; }
        }

        public DateTime Date
        {
            get { return _date; }
        }

        public double Volume
        {
            get { return _volume; }
        }

        public double Chapter
        {
            get { return _chapter; }
        }
    }
}
