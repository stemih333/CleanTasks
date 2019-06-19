namespace TodoTasks.DataAccess.Auth
{
    public class AuthSettings
    {
        public string Type { get; set; }
        public string Instance { get; set; }
        public string Domain { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
        public string CallbackPath { get; set; }
        public string AuthType { get; set; }
    }
}
