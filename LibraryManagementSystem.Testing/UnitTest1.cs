using Xunit;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Common.Services;

namespace LibraryManagementSystem.Testing;

public class UnitTest1
{
    // COME BACK TO THIS
    // [Fact]
    // public void CreateNewPatron_Returns_New_Patron()
    // {
    //     Patron testPatron = PatronActions.CreateNewPatron();
    //     if(testPatron.FirstName != "jIM")
    //     {
    //         throw new Exception();
    //     }
    // }

    [Fact]
    public void Date_of_birth_should_be_valid()
    {
        string? date;
        date = "09/28/1996";
        if (!DateOfBirth.IsValid(date))
            throw new Exception($"{date} is not a valid Date of Birth.");
    }

    [Fact]
    public void User_input_should_not_be_null_or_empty()
    {
        bool result = UserActions.IsInputValid("123");
        if (!result)
        {
            throw new Exception();
        }
    }
}
