

The generated database code requires Entity Framework Core Migrations. Run the following commands:
1. dotnet ef migrations add CreateIdentitySchema
2. dotnet ef database update
 Or from the Visual Studio Package Manager Console:
1. Add-Migration CreateIdentitySchema
2. Update-Database

Apps that use ASP.NET Core Identity should also use HTTPS. To enable HTTPS see https://go.microsoft.com/fwlink/?linkid=848054.

