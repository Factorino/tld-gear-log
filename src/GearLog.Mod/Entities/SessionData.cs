using System;
using System.Collections.Generic;

namespace GearLog.Mod.Entities
{
    public class SessionData
    {
        public string SessionName { get; private set; } = string.Empty;

        public DateTime SessionStart { get; private set; } = DateTime.Now;

        public DateTime LastUpdated { get; private set; } = DateTime.Now;

        public Dictionary<string, SceneData> Scenes { get; private set; } = new();

        public SessionData(string sessionName, DateTime sessionStart)
        {
            SessionName = sessionName;
            SessionStart = sessionStart;
        }

        public void SaveScene(SceneData sceneData)
        {
            Scenes[sceneData.SceneName] = sceneData;
            LastUpdated = DateTime.Now;
        }

        public bool DeleteScene(string sceneName)
        {
            if (!Scenes.ContainsKey(sceneName))
            {
                return false;
            }

            Scenes.Remove(sceneName);
            LastUpdated = DateTime.Now;
            return true;
        }

        public SceneData? GetScene(string sceneName) =>
            Scenes.TryGetValue(sceneName, out var data) ? data : null;
    }
}
