using System;
using System.Collections.Generic;

namespace MyStudioWebApi.Models
{
    public partial class Scene
    {
        public Scene()
        {
            SceneActor = new HashSet<SceneActor>();
            SceneTool = new HashSet<SceneTool>();
        }

        public int SceneId { get; set; }
        public string SceneName { get; set; }
        public string Description { get; set; }
        public int? ShootTimes { get; set; }
        public string SceneScript { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }

        public virtual ICollection<SceneActor> SceneActor { get; set; }
        public virtual ICollection<SceneTool> SceneTool { get; set; }
    }
}
