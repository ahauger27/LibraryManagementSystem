using LibraryManagementSystem.ConsoleApp.Models;
using LibraryManagementSystem.ConsoleApp.Services;
using LibraryManagementSystem.ConsoleApp.Resources;

namespace LibraryManagementSystem.Tests;

public class CirculateTests
{
    [Fact]  
    public void CheckInItem_Should_Successfully_Make_Item_Available_And_Removes_Item_From_ActiveLoans()
    {
        Patron testPatron = new("John", "Doe", new DateOnly(), "M")
        {
            PatronID = "00000"
        };

        Item testItem = new("TestBook", "Jim Bob", Collection.Fiction, Format.Hardcover)
        {
            ItemNumber = "10000",
            CircStatus = CircStatus.Out  
        };

        Circulate.CheckInItem(testPatron, testItem);

        Assert.Multiple(
            () => Assert.Equal(CircStatus.In, testItem.CircStatus),
            () => Assert.DoesNotContain(testItem, testPatron.ActiveLoans)
        );
    }
    
    [Fact]
    public void CheckOutItem_Should_Successfully_Make_Item_Unavailable_And_Adds_Item_To_ActiveLoans()
    {
        Patron testPatron = new("John", "Doe", new DateOnly(), "M")
        {
            PatronID = "00000"
        };

        Item testItem = new("TestBook", "Jim Bob", Collection.Fiction, Format.Hardcover)
        {
            ItemNumber = "10000",
            CircStatus = CircStatus.In  
        };

        Circulate.CheckOutItem(testPatron, testItem);

        Assert.Multiple(
            () => Assert.Equal(CircStatus.Out, testItem.CircStatus),
            () => Assert.Contains(testItem, testPatron.ActiveLoans)
        );
    }

    [Fact]
    public void AddToActiveLoans_Should_Successfully_Add_Available_Item_To_PatronActiveLoans()
    {
        Patron testPatron = new("John", "Doe", new DateOnly(), "M")
        {
            PatronID = "00000"
        };

        Item testItem = new("TestBook", "Jim Bob", Collection.Fiction, Format.Hardcover)
        {
            ItemNumber = "10000",  
        };

        Circulate.AddToActiveLoans(testPatron, testItem);

        Assert.Contains(testItem, testPatron.ActiveLoans);
    }

    [Fact]
    public void AddToActiveLoans_Should_Fail_To_Add_Unavailable_Item_To_PatronActiveLoans()
    {
        Patron testPatron = new("FirstName", "LastName", new DateOnly(), "M");
        Item testItem = new("TestBook", "Jim Bob", Collection.Fiction, Format.Hardcover)
        {
            CircStatus = CircStatus.Out
        };

        Circulate.AddToActiveLoans(testPatron, testItem);

        Assert.DoesNotContain(testItem, testPatron.ActiveLoans);
    }

    [Fact] 
    public void RemoveFromActiveLoans_Should_Successfully_Remove_Item_From_PatronActiveLoans()
    {
        Patron testPatron = new("FirstName", "LastName", new DateOnly(), "M");
        Item testItem = new("TestBook", "Jim Bob", Collection.Fiction, Format.Hardcover);

        Circulate.AddToActiveLoans(testPatron, testItem);

        Circulate.RemoveFromActiveLoans(testPatron, testItem.ItemNumber);

        Assert.DoesNotContain(testItem, testPatron.ActiveLoans);
    }
}
  
