using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStudioWebApi.Models;

namespace MyStudioWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly MyStudioAppContext _context;
        private readonly IWebHostEnvironment _environment;

        public ToolsController(MyStudioAppContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tool>>> GetTool()
        {
            return await _context.Tool.ToListAsync();
        }

        // GET: api/Tools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tool>> GetTool(int id)
        {
            var tool = await _context.Tool.FindAsync(id);

            if (tool == null)
            {
                return NotFound();
            }

            return tool;
        }

        // PUT: api/Tools/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTool(int id, Tool tool)
        {
            if (id != tool.ToolId)
            {
                return BadRequest();
            }

            _context.Entry(tool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tools
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tool>> PostTool(Tool tool)
        {
            _context.Tool.Add(tool);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ToolExists(tool.ToolId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTool", new { id = tool.ToolId }, tool);
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tool>> DeleteTool(int id)
        {
            var sceneTool = _context.SceneTool.Where(result => result.ToolId.Equals(id)).Include(result => result.Tool);
            var tool = await _context.Tool.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            _context.SceneTool.RemoveRange(sceneTool);
            _context.Tool.Remove(tool);
            await _context.SaveChangesAsync();

            return tool;
        }

        private bool ToolExists(int id)
        {
            return _context.Tool.Any(e => e.ToolId == id);
        }

        // POST: api/Tools/UploadImage
        [HttpPost("UploadImage"), DisableRequestSizeLimit]
        public IActionResult UploadPostImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "ToolImages");
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(fileStream);
                }
                return Ok("PostImage Uploaded");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
