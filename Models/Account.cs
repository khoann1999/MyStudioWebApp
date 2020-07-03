using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
