namespace LibraryManagementSystem.Common.Models;

public class Patron
{
    private static int _nextPatronID;
    public int PatronID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleInitial { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<IItem> ActiveLoans { get; } = new List<IItem>();

    static Patron()
    {
        Random random = new Random();
        _nextPatronID = random.Next(10000000, 20000000);
    }

    public Patron
        (string FirstName, 
        string LastName, 
        DateTime DateOfBirth, 
        string MiddleInitial = "",
        string? Address = null,
        string? Email = null,
        string? PhoneNumber = null)
    {
        PatronID = _nextPatronID++;
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
            return $"{FirstName} {MiddleInitial} {LastName}";
        }
    }
    public int DisplayAge()
    {
        DateTime dateNow = DateTime.Now;
        return dateNow.Year - DateOfBirth.Year;
    }

    public string PrintPatronName()
    {
        return $"{FullName()}";
    }

    public void DisplayActiveLoans()
    {
        if (ActiveLoans.Count == 0)
        {
            Console.WriteLine($"{PrintPatronName()} has no current loans.");
        }
        else
        {
            Console.WriteLine($"{PrintPatronName()}'s current loans:");
            foreach (IItem item in ActiveLoans)
            {
                Console.WriteLine($"-{item.Title}");

            }
        }
    }
}