namespace OneLine.Models.Users
{
    public interface IResetOwnPasswordInternal
    {
        string CurrentPassword { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Code { get; set; }
    }
    public class ResetOwnPasswordInternal : IResetOwnPasswordInternal
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
    public class ResetOwnPasswordInternalViewModel : IResetOwnPasswordInternal
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Code { get; set; }
    }
}
