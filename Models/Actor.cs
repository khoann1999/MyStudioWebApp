using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Actor
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? Phonenumber { get; set; }
        public string Email { get; set; }
    }
}
