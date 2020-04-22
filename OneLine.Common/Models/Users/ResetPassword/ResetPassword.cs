namespace OneLine.Models.Users
{
    public interface IResetPassword
    {
        string Email { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Code { get; set; }
    }
    public class ResetPassword : IResetPassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
    public class ResetPasswordViewModel : IResetPassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}
