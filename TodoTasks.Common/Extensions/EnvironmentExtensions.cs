using Microsoft.AspNetCore.Hosting;

namespace TodoTasks.Common.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsLocalTest(this IHostingEnvironment env) => env.EnvironmentName.Equals("LocalTest");
    }
}
