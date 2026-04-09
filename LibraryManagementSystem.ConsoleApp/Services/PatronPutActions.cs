using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.ConsoleApp.Services;

public static class PatronPutActions
{
    public static string ChoosePatronInfoToUpdate(Patron patron)
    {
        string[] validChoices = { "1", "2", "3", "4", "5", "6", "7", "8" };

        while (true)
        {
            Console.WriteLine(
            $"""

            CURRENT PATRON INFORMATION
            1. First Name {patron.FirstName.ToUpper()}
            2. Middle Initial: {patron.MiddleInitial}
            3. Last Name: {patron.LastName}
            4. Date of Birth: {patron.DateOfBirth}
            5. Address: {patron.Address}
            6. Email: {patron.Email}
            7. Phone Number: {patron.PhoneNumber}
            8. GO BACK
            
            """
            );

            Console.Write("Select a field to update: ");
            string? fieldNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(fieldNumber) || !validChoices.Contains(fieldNumber))
            {
                Console.WriteLine("Please make a valid choice");
                continue;
            }
            else if (fieldNumber == "8")
            {
                Console.WriteLine("Returning To Account Menu...");
                UserActions.PressKeyToContinue();
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
                // COME BACK HERE
                // patron.DateOfBirth = PatronPostActions.fieldNumberPatronDOB();
                Console.WriteLine("WIP");
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