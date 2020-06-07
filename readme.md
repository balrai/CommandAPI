// command to create our solution
\$ dotnet new sln --name CommandAPISolution

// We now want to associate both our “child” projects to our solution, to do so, issue the following command:

\$ dotnet sln CommandAPISolution.sln add src/CommandAPI/CommandAPI.csproj test/CommandAPI.Tests/CommandAPI.Tests.csproj

// start postgres in docker
\$ docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres
\$ docker ps

// install ENTITY FRAMEWORK COMMAND LINE TOOLS
\$ dotnet tool install --global dotnet-ef

• Microsoft.EntityFrameworkCore - Primary Entity Framework Core Package
• Microsoft.EntityFrameworkCore.Design - Design time components (required for migrations)
• Npgsql.EntityFrameworkCore.PostgreSQL - PosrgreSQL provider for Entity Framework Core

\$ dotnet add package Microsoft.EntityFrameworkCore
\$ dotnet add package Microsoft.EntityFrameworkCore.Design
\$ dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
