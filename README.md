# MinimalApiWithModule
A .NET 6 Minimal Web API implemented with the DOMAIN/MODULE pattern and xUnit tests.

This repository shows how to use the .NET 6 minimal Web API which implements the 'domain/module-driven' approach. It moves from the traditional folder structure where the application is grouped by its domain.  The different domains of the application are organised in module (or feature) folders.

## The structure of a module
The benefits of this approach makes that every module becomes self-contained.  Simple modules can have a simple set-up, while a module has the flexibility to deviate from the "default" set-up for more complex modules.  A domain-based structure groups files and folders by their (sub)domain, this gives us a better understanding of the application and makes it easier to navigate through the application.

## Testing
We will be using XUnit for integration testing.
### InternalsVisibleTo
To make the API project visible to internal testing, we need to add the following to the API project:
```xml
<ItemGroup>
	<InternalsVisibleTo Include="MinimalApiWithModuleTests" />
</ItemGroup>
```
Add to the API Program.cs the following to make the implicit Program class public so test projects can access it.
```csharp
public partial class Program { }
```

### Using HTTPREPL
 1. Run the following .NET Core CLI command in the command shell:
```bash
dotnet run
```
The preceding command locates the project at the current directory, retrieves and installs any required project dependencies, compiles the project code, and hosts the web API with the ASP.NET Code Kestrel web server at both an HTTP and HTTPS endpoint.

Ports as defined in the project will be selected for HTTP, port 5000, and port 5001 for HTTPS. Ports used during development can be easily changed by editing the project's launchSettings.json file.

A variation of the following output appears to indicate that the app is running:

```cmd
Building...
info: MinimalApiWithModule[0]
	  Starting MinimalApiWithModule 04/27/2022 11:40:19
info: Microsoft.Hosting.Lifetime[14]
	  Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
	  Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
	  Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
	  Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
	  Content root path: ...\MinimalApiWithModule\MinimalApiWithModule\
```
2. Open a new integrated terminal, then run the following command to install the .NET HTTP REPL command-line tool, to use to make HTTP requests to the web API:
```cmd
dotnet tool install -g Microsoft.dotnet-httprepl
```
3. Connect to the web API by running the following command:
```cmd
httprepl https://localhost:5001
```
4. Explore the available endpoints by running the following command:
```cmd
ls
```
The preceding command detects all APIs available on the connected endpoint.  A variation of the following output appears:
```cmd
https://localhost:5001/> ls
.        []
person   [GET|POST]
ticket   [GET|POST]
```
5. Go to the **person** endpoint by running the following command:
```cmd
cd person
```
6. make a **GET** request in **HttpRepl** by using the following command:
```cmd
get
```
The preceding command makes a **GET** request:
```json
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Wed, 27 Apr 2022 13:51:07 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
	"forename": "Jane",
	"id": 1,
	"isAdmin": true,
	"surname": "Doe",
	"ticketNotes": [
	],
	"tickets": [
	]
  },
  {
	"forename": "Jack",
	"id": 2,
	"isAdmin": false,
	"surname": "Smith",
	"ticketNotes": [
	],
	"tickets": [
	]
  },
]
```

## Minimal API
Using the .NET 6 minimal API framework. See [Minimal APIs overview](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0) for details.

## Entity Framework
### Code First
Using the '[code first](https://entityframework.net/ef-code-first)' principal of Entity Framework.  Use the Package Manager Console to add migrations as needed and then the database needs to be upgraded to include the changes.
```cmd
Add-Migration Initial
Update-Database
```

### Query the database
```PowerShell
Import-Module PSSQLite
$Database = ".\Ticketing.db"
$Query = "SELECT * FROM sqlite_master WHERE type='table'"
Invoke-SqliteQuery -DataSource $Database -Query $Query
```