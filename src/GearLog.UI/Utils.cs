using Il2Cpp;
using MelonLoader;
using System;

namespace GearLog.UI
{
    public class Utils
    {
        private static MelonLogger.Instance _logger = null!;
        
        public static void SetLogger(MelonLogger.Instance logger)
        {
            _logger = logger; 
        }

        public static void Debug(ConsoleColor color, string message)
        {
            if (Settings.Options.DebugLogs)
            {
                _logger.Msg(color, message);
            }
        }

        public static void Debug(string message)
        {
            if (Settings.Options.DebugLogs)
            {
                _logger.Msg(message);
            }
        }

        public static string GetCurrentSaveName()
        {
            return SaveGameSystem.GetCurrentSaveName();
        }

        public static bool IsScenePlayable(string scene)
        {
            return !(
                string.IsNullOrEmpty(scene)
                || scene.Contains("MainMenu")
                || scene == "Boot"
                || scene == "Empty"
            );
        }
    }
}
