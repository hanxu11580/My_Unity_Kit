using System;
using System.Collections.Generic;

namespace Hotfix
{
    public static class Game
    {
        public static EventSystem EventSystem => EventSystem.Instance;

        private static Scene scene;
        public static Scene Scene
        {
            get
            {
                if (scene != null)
                {
                    return scene;
                }
                scene = EntitySceneFactory.CreateScene(IdGenerater.GenerateId(), 0, SceneType.Process, "HotfixProcess");
                return scene;
            }
        }

        public static ObjectPool ObjectPool => ObjectPool.Instance;

        public static ET.IdGenerater IdGenerater => ET.IdGenerater.Instance;
        
        public static void Update()
        {
            EventSystem.Update();
        }
        
        public static void LateUpdate()
        {
            EventSystem.LateUpdate();
        }
        
        public static void Close()
        {
            scene?.Dispose();
            scene = null;
            ObjectPool.Instance.Dispose();
            EventSystem.Instance.Dispose();
            ET.IdGenerater.Instance.Dispose();
        }
    }
}