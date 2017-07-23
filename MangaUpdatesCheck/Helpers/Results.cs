using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck.Helpers
{
    /// <summary>
    /// Represents a collection of static methods used to format the returned values of a series search.
    /// </summary>
    public static class Results
    {
        /// <summary>
        /// Converts the boolean value to 'Yes' or 'No' based on its value.
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <returns>A string representation of the natural english value.</returns>
        /// See <see cref="BoolToNaturalEnglishLower"/> for a lower-case value.
        public static string BoolToNaturalEnglish(bool value)
        {
            return value ? "Yes" : "No";
        }

        /// <summary>
        /// Converts the boolean value to 'yes' or 'no' based on its value.
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <returns>A lower-case string representation of the natural english value.</returns>
        /// See <see cref="BoolToNaturalEnglish"/> for a capitalized value.
        public static string BoolToNaturalEnglishLower(bool value)
        {
            return BoolToNaturalEnglish(value).ToLower();
        }

    }
}
