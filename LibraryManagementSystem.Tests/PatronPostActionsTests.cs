using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.Tests;

public class PatronPostActionsTests
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
}

