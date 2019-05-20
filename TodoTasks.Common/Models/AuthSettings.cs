using System;
using System.Collections.Generic;
using System.Text;

namespace TodoTasks.Common.Models
{
    public class AuthSettings
    {
        public string AuthUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
