Documentation and other informtions goes here.

Create Script----
dotnet ef migrations script -p Infrastructure/ -s WebAPI/ | out-file ./script-date.sql 

//Reset Migrations if it gets to big or a problem.
https://weblog.west-wind.com/posts/2016/jan/13/resetting-entity-framework-migrations-to-a-clean-slate
https://stackoverflow.com/questions/11679385/reset-entity-framework-migrations
1)Remove the _MigrationHistory table from the Database
2)Remove the individual migration files in your project's Migrations folder
3)Enable-Migrations in Package Manager Console
4)Add-migration Initial in PMC
5)Comment out the code inside of the Up method in the Initial Migration
6)Update-database in PMC (does nothing but creates Migration Entry)
7)Remove comments in the Initial method

//GET CURRENCY
https://free.currencyconverterapi.com/
https://stackoverflow.com/questions/53341259/get-exchange-rate-from-currency-converter-api-in-c-sharp
https://fixer.io/