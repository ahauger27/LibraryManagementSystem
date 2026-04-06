using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.ConsoleApp.Models;

public interface IItem
{
    private static int _nextItemNumber;
    public string ItemNumber { get; set; }
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
        _nextItemNumber = 1;
    }
}