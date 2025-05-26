This is a Dummy Project set up to demonstrate issue on JsonApiDotNetCore Package Version 5.7.1.
There is no need for running any migration script as the project ensures to run all migrations and Data Seed on Init of Application.
After project runs we've allowed CRUD of only the Resource of PriceGroup so you could select a subset of Ids of pricgroups from the database and add the following querystring on the request:
?filter=and(any(id,'XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX','XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX','XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX','XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX','XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX'),equals(isDeleted,'false'))&include=products.unitGroup.units
