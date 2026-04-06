using LibraryManagementSystem.ConsoleApp.Services;

namespace LibraryManagementSystem.Tests;

public class UserActionsTests
{
    [Fact]
    public void IsInputValid_ShouldSucceedWithValidString()
    {
        string input = "Test";
        
        bool result = UserActions.IsInputValid(input);

        Assert.True(result);
    }

    [Fact]
    public void IsInputValid_ShouldFailWithEmptyString()
    {
        string input = "";

        bool result = UserActions.IsInputValid(input);

        Assert.False(result);
    }

    [Fact]
    public void IsInputValid_ShouldFailWhenNull()
    {
        string? input = null;

        bool result = UserActions.IsInputValid(input);
        
        Assert.Null(input);
    }
}