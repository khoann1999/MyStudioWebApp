using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStudioWebApi.RequestModels
{
    public partial class RequestTool
    {
        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
    }
}
