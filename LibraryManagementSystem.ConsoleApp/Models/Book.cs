using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Models;

public class Item
{
    private static int _nextItemNumber;
    public string ItemNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string Genre { get ; set;}
    public string Format { get; set; }
    public Patron? CurrentBorrower { get; set; }
    public CircStatus CircStatus { get; set; } = CircStatus.In;
    public DateTime LastCirculation { get; set; }
    public DateTime DueDate { get; set; }

    static Item()
    {
        _nextItemNumber = 1;
    }
    
    public Item(string Title, string AuthorName, string Genre, string Format)
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
        DateTime today = DateTime.Today;
        DueDate = today.AddDays(21);
    }
}