This app simulates a Library Management System, with Patron accounts, Item Records, and Circulation Services.

todo

* Find a better way to generate PatronId, because the way it works now doesn't generate ids, or they are not properly stored in the json, so can't search by id
    going to start ID at 1 and increment, with leading 0's
* Make class library that is shared between all projects
* Creating menu loop for users to:
    create accounts
    look up accounts
    update accounts
    delete accounts
* Patron.AddPatron (http post)
* need books
    . either create a json file with book and item data, or connect to an api
* need to add circulation service classes
* Fix the IItem Format member

Nice-to-haves
* Create a script that will create random patrons so as not to need ai for this

The use of AI in this Project:

I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time.

