using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class SceneActor
    {
        public int Id { get; set; }
        public int SceneId { get; set; }
        public string UserName { get; set; }

        public virtual Scene Scene { get; set; }
        public virtual Actor UserNameNavigation { get; set; }
    }
}
