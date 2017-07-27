# Manga-Updates Check

### What it is and what it does

MangaUpdatesCheck is a simple class library which allows the end-user to search and scrape data from MangaUpdates.com.

### Usage

```csharp
using MangaUpdatesCheck;
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

Documentation will be incrementally released at a later date.

### Technical

It is written C# 5.0 targets .NET 4.5

### Closing

This library is a work-in-progress and the API it subject to change in the future.