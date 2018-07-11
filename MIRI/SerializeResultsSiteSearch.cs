using HtmlAgilityPack;
using MIRI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI
{
    public class SerializeResultsSiteSearch : ISerializeResults
    {
        public Serialization.ISiteSearchResult[] Serialize(string data)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(data);

            var results = document.DocumentNode.SelectNodes("//td[@id = 'main_content']/table//table/tr");
            int resultsCount = results.Count - 2;

            List<ISiteSearchResult> items = new List<ISiteSearchResult>();

            results.ToList().ForEach((e) =>
            {
                if (!e.InnerText.Contains("Date") && !e.InnerText.Contains("release"))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(e.InnerHtml);

                    var seriesElement = doc.DocumentNode.SelectSingleNode("//a[@title = 'Series Info']");
                    var groupElement = doc.DocumentNode.SelectSingleNode("//a[@title = 'Group Info']");

                    var series = seriesElement.InnerText;
                    Uri seriesUri = null;
                    var date = DateTime.Now;
                    var volume = doc.DocumentNode.SelectSingleNode("//td[3]").InnerText;
                    var chapter = doc.DocumentNode.SelectSingleNode("//td[4]").InnerText;
                    var group = groupElement.InnerText;
                    Uri groupUri = null;

                    Uri.TryCreate(seriesElement.GetAttributeValue("href", null), UriKind.Absolute, out seriesUri);
                    Uri.TryCreate(groupElement.GetAttributeValue("href", null), UriKind.Absolute, out groupUri);
                    DateTime.TryParse(doc.DocumentNode.SelectSingleNode("//td[1]").InnerText, out date);

                    items.Add(SiteSearchResult.CreateResult(series, seriesUri, date, volume, chapter, group, groupUri));
                }

            });

            return items.ToArray();
        }

        public Serialization.ISiteSearchResult[] Serialize(byte[] data)
        {
            /// Since we're using HAP to 'serialize' the data, we'll convert binary data to string and not vice-versa.            
            return Serialize(Encoding.UTF8.GetString(data));
        }

        public void Dispose()
        {
        }

        IResults ISerializeResults.Serialize(string data)
        {
            throw new NotImplementedException();
        }

        IResults ISerializeResults.Serialize(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
