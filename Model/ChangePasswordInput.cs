namespace TaskManagement.API.Model
{
    public class ChangePasswordInput
    {
        public string LoginName { get; set; }
        public string Old_Password { get; set; }
        public string New_Password { get; set; }
    }
}
