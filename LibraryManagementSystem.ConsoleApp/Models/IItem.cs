using System;

namespace LibraryManagementSystem.ConsoleApp.Models;

public interface IItem
{
    private static int _nextItemNumber { get; set; }
    public int ItemNumber { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public Collection Genre { get; set; }
    public Format Format { get; set; }
    public Patron? CurrentBorrower { get; set; }
    public DateTime DueDate { get; set; }
    public CircStatus CircStatus { get; set; }
    public DateTime LastCirculation { get; set; }
    
    static IItem()
    {
        Random random = new Random();
        _nextItemNumber = random.Next(10000000, 20000000);
    }
    public void SetDueDate() {}
    
}