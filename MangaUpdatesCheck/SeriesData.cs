using HtmlAgilityPack;
using MangaUpdatesCheck.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    public class SeriesData : ISeriesData
    {
        private bool LazyParsing;
        private HtmlDocument ParsedDocument;
        RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled;

        private string _content = string.Empty;
        private string _title = string.Empty;
        private bool? _isCompleted;
        private bool? _isFullyScanlated;

        public SeriesData()
            : this(string.Empty)
        {
        }

        public SeriesData(string content)
            : this(content, true)
        {
        }

        public SeriesData(string content, bool lazyParsing)
        {
            // Parse content.
            this._content = content;
            this.LazyParsing = lazyParsing;

            if (!string.IsNullOrWhiteSpace(content))
            {
                Parse(this._content);
            }
        }

        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(this._title))
                {
                    this._title = GetTitle();
                }

                return this._title;
                
            }
        }

        public string Description
        {
            get;
            set;
        }

        public bool IsCompleted
        {
            get
            {
                if (!this._isCompleted.HasValue)
                {
                    this._isCompleted = GetIsCompleted();
                }

                return this._isCompleted.Value;
            }
        }

        public bool IsFullyScanlated
        {
            get
            {
                if (!this._isFullyScanlated.HasValue)
                {
                    this._isFullyScanlated = GetIsFullyScanlated();
                }

                return this._isFullyScanlated.Value;
            }
        }

        public bool IsLicensed
        {
            get;
            set;
        }
        
        public string Author
        {
            get;
            set;
        }

        public string Illustrator
        {
            get;
            set;
        }

        public static SeriesData Empty;
                
        private void Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException();
            }

            ParsedDocument = new HtmlDocument();
            ParsedDocument.LoadHtml(content);

            if (!LazyParsing)
            {
                this._title = GetTitle();
                this._isCompleted = GetIsCompleted();
            }
        }

        private string GetTitle()
        {
            return string.Empty;
        }

        private string GetDescription()
        {
            return string.Empty;
        }

        private bool GetIsCompleted()
        {            
            string 
                XpathStatusInCountry = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[13]/b",
                XpathStatusInCountryComplete = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[14]";

            /// Check the series' status in its country.
            var country = ParsedDocument.DocumentNode.SelectSingleNode(XpathStatusInCountry);

            if (country != null && Regex.IsMatch(country.InnerText, Resources.ScrapeStatusInCountry, regexOptions))
            {
                // If the node has been found, check if it is complete.
                var status = ParsedDocument.DocumentNode.SelectSingleNode(XpathStatusInCountryComplete);

                if (status != null)
                {
                    return Regex.IsMatch(status.InnerText, Resources.ScrapeStatusInCountryComplete, regexOptions);
                }
            }

            return false;
        }

        private bool GetIsFullyScanlated()
        {
            string 
                XpathScanlationStatus = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[15]/b",
                XpathCompletelyScanlated = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[16]";

            // Check the scanlation status of the series.
            var scanlated = ParsedDocument.DocumentNode.SelectSingleNode(XpathScanlationStatus);

            if (scanlated != null && Regex.IsMatch(scanlated.InnerText, Resources.ScrapeScanlatedText, regexOptions))
            {
                var status = ParsedDocument.DocumentNode.SelectSingleNode(XpathCompletelyScanlated);

                if (status != null)
                {
                    return Regex.IsMatch(status.InnerText, Resources.ScrapeScanlatedConfirmText, regexOptions);
                }
            }

            return false;
        }

    }
}
