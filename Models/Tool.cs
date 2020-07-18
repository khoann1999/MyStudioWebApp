using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Tool
    {
        public Tool()
        {
            SceneTool = new HashSet<SceneTool>();
        }

        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<SceneTool> SceneTool { get; set; }
    }
}
