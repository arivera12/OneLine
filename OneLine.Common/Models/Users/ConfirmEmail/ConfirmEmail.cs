using System;
using System.Collections.Generic;
using System.Text;

namespace OneLine.Models.Users
{
    public interface IConfirmEmail
    {
        string UserId { get; set; }
        string Code { get; set; }
    }
    public class ConfirmEmail : IConfirmEmail
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
    public class ConfirmEmailViewModel : IConfirmEmail
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
