// command to create our solution
\$ dotnet new sln --name CommandAPISolution

// We now want to associate both our “child” projects to our solution, to do so, issue the following command:

\$ dotnet sln CommandAPISolution.sln add src/CommandAPI/CommandAPI.csproj test/CommandAPI.Tests/CommandAPI.Tests.csproj
