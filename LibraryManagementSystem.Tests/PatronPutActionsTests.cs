using LibraryManagementSystem.ConsoleApp.Models;

namespace LibraryManagementSystem.Tests;

public class PatronPutActionsTests
{

    [Fact]
    public void ApplyPatronUpdateToAccount_Case1_UpdatesFirstName()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "1";
        string input = "Test Name";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);

        Assert.True(testPatron.FirstName == input);
    }

    [Fact]
    public void ApplyPatronUpdateToAccount_Case2_UpdatesMiddleInitial()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "2";
        string input = "T";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.MiddleInitial == input);
    }

    [Fact]
    public void ApplyPatronUpdateToAccount_Case3_UpdatesLastName()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "3";
        string input = "Test Name";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.LastName == input);
    }

    [Fact]
    public void ApplyPatronUpdateToAccount_Case4_UpdatesDateOfBirth()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "4";
        string input = "01/01/1900";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.DateOfBirth == DateOnly.Parse(input));
    }

    [Fact]
    public void ApplyPatronUpdateToAccount_Case5_UpdatesAddress()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "5";
        string input = "Test Address";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.Address == input);
    }

    [Fact]
    public void ApplyPatronUpdateToAccount_Case6_UpdatesEmail()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "6";
        string input = "Test@test.com";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.Email == input);
    }

    [Fact]
    public void ApplyPatronUpdateToAccount_Case7_UpdatesPhoneNumber()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "7";
        string input = "999 999-9999";

        PatronPutActions.ApplyPatronUpdateToAccount(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.PhoneNumber == input);
    }
}