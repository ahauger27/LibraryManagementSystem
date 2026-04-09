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
Fork the repo from github to your computer, 

LATEST UPDATES
--------------
* Http get, and post work in the web api and in the console app!
* Http DELETE by id now works!
* Got SearchByPatronID() working again!
* Main Menu, Patron Search Menu, and Patron Account menu are functioning
* Json Serializer can't use interfaces in deserialization out of the box(removed for the time being)
* Figured out how to incorporate Enums in json serialization, so added the enums for format, collection, and circstatus
* Checkout is working, but is messy and needs to be cleaned up
* Checkin is working, but is messy and needs to be cleaned up

WHAT'S NEXT?
------------
* Creating a new patron sometimes makes their ID 00001, even if that id already is in use
* Date of Birth not updating...
* TEST THE UNHAPPY PATHS
* Clean up the checkin code
* Clean up the checkout code
* Need to add more items
    either create a json file with book and item data, or connect to an api
* Check if Patron exists when Creating/Posting

Nice-to-haves
* Create a script that will create random patrons so as not to need ai for this
* Fix Age display function, returns "System.Func`1[System.Int32]"

THE USE OF AI IN THIS PROJECT
-----------------------------
I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time upfront when testing early functionality.

