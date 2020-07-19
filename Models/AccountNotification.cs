using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class AccountNotification
    {
        public string UserName { get; set; }
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; }
        public virtual Account UserNameNavigation { get; set; }
    }
}
