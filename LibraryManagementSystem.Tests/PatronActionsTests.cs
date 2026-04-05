using System.Diagnostics.CodeAnalysis;
using LibraryManagementSystem.ConsoleApp.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace LibraryManagementSystem.Tests;

public class PatronActionsTests
{
    [Theory]
    [InlineData("09/28/1996")]
    [InlineData("9/28/1996")]
    [InlineData("January 1, 2026")]
    [InlineData("9-28-96")]
    public void IsValidDOB_ShouldSucceedWhenValid(string date)
    {
        Assert.True(PatronPostActions.IsValidDOB(date!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("13/01/2000")]
    [InlineData("2/30/2005")]
    public void IsValidDOB_ShouldFailWhenInvalid(string date)
    {
        Assert.False(PatronPostActions.IsValidDOB(date));
    }

    [Fact]
    public void ApplyPatronUpdate_Case1_UpdatesFirstName()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "1";
        string input = "Test Name";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);

        Assert.True(testPatron.FirstName == input);
    }

    [Fact]
    public void ApplyPatronUpdate_Case2_UpdatesMiddleInitial()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "2";
        string input = "T";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.MiddleInitial == input);
    }

    [Fact]
    public void ApplyPatronUpdate_Case3_UpdatesLastName()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "3";
        string input = "Test Name";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.LastName == input);
    }

    [Fact]
    public void ApplyPatronUpdate_Case4_UpdatesDateOfBirth()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "4";
        string input = "01/01/1900";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.DateOfBirth == DateOnly.Parse(input));
    }

    [Fact]
    public void ApplyPatronUpdate_Case5_UpdatesAddress()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "5";
        string input = "Test Address";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.Address == input);
    }

    [Fact]
    public void ApplyPatronUpdate_Case6_UpdatesEmail()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "6";
        string input = "Test@test.com";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.Email == input);
    }

    [Fact]
    public void ApplyPatronUpdate_Case7_UpdatesPhoneNumber()
    {
        Patron testPatron = new("John", "Doe", DateOnly.Parse("01/01/1900"), "W", "Main St", "John.Doe@gmail.com", "555 555-5555");
        string fieldNumber = "7";
        string input = "999 999-9999";

        PatronPostActions.ApplyPatronUpdate(testPatron, fieldNumber, input);
        
        Assert.True(testPatron.PhoneNumber == input);
    }
}