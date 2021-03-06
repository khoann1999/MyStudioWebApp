﻿using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountNotification = new HashSet<AccountNotification>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual ICollection<AccountNotification> AccountNotification { get; set; }
    }
}
