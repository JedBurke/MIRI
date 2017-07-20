using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    public interface ISeriesData
    {
        string Title { get; set; }
        string Description { get; set; }
        bool IsCompleted { get; }
        bool IsFullyScanlated { get; set; }
        bool IsLicensed { get; set; }

        string Author { get; set; }
        string Illustrator { get; set; }

    }
}
