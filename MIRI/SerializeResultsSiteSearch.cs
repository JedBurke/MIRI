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

            //results.ToList().ForEach((e) =>
            //{
            //    /// Todo: Find a better way to exclude the header and footer table rows.
            //    if (!e.InnerText.Contains("Date") && !e.InnerText.Contains("release"))
            //    {
            //        HtmlDocument doc = new HtmlDocument();
            //        doc.LoadHtml(e.InnerHtml);

            //        Console.WriteLine(doc.DocumentNode.SelectSingleNode("//a[@title = 'Series Info']").InnerText);

            //    }

            //});

            //List<ItemGeneral> items = new List<ItemGeneral>();
            //results.ToList().ForEach((e) =>
            //{
            //    if (!e.InnerText.Contains("Date") && !e.InnerText.Contains("release"))
            //    {
            //        HtmlDocument doc = new HtmlDocument();
            //        doc.LoadHtml(e.InnerHtml);

            //        var title = doc.DocumentNode.SelectSingleNode("//a[@title = 'Series Info']").InnerText;

            //        items.Add(new ItemGeneral
            //        {
            //            Id = 0,
            //            Title = title
            //        });
            //    }

            //});

            ////ISiteSearchResults result = new SiteSearchResults(

            //result.Items = items.ToArray();

            //return result;

            List<ISiteSearchResult> items = new List<ISiteSearchResult>();

            results.ToList().ForEach((e) =>
            {
                if (!e.InnerText.Contains("Date") && !e.InnerText.Contains("release"))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(e.InnerHtml);

                    var title = doc.DocumentNode.SelectSingleNode("//a[@title = 'Series Info']").InnerText;
                    var date = DateTime.Now;
                    var volume = 0.0;
                    var chapter = 0.0;
                    var group = doc.DocumentNode.SelectSingleNode("//a[@title = 'Group Info']").InnerText;

                    // Todo: Implement series id and group id.

                    items.Add(new SiteSearchResult(title, group, date, volume, chapter));
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
