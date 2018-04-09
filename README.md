## Manga Information Retrieval Interface or Miri

### What it is and what it does

Miri is a library which allows the end-user to search and scrape data from manga information databases. Currently MangaUpdates is the only supported platform.

*Note:* The project has been renamed from MangaUpdatesCheck to MIRI. Some references to the old name may be present for the time being.

### Sample usage

```csharp
using MIRI;
using MIRI.Helpers;
---

MangaUpdatesSearch series = new MangaUpdatesSearch();

// Enter the name of the series you would like to search.
string searchTerm = "Houkago Play"

// Perform the search. The function will return an ISeriesData object which contains the Title, Description, Author, Illustrator, etc.
var item = series.Search(searchTerm);

// If the result is null, the search returned no results.
if (item != null)
{
	Console.WriteLine("Title: {0}", item.Title);
	Console.WriteLine("---------------");
	Console.WriteLine("Description: {0}", item.Description);
	Console.WriteLine("Series type: {0}", item.SeriesType);
	Console.WriteLine("Is completed? {0} | Is fully scanlated? {1}", Results.BoolToNaturalEnglish(item.IsCompleted), Results.BoolToNaturalEnglish(item.IsFullyScanlated));
}
else
{
	Console.WriteLine("Series not found.");
}

```

Documentation will be incrementally added and released at a later date.

### Technical

It is written in C# 5.0 and targets .NET 4.5

### Closing

This library is a work-in-progress and the API is subject to change in the future.