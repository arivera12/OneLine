using System;
using System.Collections.Generic;

namespace OneLine.Models.Users
{
    public partial interface IAspNetUsers
    {
        string Id { get; set; }
        string UserName { get; set; }
        string NormalizedUserName { get; set; }
        string Email { get; set; }
        string NormalizedEmail { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        string ConcurrencyStamp { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool TwoFactorEnabled { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        bool IsLocked { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
    public partial class AspNetUsers : IAspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsLocked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public partial class AspNetUsersViewModel : IAspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool IsLocked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get { return $"{FirstName} {LastName}"; } }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
