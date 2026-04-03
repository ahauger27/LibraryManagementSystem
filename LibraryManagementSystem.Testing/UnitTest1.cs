using Xunit;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Common.Services;

namespace LibraryManagementSystem.Testing;

public class UnitTest1
{
    // COME BACK TO THIS
    [Fact]
    public void CreateNewPatron_Returns_New_Patron()
    {
        Patron testPatron = PatronActions.CreateNewPatron();
        if(testPatron.FirstName != "jIM")
        {
            throw new Exception();
        }
    }

    [Fact]
    public void Date_of_birth_should_be_valid()
    {
        string? date;
        date = "09/28/1996";
        if (!DateOfBirth.IsValid(date))
            throw new Exception($"{date} is not a valid Date of Birth.");
    }

    [Fact]
    public void User_string_input_should_be_valid()
    {
        
    }

    public static bool UserInputIsValid(string input)
    {
        throw new NotImplementedException();
    }

}
