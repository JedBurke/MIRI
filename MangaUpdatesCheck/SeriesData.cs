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
        private string _description = string.Empty;
        private bool? _isCompleted;
        private bool? _isFullyScanlated;
        private string _seriesType = string.Empty;

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
            get
            {
                if (string.IsNullOrEmpty(this._description))
                {
                    this._description = GetDescription();
                }

                return this._description;
            }
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

        public string SeriesType
        {
            get {
                if (string.IsNullOrEmpty(_seriesType))
                {
                    _seriesType = GetSeriesType();
                }

                return _seriesType;
            }
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
            var xPathTitle = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[1]/span[1]";
            var titleNode = ParsedDocument.DocumentNode.SelectSingleNode(xPathTitle);

            if (titleNode != null)
            {
                var title = titleNode.InnerText;

                if (!string.IsNullOrWhiteSpace(title))
                {
                    title = title.Trim();
                }

                return title;
            }

            return string.Empty;
        }

        private string GetDescription()
        {
            var xPathDescription = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[2]";
            var descriptionNode = ParsedDocument.DocumentNode.SelectSingleNode(xPathDescription);

            if (descriptionNode != null)
            {
                var description = descriptionNode.InnerText;

                if (!string.IsNullOrWhiteSpace(description))
                {
                    description = description.Trim();
                }

                return description;
            }

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

        private string GetSeriesType()
        {
            string
                xPathTypeCategory = "/html/body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[3]/b",
                xPathType = "//body/div/table/tr[3]/td/table//td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[4]";

            var typeCategoryNode = ParsedDocument.DocumentNode.SelectSingleNode(xPathTypeCategory);

            if (typeCategoryNode != null && Regex.IsMatch(typeCategoryNode.InnerText, Resources.ScrapeTypeText, regexOptions))
            {
                var typeNode = ParsedDocument.DocumentNode.SelectSingleNode(xPathType);

                if (typeNode != null && !string.IsNullOrWhiteSpace(typeNode.InnerText))
                {
                    return typeNode.InnerText.Trim();
                }
            }
            
            return string.Empty;
        }

    }
}
