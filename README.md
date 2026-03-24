This app simulates a Library Management System, with Patron accounts, Item Records, and Circulation Services.

todo

1. Find a better way to generate PatronId, because the way it works now doesn't generate ids, so can't search by id
2. either create a json file with book and item data, or connect to an api
3. need to add circulation service classes
4. need books
5. Fix the IItem Format member
6. Creating menu loop for users to:
    create accounts
    look up accounts
    update accounts
    delete accounts

Nice-to-haves
7. Create a script that will create random patrons so as not to need ai for this

The use of AI in this Project:

I utilized Claude to create a json file containing dummy data for 200 library patron users. I opted to generate this file with AI rather than serializing this data by hand to save time.

