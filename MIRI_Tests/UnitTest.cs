using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIRI;
using MIRI.Helpers;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Linq;

namespace MIRI_Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Search_Test()
        {
            MangaUpdatesSearch miri = new MangaUpdatesSearch();
            
            var item = miri.Search("Houkago play");

            if (item != null)
            {
                Console.WriteLine("Title: {0}", item.Title);
                Console.WriteLine("---------------");
                Console.WriteLine("Description: {0}", item.Description);
                Console.WriteLine("-----");
                Console.WriteLine("Series type: {0}", item.SeriesType);
                Console.WriteLine("Is completed? {0} | Is fully scanlated? {1}", Results.BoolToNaturalEnglish(item.IsCompleted), Results.BoolToNaturalEnglish(item.IsFullyScanlated));
            }
            else
            {
                Console.WriteLine("Series not found.");
            }
        }

        
        /// Todo: Fix site search.
        [Ignore]
        [TestMethod]
        public void SiteSearch_Test()
        {
            /// Uncomment for live searching.
            // MangaUpdatesSearch muSearch = new MangaUpdatesSearch();
            // var results = muSearch.SearchSite("Need a Girl");

            var serialize = new SerializeResultsSiteSearch();
            var serializer = new SerializeResultsSiteSearch();
            var results = serializer.Serialize(Properties.Resources.Baka_Updates_Manga___Search_Results);

            //Console.WriteLine("Total Items: {0}", results.TotalResults);

            //foreach (var i in results.Items)
            //{
            //    Console.WriteLine("#{0} - {1}", i.Id, i.Title);
            //}

        }

        [TestMethod]
        public void Serialize_Test()
        {
            MIRI.Serialization.Results results = new MIRI.Serialization.Results();



            MemoryStream ms = new MemoryStream(File.ReadAllBytes("result.txt"));
            
            DataContractJsonSerializer ser = new DataContractJsonSerializer(results.GetType());

            results = (MIRI.Serialization.Results)ser.ReadObject(ms);

            //Console.WriteLine("Items per page: {0}", results.itemsPerPage);
            var result = results.Items.Cast<MIRI.Serialization.Item>().FirstOrDefault(i => string.Compare(i.Title, "Itoshi no Kana", true) == 0);
            result.Id = 0;
        }

        [TestMethod]
        public void ParseDocument_Test()
        {
            var item = new SeriesData(Properties.Resources.Baka_Updates_Manga___Itoshi_no_Kana);

            Console.WriteLine(item.Title);
            Console.WriteLine(item.Description);
            Console.WriteLine("-----");
            Console.WriteLine("Author: {0} - {1}", item.Author, item.AuthorLink);
            Console.WriteLine("Illustrator: {0} - {1}", item.Illustrator, item.IllustratorLink);
            Console.WriteLine();
            Console.WriteLine("Year: {0}", item.Year);
            Console.WriteLine("Publisher: {0} - {1}", item.Publisher, item.PublisherLink);
            Console.WriteLine("Is fully scanlated? {0}", Results.BoolToNaturalEnglish(item.IsFullyScanlated));
            Console.WriteLine("Is completed in country of origin? {0}", Results.BoolToNaturalEnglish(item.IsCompleted));
            Console.WriteLine("Is licensed in English: {0}", item.IsLicensed);
            Console.WriteLine("Series Type: {0}", item.SeriesType);


            Assert.AreEqual(2006, item.Year);
            Assert.IsFalse(item.IsLicensed);
            Assert.IsTrue(item.IsFullyScanlated);
            Assert.IsTrue(item.IsCompleted);
        }

        [TestMethod]
        public void Downloader_Test()
        {
            Assert.IsNotNull(Downloader.Instance.DownloadString(new Uri("https://www.google.com")));
        }

        [TestMethod]
        public void DownloaderPOST_Test()
        {
            var i = new System.Collections.Specialized.NameValueCollection();

            i.Add("act", "series");
            i.Add("session", string.Empty);
            i.Add("stype", "title");
            i.Add("search", MIRI.Helpers.Search.FormatParameters("Houkago Play"));
            i.Add("x", "0");
            i.Add("y", "0");
            i.Add("output", "json");

            byte[] v = Downloader.Instance.UploadValues(new Uri("https://www.mangaupdates.com/series.html"), i);

            Console.WriteLine(System.Text.Encoding.UTF8.GetString(v));
        }
    }
}
