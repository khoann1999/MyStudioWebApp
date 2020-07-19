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
    public class ScenesController : ControllerBase
    {
        private readonly MyStudioAppContext _context;
        private readonly IWebHostEnvironment _environment;

        public ScenesController(MyStudioAppContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // GET: api/Scenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scene>>> GetScene()
        {
            return await _context.Scene.ToListAsync();
        }

        // GET: api/Scenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scene>> GetScene(int id)
        {
            var scene = await _context.Scene.FindAsync(id);

            if (scene == null)
            {
                return NotFound();
            }

            return scene;
        }

        // PUT: api/Scenes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScene(int id, Scene scene)
        {
            if (id != scene.SceneId)
            {
                return BadRequest();
            }

            _context.Entry(scene).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SceneExists(id))
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

        // POST: api/Scenes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Scene>> PostScene(Scene scene)
        {
            _context.Scene.Add(scene);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScene", new { id = scene.SceneId }, scene);
        }

        // DELETE: api/Scenes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Scene>> DeleteScene(int id)
        {
            var scene = await _context.Scene.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }

            _context.Scene.Remove(scene);
            await _context.SaveChangesAsync();

            return scene;
        }

        private bool SceneExists(int id)
        {
            return _context.Scene.Any(e => e.SceneId == id);
        }

        // GET: api/Scenes/5
        [HttpGet("GetActors/{id}")]
        public  ActionResult<List<SceneActor>> GetActor(int id)
        {
            var scene = _context.SceneActor.Where(result => result.SceneId.Equals(id)).ToList();
            if (scene == null)
            {
                return NotFound();
            }

            return scene;
        }

        
        // POST: api/SceneTools
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("AddActor")]
        public async Task<ActionResult<SceneTool>> PostSceneActor(SceneActor sceneActor)
        {
            _context.SceneActor.Add(sceneActor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                    throw;
            }

            return CreatedAtAction("GetSceneTool", new { id = sceneActor.UserName }, sceneActor);
        }
        // GET: api/Scenes/5
        [HttpGet("GetPosts/{id}")]
        public ActionResult<List<SceneTool>> GetSceneTool(int id)
        {
            var scene = _context.SceneTool.Include(result => result.Tool).Where(result => result.SceneId.Equals(id)).ToList();

            if (scene == null)
            {
                return NotFound();
            }

            return scene;
        }

        // POST: api/SceneTools
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("AddPost")]
        public async Task<ActionResult<SceneTool>> PostSceneTool(SceneTool sceneTool)
        {
            _context.SceneTool.Add(sceneTool);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SceneExists(sceneTool.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSceneTool", new { id = sceneTool.Id }, sceneTool);
        }

        // GET: api/Scenes/5
        [HttpGet("GetCurrentScenes/{userName}")]
        public  ActionResult<List<Scene>> GetCurrentScenes(string userName)
        {
            var scene = _context.Scene
                .Where(result => result.SceneActor.Any(result => result.UserName.Equals(userName)) && result.DateEnd >= DateTime.Now).ToList();
            if (scene == null)
            {
                return NotFound();
            }

            return scene;
        }

        // GET: api/Scenes/5
        [HttpGet("GetHistoryScenes/{userName}")]
        public ActionResult<List<Scene>> GetHistoryScenes(string userName)
        {
            var scene = _context.Scene.Where(result => result.SceneActor.Any(result => result.UserName.Equals(userName)) && result.DateEnd < DateTime.Now).ToList();

            if (scene == null)
            {
                return NotFound();
            }

            return scene;
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
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "SceneScripts");
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
