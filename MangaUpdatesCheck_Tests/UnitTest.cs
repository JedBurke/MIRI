using Microsoft.VisualStudio.TestTools.UnitTesting;
using MangaUpdatesCheck;
using MangaUpdatesCheck.Helpers;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Linq;

namespace MangaUpdatesCheck_Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Search_Test()
        {
            MangaUpdatesSearch series = new MangaUpdatesSearch();
            Downloader.Instance.UserAgent = "MUC-R";

            var item = series.Search("Houkago Play");

            Console.WriteLine("Title: {0}", item.Title);
            Console.WriteLine("---------------");
            Console.WriteLine("Description: {0}", item.Description);
            Console.WriteLine("Is completed: {0} | Is fully scanlated: {1}", item.IsCompleted, item.IsFullyScanlated);
        }

        [TestMethod]
        public void Serialize_Test()
        {
            MangaUpdatesCheck.Serialization.Results results = new MangaUpdatesCheck.Serialization.Results();



            MemoryStream ms = new MemoryStream(File.ReadAllBytes("result.txt"));
            
            DataContractJsonSerializer ser = new DataContractJsonSerializer(results.GetType());

            results = (MangaUpdatesCheck.Serialization.Results)ser.ReadObject(ms);

            //Console.WriteLine("Items per page: {0}", results.itemsPerPage);
            var result = results.Items.Cast<MangaUpdatesCheck.Serialization.Item>().FirstOrDefault(i => string.Compare(i.Title, "Itoshi no Kana", true) == 0);
            result.Id = 0;


        }

        [TestMethod]
        public void ParseDocument_Test()
        {
            // Todo: Deprecate 'SeriesDataParser.Parse()' in favor of lazy parsing.
            //var item = SeriesDataParser.Parse(Properties.Resources.Baka_Updates_Manga___Itoshi_no_Kana);
            var item = new SeriesData(Properties.Resources.Baka_Updates_Manga___Itoshi_no_Kana);
            Console.WriteLine("Is fully scanlated: {0}", item.IsFullyScanlated);
        }

        [TestMethod]
        public void Downloader_Test()
        {
            Console.WriteLine(Downloader.Instance.DownloadString(new Uri("https://www.google.com")));
        }

        [TestMethod]
        public void DownloaderUpload_Test()
        {
            
        }
    }
}
