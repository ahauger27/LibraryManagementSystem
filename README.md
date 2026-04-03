This app simulates a Library Management System, with Patron accounts, Item Records, and Circulation Services.

Updates:
* Http get, and post work in the web api and in the console app!
* Http DELETE by id now works!
* Got SearchByPatronID() working again!

===================================
CATEGOREIS:
CAT 4: OF HIGHEST IMPORTANCE
CAT 3: OF SECOND HIGHEST IMPORTANCE
CAT 2: OF THIRD HIGHEST IMPORTANCE
CAT 1: OF LEAST IMPORTANCE
===================================

Next TODOS
* Check if Patron exists when Creating/Posting/Putting/Updating  CAT 2
* Implement XUnit testing project
* Add PUT console operations with user inputs
* Add CreatePatron with user inputs, not jIM bO
* going to start ID at "00001" and increment
* Creating nice menu loop for users to:
    get patron accounts
    create patron accounts
    update patron accounts
    delete patron accounts
* Find a better way to generate PatronId, because the way it works now doesn't generate ids, or they are not properly stored in the json, so can't search by id
* Fix warnings CAT 1

Future TODOS
* need books
    . either create a json file with book and item data, or connect to an api
* need to add circulation service classes
* Fix the IItem Format member

Nice-to-haves
* Create a script that will create random patrons so as not to need ai for this

The use of AI in this Project:

I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time upfront when testing early functionality.

