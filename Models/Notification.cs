using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public string Content { get; set; }
    }
}
