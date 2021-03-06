﻿using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Actor
    {
        public Actor()
        {
            SceneActor = new HashSet<SceneActor>();
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? PhoneNumber { get; set; }
        public string Email { get; set; }

        public virtual Account UserNameNavigation { get; set; }
        public virtual ICollection<SceneActor> SceneActor { get; set; }
    }
}
