using System.Collections.Generic;

namespace Hotfix
{
    public class ZoneSceneManagerComponent: Entity
    {
        public static ZoneSceneManagerComponent Instance;
        public Dictionary<int, Scene> ZoneScenes = new Dictionary<int, Scene>();
    }
}