namespace OneLine.Models.Users
{
    public interface IResetPasswordInternal
    {
        string CurrentPassword { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Code { get; set; }
    }
    public class ResetPasswordInternal : IResetPasswordInternal
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
    public class ResetPasswordViewModelInternal : IResetPasswordInternal
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}
