namespace LibraryManagementSystem.ConsoleApp.Models;

public class Patron
{
    private static int _nextPatronID;
    public string PatronID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleInitial { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<Item> ActiveLoans { get; set; } = new List<Item>();

    static Patron()
    {
        _nextPatronID = 1;
    }

    public Patron
        (string FirstName, 
        string LastName, 
        DateOnly DateOfBirth, 
        string MiddleInitial = "",
        string? Address = null,
        string? Email = null,
        string? PhoneNumber = null)
    {
        PatronID = _nextPatronID++.ToString("D5");
        this.FirstName = FirstName;
        this.MiddleInitial = MiddleInitial;
        this.LastName = LastName;
        this.DateOfBirth = DateOfBirth;
        this.Address = Address;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
    }

    public string FullName()
    {
        if (string.IsNullOrEmpty(MiddleInitial))
        {
            return $"{FirstName} {LastName}";
        }
        else
        {
            return $"{FirstName} {MiddleInitial}. {LastName}";
        }
    }

    // public int DisplayAge()
    // {
    //     DateOnly today = DateOnly.FromDateTime(DateTime.Now);
    //     int today - DateOfBirth;
    // }

    public string PrintPatronName()
    {
        return $"{FullName().ToUpper()}";
    }

    public void DisplayActiveLoans()
    {
        if (ActiveLoans.Count == 0)
        {
            Console.WriteLine($"{PrintPatronName()} has no current loans.");
        }
        else
        {
            foreach (Item item in ActiveLoans)
            {
                Console.WriteLine($"{item.ItemNumber}\t{item.PrintTitle()}");
            }
        }
    }
}