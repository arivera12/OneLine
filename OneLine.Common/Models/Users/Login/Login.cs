namespace OneLine.Models.Users
{
    public interface ILogin
    {
        string UserName { get; set; }
        string Password { get; set; }
        bool RememberMe { get; set; }
    }
    public class Login : ILogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class LoginViewModel : ILogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
