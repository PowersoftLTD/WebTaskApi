namespace TaskManagement.API.Model
{
    public class ConfigDetail
    {
        public string App { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
