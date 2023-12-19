# CMC Backend
Dotnet Core Web Api (continuing the todo theme) utilising SQL Server Database.

## Solution notes
### Database Design
A minimalist approach was done for authentication/authorisation covering users, roles and their claims. I have made this tradeoff in lieu of time as the standard schema with boilerplate is well covered by ASP.NET Core Identity.
To summarise my design for Web API auth:
* A User is assigned a single Role.
* A Role has multiple Claims/Permissions associated
* From here, various API Authentication providers can provide this service e.g. one can issue a claims token and Authorisation is persisted on subsequent API call.

### Other notes
* Database First approach with scaffold was used to initialise the models and CRUD controllers
* Appropriate tables are seeded
* I have provided 2 api endpoints which cover the following non-trivial queries (we can definitely be more complex with the right data):
1. LINQ to entity query for getting list of completed Todos within last 7 days
2. Query with Raw SQL to a obtain completed query count by user utilising a join to get UserName using a specified DTO
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
2. Open solution and run migrations `update-database`
3. Start debugging (F5) to seed initial data
