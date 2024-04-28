# Product Information System (PIS)

## CLI commands

 - To create a new Entity Framework migration, navigate to inside the
   `PIS.Infrastructure` project and run `dotnet ef migrations add
   MIGRATION_NAME --project ..\PIS.Infrastructure\`
 - To apply migrations to the database, navigate to inside the
   `PIS.Infrastructure` project and run `dotnet ef database update
   --project ..\PIS.Infrastructure\ --connection "**********"`
