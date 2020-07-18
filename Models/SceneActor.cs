using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class SceneActor
    {
        public int SceneId { get; set; }
        public string Username { get; set; }

        public virtual Scene Scene { get; set; }
        public virtual Actor UsernameNavigation { get; set; }
    }
}
