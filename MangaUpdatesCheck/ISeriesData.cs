using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    public interface ISeriesData
    {
        string Title { get; }
        string Description { get; }
        bool IsCompleted { get; }
        bool IsFullyScanlated { get; }
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
