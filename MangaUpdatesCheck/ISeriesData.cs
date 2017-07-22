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

        string Author { get; }
        string Illustrator { get; }
        string Publisher { get; }
        string SeriesType { get; }

        double Year { get; }
        
        Uri AuthorLink { get; }
        Uri IllustratorLink { get; }
        Uri PublisherLink { get; }

    }
}
