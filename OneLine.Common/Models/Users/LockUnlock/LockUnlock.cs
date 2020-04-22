namespace OneLine.Models.Users
{
    public interface ILockUnlock
    {
        string UserId { get; set; }
    }
    public class LockUnlock : ILockUnlock
    {
        public string UserId { get; set; }
    }
    public class LockUnlockViewModel : ILockUnlock
    {
        public string UserId { get; set; }
    }
}
