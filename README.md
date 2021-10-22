# Demo of Expression Tree Filters

Quick example to demonstrate how you could
use expression trees to create filters
in EF Core.

See the GetAllAsync method in src/FilterDemo/Data/PersonRepository.cs.

The code is pretty hacky, but makes the point.

Uses .NET 5.0. Enter `dotnet run -p src/FilterDemo` from the command line.

The code dumps out the generated SQL.

Assumes you have SQL Server LocalDb available. Creates and drops a database called "Filter Demo".
