# CMC Backend
Dotnet Core Web Api (continuing the todo theme) utilising SQL Server Database.

## Solution notes
### Database Design
A minimalist approach was done for authentication/authorisation covering users, roles and their claims. I have made this tradeoff in lieu of time as the standard 7 table schema is boilerplate covered by ASP.NET Core Identity.
The schema in this design is for Role Based Identity and assumes Windows Authentication is available.
* A User can only have a single Role.
* A Role is seeded with Admin, PowerUser, User
* Roles have associated Permissions, e.g. read, create, update, delete (not seeded)
* Multiple Permissions are assigned to each Role
* Note datatype varchar(50) has been defined as such for brevity for most columns but without time contraints should be modelled more closely to a standard specifification

### Other notes
* Database First approach with scaffold was used to initialise the models and CRUD controllers
* Appropriate tables are seeded
* I have provided 2 API endpoints which cover the following non-trivial queries (we can definitely be more complex with the right data):
1. LINQ to entity query for getting list of completed Todos within last 7 days
2. Query with Raw SQL to a obtain completed query count by user utilising a join to get UserName using a specified DTO
* Swagger can be used on application start to trigger these APIs
* Async has been used for initial seeding and API endpoints
* Error messages are kept generic to avoid unnecessary information leakage

## Target Framework and Packages
* .NET 6.0
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.VisualStudio.Web.CodeGeneration.Design
* Swashbuckle.AspNetCore

## Setup
1. `git clone https://github.com/AbeSong/cmc-backend.git`
2. Update ConnectionStrings by replacing `localhost\\SQLEXPRESS` in appsettings.json to your SQL Server if this is not your default
3. Open solution and run migrations `update-database`
4. Start debugging (F5) to seed initial data
