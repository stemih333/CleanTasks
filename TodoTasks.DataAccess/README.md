dotnet ef migrations add Initial --startup-project ..\CleanTodoTasks.WebAPI

dotnet ef database update --startup-project ..\CleanTodoTasks.WebAPI
