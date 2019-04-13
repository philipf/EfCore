# EfCore
Bootstrap for EfCore

## Useful commands:
Add database migrations from existing model:

`Add-Database`

Apply changes to database:

`Update-Database`


To move quickly:
Revert back to clean database and run latest model

```
cd  .\EfCore
dotnet ef database update 0; dotnet ef migrations remove; dotnet build; dotnet ef migrations add initial; dotnet ef database update
```

Alternative:
```
Update-Database 0; Remove-Migration; 
Add-Migration -Name Initial; Update-Database
```
