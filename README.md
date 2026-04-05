LIBRARY MANAGEMENT SYSTEM
=========================
This app simulates a Library Management System, with Patron accounts, Item Records, and Circulation Services.

Updates:
* Http get, and post work in the web api and in the console app!
* Http DELETE by id now works!
* Got SearchByPatronID() working again!

Next TODOS
* Check if Patron exists when Creating/Posting/Putting/Updating  CAT 2
* Flesh out XUnit testing project
* going to start ID at "00001" and increment
* Creating nice menu loop for users to:
    get patron accounts
    create patron accounts
    update patron accounts
    delete patron accounts
* Find a better way to generate PatronId, because the way it works now doesn't generate ids, or they are not properly stored in the json, so can't search by id
* Say why string input is invalid.
    Empty
    Wrong type of characters
    etc
* Fix Age display function, returns "System.Func`1[System.Int32]"

Future TODOS
* need books
    . either create a json file with book and item data, or connect to an api
* need to add circulation service classes
* Fix the IItem Format member

Nice-to-haves
* Create a script that will create random patrons so as not to need ai for this

The use of AI in this Project:

I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time upfront when testing early functionality.

