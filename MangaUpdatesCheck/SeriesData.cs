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
    /// <summary>
    /// Represents an implementation for the ISeriesData interface employing XPath to scrape and display series data from Manga-Updates.
    /// </summary>
    public class SeriesData : ISeriesData
    {
        /// <summary>
        /// Gets the string representation of the HTML anchor's <c>Hyperlink</c> attribute.
        /// </summary>
        private static readonly string ATTRIBUTE_HREF = "href";

        /// <summary>
        /// Represents a SeriesData object which contains no data.
        /// </summary>
        public static readonly SeriesData Empty = null;

        /// <summary>
        /// Gets whether lazy parsing is enabled for the series properties.
        /// </summary>
        public bool LazyParsing
        {
            get
            {
                return _lazyParsing;
            }
        }

        /// <summary>
        /// The backing-field for the LazyParsing property.
        /// </summary>
        /// See <see cref="LazyParsing"/>.
        private readonly bool _lazyParsing;

        [Obsolete("Consider removing in favor of ParsedDocumentRootNode.")]
        private HtmlDocument ParsedDocument;

        /// <summary>
        /// Gets the root node of the parsed document as per the document content.
        /// </summary>
        private readonly HtmlNode ParsedDocumentRootNode;

        /// <summary>
        /// The default regular expression options used to compare scrapped values against the expected values.
        /// </summary>
        private readonly RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled;

        private string _documentContent = string.Empty;

        /// <summary>
        /// The backing-field for the Title property.
        /// </summary>
        /// See <see cref="Title"/>.
        private string _title = string.Empty;

        /// <summary>
        /// The backing-field for the Description property.
        /// </summary>
        /// See <see cref="Description"/>.
        private string _description = string.Empty;

        /// <summary>
        /// The backing-field for the IsCompleted property.
        /// </summary>
        /// See <see cref="IsCompleted"/>.
        private bool? _isCompleted;

        /// <summary>
        /// The backing-field for the IsFullyScanlated property.
        /// </summary>
        /// See <see cref="IsFullyScanlated"/>.
        private bool? _isFullyScanlated;

        /// <summary>
        /// The backing-field for the SeriesType property.
        /// </summary>
        /// See <see cref="SeriesType"/>.
        private string _seriesType = string.Empty;

        /// <summary>
        /// The backing-field for the Author property.
        /// </summary>
        /// See <see cref="Author"/>.
        private string _author = string.Empty;

        /// <summary>
        /// The backing-field for the Illustrator property.
        /// </summary>
        /// See <see cref="Illustrator"/>.
        private string _illustrator = string.Empty;

        /// <summary>
        /// The backing-field for the Publisher property.
        /// </summary>
        /// See <see cref="Publisher"/>.
        private string _publisher = string.Empty;

        /// <summary>
        /// The backing-field for the Year property.
        /// </summary>
        /// See <see cref="Year"/>.
        private double _year = -1;

        /// <summary>
        /// The backing-field for the AuthorLink property.
        /// </summary>
        /// See <see cref="AuthorLink"/>.
        private Uri _authorLink = null;

        /// <summary>
        /// The backing-field for the IllustratorLink property.
        /// </summary>
        /// See <see cref="IllustratorLink"/>.
        private Uri _illustratorLink = null;

        /// <summary>
        /// The backing-field for the PublisherLink property.
        /// </summary>
        /// See <see cref="PublisherLink"/>.
        private Uri _publisherLink = null;

        /// <summary>
        /// Initializes a new instance of the SeriesData class which is empty.
        /// </summary>
        public SeriesData()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SeriesData class with the series page.
        /// </summary>
        /// <param name="documentContent">The HTML document of the series page to be parsed.</param>
        public SeriesData(string documentContent)
            : this(documentContent, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SeriesData class with the series page and whether to parse lazily.
        /// </summary>
        /// <param name="documentContent">The HTML document of the series page to be parsed.</param>
        /// <param name="lazyParsing">Sets whether to parse all properties upon initialization.</param>
        public SeriesData(string documentContent, bool lazyParsing)
        {
            // Parse content.
            this._documentContent = documentContent;
            this._lazyParsing = lazyParsing;

            if (!string.IsNullOrWhiteSpace(documentContent))
            {
                ParsedDocument = new HtmlDocument();
                ParsedDocument.LoadHtml(documentContent);

                ParsedDocumentRootNode = ParsedDocument.DocumentNode;

                Parse(this._documentContent, LazyParsing);
            }
        }

        /// <summary>
        /// Gets the series title.
        /// </summary>
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

        /// <summary>
        /// Gets the series description1.
        /// </summary>
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

        /// <summary>
        /// Gets whether the series has been completed in its origin country.
        /// </summary>
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

        /// <summary>
        /// Gets whether the series has been fully scanlated.
        /// </summary>
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

        /// <summary>
        /// Gets whether the series has been licensed in English.
        /// </summary>
        public bool IsLicensed
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the series' author.
        /// </summary>
        public string Author
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._author))
                {
                    this._author = GetAuthor();
                }

                return this._author;
            }
        }

        /// <summary>
        /// Gets the name of the series' illustrator or artist.
        /// </summary>
        public string Illustrator
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._illustrator))
                {
                    this._illustrator = GetIllustrator();
                }

                return this._illustrator;
            }
        }

        /// <summary>
        /// Gets the type of series. (i.e Manga or Novel)
        /// </summary>
        public string SeriesType
        {
            get
            {
                if (string.IsNullOrEmpty(_seriesType))
                {
                    _seriesType = GetSeriesType();
                }

                return _seriesType;
            }
        }

        /// <summary>
        /// Gets the original publisher.
        /// </summary>
        public string Publisher
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._publisher))
                {
                    this._publisher = GetPublisher();
                }

                return this._publisher;
            }
        }

        /// <summary>
        /// Gets the year in which the series was originally published.
        /// </summary>
        public double Year
        {
            get
            {
                if (this._year == -1)
                {
                    this._year = GetYear();
                }

                return this._year;
            }
        }

        /// <summary>
        /// Gets the uri of the author's profile page.
        /// </summary>
        public Uri AuthorLink
        {
            get
            {
                if (this._authorLink == null)
                {
                    this._authorLink = GetAuthorLink();
                }

                return this._authorLink;
            }
        }

        /// <summary>
        /// Gets the uri of the illustrator's profile page.
        /// </summary>
        public Uri IllustratorLink
        {
            get
            {
                if (this._illustratorLink == null)
                {
                    this._illustratorLink = GetIllustratorLink();
                }

                return this._illustratorLink;
            }
        }

        /// <summary>
        /// Gets the uri of the publisher's information page.
        /// </summary>
        public Uri PublisherLink
        {
            get
            {
                if (this._publisherLink == null)
                {
                    this._publisherLink = GetPublisherLink();
                }

                return this._publisherLink;
            }
        }

        private string GetTitle()
        {
            var xPathTitle = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[1]/span[1]";
            return ScrapeInformationWithoutHeader(xPathTitle);

        }

        private string GetDescription()
        {
            var xPathDescription = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[2]";
            string value = ScrapeInformationWithoutHeader(xPathDescription);

            value = System.Net.WebUtility.HtmlDecode(value);
            value = Regex.Replace(value, "<!--(.[^(-->)]*)-->", "", regexOptions);

            return value;
        }

        private bool GetIsCompleted()
        {
            string
                XpathStatusInCountry = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[13]/b",
                XpathStatusInCountryComplete = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[14]";

            string status = ScrapeInformation(XpathStatusInCountry, XpathStatusInCountryComplete, Resources.ScrapeStatusInCountry);
            bool value = false;

            if (!string.IsNullOrWhiteSpace(status))
            {
                value = Regex.IsMatch(status, Resources.ScrapeStatusInCountryComplete, regexOptions);
            }

            return value;
        }

        private bool GetIsFullyScanlated()
        {
            string
                XpathScanlationStatus = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[15]/b",
                XpathCompletelyScanlated = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[16]";

            string status = ScrapeInformation(XpathScanlationStatus, XpathCompletelyScanlated, Resources.ScrapeScanlatedText);
            bool value = false;

            // Todo: Move to ScrapeInformation.

            if (!string.IsNullOrWhiteSpace(status))
            {
                value = Regex.IsMatch(status, Resources.ScrapeScanlatedConfirmText, regexOptions);
            }

            //// Check the scanlation status of the series.
            //var scanlated = ParsedDocument.DocumentNode.SelectSingleNode(XpathScanlationStatus);

            //if (scanlated != null && Regex.IsMatch(scanlated.InnerText, Resources.ScrapeScanlatedText, regexOptions))
            //{
            //    var status = ParsedDocument.DocumentNode.SelectSingleNode(XpathCompletelyScanlated);

            //    if (status != null)
            //    {
            //        return Regex.IsMatch(status.InnerText, Resources.ScrapeScanlatedConfirmText, regexOptions);
            //    }
            //}

            return value;
        }

        private string GetSeriesType()
        {
            string
                xPathTypeCategory = "/html/body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[3]/b",
                xPathType = "//body/div/table/tr[3]/td/table//td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[4]";

            return ScrapeInformation(xPathTypeCategory, xPathType, Resources.ScrapeTypeText);
        }

        private string GetAuthor()
        {
            string
                xPathAuthorHeader = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[11]/b",
                xPathAuthor = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[12]/a/u";

            return ScrapeInformation(xPathAuthorHeader, xPathAuthor, Resources.ScrapeAuthorHeader);

        }

        private string GetIllustrator()
        {
            string
                xPathIllustratorHeader = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[13]/b",
                xPathIllustrator = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[14]/a/u";

            return ScrapeInformation(xPathIllustratorHeader, xPathIllustrator, Resources.ScrapeIllustratorHeader);

        }

        private string GetPublisher()
        {
            string
                xPathPublisherCategory = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[17]/b",
                xPathPublisher = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[18]/a/u";

            return ScrapeInformation(xPathPublisherCategory, xPathPublisher, Resources.ScrapePublisherHeader);

        }

        /// <summary>
        /// Scrapes the year the series was originally published.
        /// </summary>
        /// <returns>The scrapped value as a double.</returns>
        private double GetYear()
        {
            double publishedYear = 0;

            string
                xPathYearCategory = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[15]/b",
                xPathYear = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[16]";

            string value = ScrapeInformation(xPathYearCategory, xPathYear, Resources.ScrapeYearText);

            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!double.TryParse(value, out publishedYear))
                {
                    // Signify that it has failed.
                    publishedYear = -1;
                }
            }

            return publishedYear;
        }

        /// <summary>
        /// Scrapes the author's page link from the parsed document.
        /// </summary>
        /// <returns>The scraped value as a Uri object.</returns>
        private Uri GetAuthorLink()
        {
            string
                xPathAuthorHeader = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[11]/b",
                xPathAuthor = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[12]/a";

            string value = ScrapeAttributeInformation(xPathAuthorHeader, xPathAuthor, Resources.ScrapeAuthorHeader, ATTRIBUTE_HREF);

            return value != null ? new Uri(value) : null;
        }

        /// <summary>
        /// Scrapes the illustrator's page link from the parsed document.
        /// </summary>
        /// <returns>The scrapped value as a Uri object.</returns>
        private Uri GetIllustratorLink()
        {
            string
                xPathIllustratorHeader = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[13]/b",
                xPathIllustrator = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[14]/a";

            string value = ScrapeAttributeInformation(xPathIllustratorHeader, xPathIllustrator, Resources.ScrapeIllustratorHeader, ATTRIBUTE_HREF);

            return value != null ? new Uri(value) : null;
        }

        /// <summary>
        /// Scrapes the publisher link from the parsed document.
        /// </summary>
        /// <returns>The scrapped value as a Uri object.</returns>
        private Uri GetPublisherLink()
        {
            string
                xPathPublisherHeader = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[17]/b",
                xPathPublisher = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[4]/div/div[18]/a";

            string value = ScrapeAttributeInformation(xPathPublisherHeader, xPathPublisher, Resources.ScrapePublisherHeader, ATTRIBUTE_HREF);

            return value != null ? new Uri(value) : null;
        }

        /// <summary>
        /// Parses the document content as an HtmlDocument object and scrapes the values from it.
        /// </summary>
        /// <param name="documentContent">The string representation of the content which is to be parsed.</param>
        /// <param name="lazyParsing">Lazily parses the properties.</param>
        private void Parse(string documentContent, bool lazyParsing)
        {
            if (string.IsNullOrEmpty(documentContent))
            {
                throw new ArgumentNullException();
            }

            //ParsedDocument = new HtmlDocument();
            //ParsedDocument.LoadHtml(documentContent);

            //ParsedDocumentRootNode = ParsedDocument.DocumentNode;

            /// Sets the backing fields of the respective properties if the lazy loading
            /// option has been set to false.
            if (!lazyParsing)
            {
                this._title = GetTitle();
                this._author = GetAuthor();
                this._illustrator = GetIllustrator();
                this._isCompleted = GetIsCompleted();
                this._isFullyScanlated = GetIsFullyScanlated();
                this._description = GetDescription();
                this._seriesType = GetSeriesType();
                this._year = GetYear();
                this._publisher = GetPublisher();

                this._authorLink = GetAuthorLink();
                this._illustratorLink = GetIllustratorLink();
                this._publisherLink = GetPublisherLink();
            }
        }

        // Todo: Employ inversion to avoid repeating buggy code.

        private string ScrapeInformationWithoutHeader(string valueXpath)
        {
            return ScrapeInformation(string.Empty, valueXpath, string.Empty);
        }

        private string ScrapeInformation(string categoryXpath, string valueXpath, string expectedCategoryText)
        {
            if (!string.IsNullOrWhiteSpace(valueXpath))
            {
                if (!string.IsNullOrWhiteSpace(categoryXpath))
                {
                    var categoryNode = ParsedDocumentRootNode.SelectSingleNode(categoryXpath);
                    if (categoryNode == null || !Regex.IsMatch(categoryNode.InnerText, Regex.Escape(expectedCategoryText), regexOptions))
                    {
                        return string.Empty;
                    }
                }

                var valueNode = ParsedDocumentRootNode.SelectSingleNode(valueXpath);

                /// Todo: Consider placing in an optional 'pre-parsing' method.
                var content = valueNode.InnerHtml;
                content = Regex.Replace(content, "<br>|<br/>|<br></br>", Environment.NewLine, regexOptions);

                valueNode.InnerHtml = content;

                if (valueNode != null && !string.IsNullOrWhiteSpace(valueNode.InnerText))
                {
                    string value = valueNode.InnerText;
                    value = value.Trim();

                    return value;
                }
            }


            return string.Empty;
        }

        private HtmlNode ScrapeInformationAsHtmlNode(string categoryXpath, string valueXpath, string expectedCategoryText)
        {
            var categoryNode = ParsedDocumentRootNode.SelectSingleNode(categoryXpath);
            if (categoryNode != null && Regex.IsMatch(categoryNode.InnerText, Regex.Escape(expectedCategoryText), regexOptions))
            {
                var valueNode = ParsedDocumentRootNode.SelectSingleNode(valueXpath);

                return valueNode;
            }

            return null;
        }

        private string ScrapeAttributeInformation(string categoryXpath, string valueXpath, string expectedCategoryText, string attribute)
        {
            var linkNode = ScrapeInformationAsHtmlNode(categoryXpath, valueXpath, expectedCategoryText);
            if (linkNode != null && linkNode.HasAttributes)
            {
                string value = linkNode.Attributes[attribute].Value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    value = value.Trim();
                    return value;
                }

            }

            return null;

        }

    }
}
