using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class AccountNotification
    {
        public int Id { get; set; }
        public int NoticationId { get; set; }
        public string UserName { get; set; }

        public virtual Notification Notication { get; set; }
        public virtual Account UserNameNavigation { get; set; }
    }
}
