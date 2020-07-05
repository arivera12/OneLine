using System.Collections.Generic;

namespace OneLine.Models.Users
{
    public interface IUpdateInternal
    {
        string Id { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        IEnumerable<string> Roles { get; set; }
    }
    public class UpdateInternal : IUpdateInternal
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
    public class UpdateInternalViewModel : IUpdateInternal
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
