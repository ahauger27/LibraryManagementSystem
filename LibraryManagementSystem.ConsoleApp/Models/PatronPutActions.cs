namespace LibraryManagementSystem.ConsoleApp.Models;

public static class PatronPutActions
{
    public static void ApplyPatronUpdate(Patron patron, string fieldNumber, string input)
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
                patron.DateOfBirth = PatronPostActions.InputPatronDOB();
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
            
            case "8":
                Console.WriteLine("Returning to Menu...");
                break;
                 
            default:
                break;
        }        
    }

    public static string UpdatePatronInfo(Patron patron)
    {
        Console.WriteLine(
        $"""

        CHOOSE WHICH FIELD TO CHANGE:
        1. FIRST NAME: {patron.FirstName}
        2. MIDDLE INITIAL: {patron.MiddleInitial}
        3. LAST NAME: {patron.LastName}
        4. DATE OF BIRTH: {patron.DateOfBirth}
        5. ADDRESS: {patron.Address}
        6. EMAIL: {patron.Email}
        7. PHONE NUMBER: {patron.PhoneNumber}
        8. Quit Editing
        """
        );

        Console.Write("Which field do you want to update?: ");
        return UserActions.StringInput();
    }
}