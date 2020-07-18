using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class SceneTool
    {
        public int Id { get; set; }
        public int SceneId { get; set; }
        public int ToolId { get; set; }
        public int? Quantity { get; set; }

        public virtual Scene Scene { get; set; }
        public virtual Tool Tool { get; set; }
    }
}
