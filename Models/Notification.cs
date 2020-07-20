using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Notification
    {
        public Notification()
        {
            AccountNotification = new HashSet<AccountNotification>();
        }

        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<AccountNotification> AccountNotification { get; set; }
    }
}
