using CleanTasks.Application.TodoArea.Models;
using CleanTasks.Common.Constants;
using CleanTasks.RazorGUI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanTasks.RazorGUI.Pages
{
    public class IndexModel : BaseModel
    {
        public string Username { get; set; }

        private readonly ITodoApiClient _client;
        private List<TodoAreaDto> Areas;
 
        public IndexModel(ITodoApiClient client) : base()
        {
            _client = client;
        }

        public async Task OnGet()
        {
            if(User.Identity.IsAuthenticated && User is ClaimsPrincipal)
            {
                Username = User.Claims.FirstOrDefault(_ => _.Type.Equals("given_name"))?.Value + " "
                    + User.Claims.FirstOrDefault(_ => _.Type.Equals("family_name"))?.Value;

                Areas = await _client.GetTodoAreas();
            }
        }
    }
}
