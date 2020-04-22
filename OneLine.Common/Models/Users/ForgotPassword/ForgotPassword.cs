namespace OneLine.Models.Users
{
    public interface IForgotPassword
    {
        string Email { get; set; }
    }
    public class ForgotPassword : IForgotPassword
    {
        public string Email { get; set; }
    }
    public class ForgotPasswordViewModel : IForgotPassword
    {
        public string Email { get; set; }
    }
}
