dotnet ef migrations add Initial --startup-project ..\TodoTasks.WebAPI

dotnet ef database update --startup-project ..\TodoTasks.WebAPI
