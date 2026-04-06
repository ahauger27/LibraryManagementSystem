LIBRARY MANAGEMENT SYSTEM
=========================

WHAT DOES THIS PROJECT DO?
--------------------------
This program acts as a way allow users in a public facing environment to access patron and item records, and circulate items to and from patrons.
* Create, Retrieve, Update, and Delete Patron information and records.
* View Catalog and Item records.
* Circulation functionality for lending Items to Patrons.

HOW TO RUN THIS PROJECT
-----------------------


LATEST UPDATES
--------------
* Http get, and post work in the web api and in the console app!
* Http DELETE by id now works!
* Got SearchByPatronID() working again!
* Main Menu, Patron Search Menu, and Patron Account menu are functioning
* Json Serializer can't use interfaces in deserialization, so I had to do away with the IItem interface

WHAT'S NEXT?
------------
* Can't seem to use enumerables in json deserialization, so look at refactoring Genre, Format, CircStatus in the Item Class to strings
* Need to add books
    either create a json file with book and item data, or connect to an api
* Need to add book endpoints to api
* Flesh out XUnit testing project
* Check if Patron exists when Creating/Posting
* Say why string input is invalid.
    Empty
    Wrong type of characters
    etc
* Fix Age display function, returns "System.Func`1[System.Int32]"

Future TODOS
* need to add circulation service classes

Nice-to-haves
* Create a script that will create random patrons so as not to need ai for this

THE USE OF AI IN THIS PROJECT
-----------------------------
I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time upfront when testing early functionality.

