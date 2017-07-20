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
    public class SeriesDataParser
    {
        static readonly string
            XpathStatusInCountry = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[13]/b",
            XpathStatusInCountryComplete = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[14]",
            XpathScanlationStatus = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[15]/b",
            XpathCompletelyScanlated = "//body/div/table/tr[3]/td/table/tr/td[2]/table/tr[2]/td/table[2]/tr/td/div[1]/div[3]/div/div[16]";
       

        public static ISeriesData Parse(string content)
        {
            ISeriesData seriesData = new SeriesData();
            HtmlDocument document = new HtmlDocument();

            RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.Compiled;

            document.LoadHtml(content);

            /// Check the series' status in its country.
            var country = document.DocumentNode.SelectSingleNode(XpathStatusInCountry);

            if (country != null && Regex.IsMatch(country.InnerText, Resources.ScrapeStatusInCountry, regexOptions))
            {
                // If the node has been found, check if it is complete.
                var status = document.DocumentNode.SelectSingleNode(XpathStatusInCountryComplete);

                if (status != null)
                {
                    //seriesData.IsCompleted = Regex.IsMatch(status.InnerText, Resources.ScrapeStatusInCountryComplete, regexOptions);
                }
            }

            /// Check the scanlation status of the series.
            var scanlated = document.DocumentNode.SelectSingleNode(XpathScanlationStatus);

            if (scanlated != null && Regex.IsMatch(scanlated.InnerText, Resources.ScrapeScanlatedText, regexOptions))
            {
                var status = document.DocumentNode.SelectSingleNode(XpathCompletelyScanlated);

                if (status != null)
                {
                    seriesData.IsFullyScanlated = Regex.IsMatch(status.InnerText, Resources.ScrapeScanlatedConfirmText, regexOptions);
                }
            }

            document = null;

            return seriesData;
        }
    }
}
