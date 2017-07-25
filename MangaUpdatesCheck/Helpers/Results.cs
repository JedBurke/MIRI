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
        /// See <see cref="BoolToNaturalEnglishLower(bool)"/> for a lower-case value.
        public static string BoolToNaturalEnglish(bool value)
        {
            return value ? "Yes" : "No";
        }

        /// <summary>
        /// Converts the boolean value to 'yes' or 'no' based on its value.
        /// </summary>
        /// <param name="value">The boolean value to be converted.</param>
        /// <returns>A lower-case string representation of the natural english value.</returns>
        /// See <see cref="BoolToNaturalEnglish(bool)"/> for a capitalized value.
        public static string BoolToNaturalEnglishLower(bool value)
        {
            return BoolToNaturalEnglish(value).ToLower();
        }

        /// <summary>
        /// Formats the properties of SeriesData object in a generic manner.
        /// </summary>
        /// <param name="data">The series information to be formatted.</param>
        /// <returns>A string of the formatted series information. </returns>
        public static string PrintSeriesData(ISeriesData data)
        {
            // Todo: Implement.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the string representation of the type of series defined in the ISeriesData object.
        /// </summary>
        /// <param name="data">The object from which the series type is to be obtained.</param>
        /// <returns></returns>
        public static string GetSeriesTypeAsString(ISeriesData data)
        {
            throw new NotImplementedException();
        }

    }
}
