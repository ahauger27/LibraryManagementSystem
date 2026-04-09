using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronPutActions
{
    public static string? ChoosePatronInfoToUpdate(Patron patron)
    {
        string[] validChoices = ["1", "2", "3", "4", "5", "6", "7", "8"];

        while (true)
        {
            Console.WriteLine("CURRENT PATRON INFORMATION");
            Console.WriteLine($"1. First Name: \t\t{patron.FirstName.ToUpper()}");
            Console.WriteLine($"2. Middle Initial: \t{patron.MiddleInitial.ToUpper()}");
            Console.WriteLine($"3. Last Name:  \t\t{patron.LastName.ToUpper()}");
            Console.WriteLine($"4. Date of Birth:  \t{patron.DateOfBirth}");
            Console.WriteLine($"5. Address:  \t\t{patron.Address?.ToUpper()}");
            Console.WriteLine($"6. Email Address:  \t{patron.Email?.ToLower()}");
            Console.WriteLine($"7. Phone Number:  \t{patron.PhoneNumber}"); 
            Console.WriteLine("8. GO BACK");

            Console.WriteLine("");
            Console.Write("Select a field to update (Or enter 8 to go back): ");
            string? fieldNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(fieldNumber) || !validChoices.Contains(fieldNumber))
            {
                Console.WriteLine("Please make a valid choice");
                continue;
            }
            else if (fieldNumber == "8")
            {
                return null;
            }
            else
            {
                return fieldNumber;
            }
        }
    }

    public static void ApplyPatronUpdateToAccount(Patron patron, string fieldNumber, string input)
    {
        switch (fieldNumber)
        {
            case "1":
                patron.FirstName = input;
                break;

            case "2":
                patron.MiddleInitial = input;
                break;

            case "3":
                patron.LastName = input;
                break;

            case "4":
                if (PatronPostActions.IsValidDOB(input))
                patron.DateOfBirth = DateOnly.Parse(input);
                break;

            case "5":
                patron.Address = input;
                break;

            case "6":
                patron.Email = input;
                break;

            case "7":
                patron.PhoneNumber = input;
                break;
                 
            default:
                break;
        }        
    }
}