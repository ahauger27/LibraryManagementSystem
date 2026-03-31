This app simulates a Library Management System, with Patron accounts, Item Records, and Circulation Services.

Updates:
* Http get, and post work in the web api and in the console app! adding POST with user input next!
* 

todo

* Add basic delete and put console operations
* HTTP Put, get it figured out, be able to update accounts
* Find a better way to generate PatronId, because the way it works now doesn't generate ids, or they are not properly stored in the json, so can't search by id
    going to start ID at 1 and increment, with leading 0's
* Creating menu loop for users to:
    create accounts
    update accounts
    delete accounts

* need books
    . either create a json file with book and item data, or connect to an api
* need to add circulation service classes
* Fix the IItem Format member

Nice-to-haves
* Create a script that will create random patrons so as not to need ai for this

The use of AI in this Project:

I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time upfront when testing early functionality.

