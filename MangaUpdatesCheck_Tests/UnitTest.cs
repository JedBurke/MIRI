using Microsoft.VisualStudio.TestTools.UnitTesting;
using MangaUpdatesCheck;
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
            Series series = new Series();
            var item = series.Search("Itoshi no Kana");
            Console.WriteLine("Is fully scanlated: {0}", item.IsFullyScanlated);
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
            var item = SeriesDataParser.Parse(Properties.Resources.Baka_Updates_Manga___Itoshi_no_Kana);
            Console.WriteLine("Is fully scanlated: {0}", item.IsFullyScanlated);
        }
    }
}
