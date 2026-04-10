LIBRARY MANAGEMENT SYSTEM
=========================

WHAT DOES THIS PROJECT DO?
--------------------------
This program acts as a way track circulation of items to and from patrons in a 'library' setting. Currently, the items are all books, but could easily be updated to be anything: tools, clothing, or any other items.
* Create, Retrieve, Update, and Delete patron records.
* View Catalog and Item records.
* Circulation functionality for lending Items to Patrons, and checking these items back in.

HOW TO USE THIS PROJECT
-----------------------
After forking and cloning the repo to your own machine, open the LibraryManagementSystem directory in VS Code or another IDE.

In one terminal, run the following code to launch the web api:
`dotnet run --project LibraryManagementSystem.API/LibraryManagementSystem.API.csproj`

Open another terminal and run this code to launch the console application:
`dotnet run --project LibraryManagementSystem.ConsoleApp/LibraryManagementSystem.ConsoleApp.csproj`

From here, you can open the Patrons menu, look at the Catalog of items, or check in an item directly from the main menu.

Items can be checked out while inside a Patron Record menu, or while in an Item Record menu.

After viewing all patrons/items, I would recommend taking note of a few Patron IDs and Item Numbers to test out the circulation functions.
(Ideally, there would be physical items with barcodes and the program would operate more fluidly with a scanner)


LATEST UPDATES
--------------
* Menus are more cohesive after extensive testing
* A fair amount of edge cases have been accounted for

WHAT'S NEXT?
------------
* Creating a new patron sometimes makes their ID 00001, even if that id already is in use
* Check if Patron exists when Creating/Posting

THE USE OF AI IN THIS PROJECT
-----------------------------
Needing a list of dummy patrons to test CRUD functionality, I used Claude to create the initial patron json file by referencing the custom Patron class I had created. The books were added later, and this json file was created by hand.

Any other AI use consisted of debugging my project when I could not find the culprit for the error in the code my myself. In these cases, I always made sure to understand what the generated edits did before making any changes, opting to review the generated code and then implementing the changes myself.

