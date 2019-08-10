$Env:ASPNETCORE_ENVIRONMENT = "LocalTest"

dotnet ef migrations add Initial --startup-project ..\TodoTasks.WebAPI --context TodoDbContext

dotnet ef database update --startup-project ..\TodoTasks.WebAPI --context TodoDbContext
