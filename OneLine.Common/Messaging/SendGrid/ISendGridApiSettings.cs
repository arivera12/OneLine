using System;
using System.Collections.Generic;
using System.Text;

namespace OneLine.Messaging
{
    public interface ISendGridApiSettings
    {
        string SendGridUser { get; set; }
        string SendGridKey { get; set; }
        string Email { get; set; }
        string DisplayName { get; set; }
    }
}
