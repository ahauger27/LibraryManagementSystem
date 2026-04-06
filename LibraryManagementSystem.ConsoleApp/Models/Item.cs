using System.Text.Json.Serialization;
using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Models;

public class Item
{
    private static int _nextItemNumber;
    public string ItemNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public Patron? CurrentBorrower { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Collection Genre { get ; set;}

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Format Format { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CircStatus CircStatus { get; set; } = CircStatus.In;        
    
    public DateOnly LastCirculation { get; set; }
    public DateOnly DueDate { get; set; }

    static Item()
    {
        _nextItemNumber = 1;
    }
    
    public Item(string Title, string AuthorName, Collection Genre, Format Format)
    {
        ItemNumber = "1" + _nextItemNumber++.ToString("D4");
        this.Title = Title;
        this.AuthorName = AuthorName;
        this.Genre = Genre;
        this.Format = Format;
    }

    public void DisplayTitle()
    {
        Console.WriteLine($"Title: {Title}");
    }

    public void SetDueDate()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        DueDate = today.AddDays(21);
    }
}