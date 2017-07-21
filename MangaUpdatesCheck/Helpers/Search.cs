using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaUpdatesCheck.Helpers
{
    public class Search
    {
        /// <summary>
        /// Formats the parameter for use with POST or GET web requests.
        /// </summary>
        /// <param name="input">The content to be formatted.</param>
        /// <returns></returns>
        public static string FormatParameters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            else
            {
                /// Remove trailing whitespace prior to regex-replace.
                input = input.Trim();

                /// The search parameters require '+' in place of spaces. Replace multiple whitespace with a single '+'.
                input = Regex.Replace(input, "\\s+", "+");

                return input;
            }
        }

    }
}
