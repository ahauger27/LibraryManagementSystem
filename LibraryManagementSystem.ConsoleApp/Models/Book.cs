using System;

namespace LibraryManagementSystem.ConsoleApp.Models;

public class Book : IItem
{
    private static int _nextItemNumber { get; set; }
    public int ItemNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public Collection Genre { get ; set;}
    public Format Format { get; set; }
    public Patron? CurrentBorrower { get; set; }
    public CircStatus CircStatus { get; set; } = CircStatus.In;
    public DateTime LastCirculation { get; set; }
    public DateTime DueDate { get; set; }

    public Book(string title, string author, Collection genre, Format format)
    {
        Title = title;
        AuthorName = author;
        Genre = genre;
        Format = format;
    }

    public void DisplayTitle()
    {
        System.Console.WriteLine($"Title: {Title}");
    }

    public void SetDueDate()
    {
        DateTime today = DateTime.Today;
        DueDate = today.AddDays(21);
    }
}