using Xunit;
using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.Tests;

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
        string?[] dates = {"09/28/1996", "9/28/1996", "September 28, 1996", "9-28-96"};

        foreach (var date in dates)
        {
            Assert.True(DateOfBirth.IsValid(date));
        }
    }

    [Fact]
    public void User_input_should_not_be_null_or_empty()
    {
        bool result = UserActions.IsInputValid("123");
        if (!result)
        {
            throw new Exception("Input cannot be empty");
        }
    }

    [Theory]
    [InlineData()]
    public void TestUser()
    {
        // Arrange

        // Act
        
        // Assert
    }
}
