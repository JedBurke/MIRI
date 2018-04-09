using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI
{
    /// <summary>
    /// Represents properties used to display information about series found on Manga-Updates.
    /// </summary>
    public interface ISeriesData
    {
        /// <summary>
        /// Gets the series title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the series description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets whether the series has been completed its original country.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Gets whether the series has been fully scanlated.
        /// </summary>
        bool IsFullyScanlated { get; }

        /// <summary>
        /// Gets whether the series has been licensed in English.
        /// </summary>
        bool IsLicensed { get; }

        /// <summary>
        /// Gets the name of the author.
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the name of the illustrator.
        /// </summary>
        string Illustrator { get; }
        
        /// <summary>
        /// Gets the original publisher.
        /// </summary>
        string Publisher { get; }
        
        /// <summary>
        /// Gets the type of series. (i.e Manga or Novel)
        /// </summary>
        string SeriesType { get; }

        /// <summary>
        /// Gets the year in which the series was originally published.
        /// </summary>
        double Year { get; }
        
        /// <summary>
        /// Gets the uri of the author's profile page.
        /// </summary>
        Uri AuthorLink { get; }

        /// <summary>
        /// Gets the uri of the illustrator's profile page.
        /// </summary>
        Uri IllustratorLink { get; }

        /// <summary>
        /// Gets the uri of the publisher's information page.
        /// </summary>
        Uri PublisherLink { get; }

    }
}
