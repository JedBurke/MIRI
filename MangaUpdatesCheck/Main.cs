using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MangaUpdatesCheck
{
    public class Series
    {
        public async Task<ISeriesData> SearchAsync(string series)
        {
            return await Task.Run(() => Search(series));
        }

        public ISeriesData Search(string series)
        {
            try
            {
                string seriesSanitized = Helpers.FormatParameters(series);

                System.Net.WebClient request = new System.Net.WebClient();

                System.Collections.Specialized.NameValueCollection param = new System.Collections.Specialized.NameValueCollection();
                param.Add("act", "series");
                param.Add("stype", "title");
                param.Add("search", seriesSanitized);
                param.Add("x", "0");
                param.Add("y", "0");
                param.Add("output", "json");
                
                byte[] response = request.UploadValues(new Uri(Properties.Resources.SeriesSearchUri), param);
                string sanitizedResponse = Encoding.UTF8.GetString(response);
                sanitizedResponse = sanitizedResponse.Remove(0, 11);
                sanitizedResponse = sanitizedResponse.Substring(0, sanitizedResponse.Length - 1);


                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(sanitizedResponse));
                Serialization.Results results = new Serialization.Results();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(results.GetType());

                results = (Serialization.Results)ser.ReadObject(ms);

                // Todo: Do a proper word-boundary comparison.
                var item = results.Items.FirstOrDefault(i => i.Title == series);
                var info = SeriesLookup(item.Id);
                
                return info;
            }
            finally
            {

            }
        }

        void request_UploadStringCompleted(object sender, System.Net.UploadStringCompletedEventArgs e)
        {
            Console.WriteLine("Upload result: {0}", e.Result);
        }

        void request_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            Console.WriteLine("Result: {0}", e.Result);
        }

        public ISeriesData SeriesLookup(int id)
        {
            return SeriesLookup(new Uri(string.Format(Properties.Resources.SeriesUriFormat, id)));
        }

        public ISeriesData SeriesLookup(Uri uri)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                string content = client.DownloadString(uri);

                return SeriesDataParser.Parse(content);
            }

        }
        
    }
}
