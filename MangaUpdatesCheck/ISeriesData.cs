﻿using System;
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
        bool IsLicensed { get; set; }

        string Author { get; set; }
        string Illustrator { get; set; }

    }
}
