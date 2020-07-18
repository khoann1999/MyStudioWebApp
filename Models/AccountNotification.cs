using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class AccountNotification
    {
        public string Username { get; set; }
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }
        public virtual Account UsernameNavigation { get; set; }
    }
}
