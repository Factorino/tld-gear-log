using Il2Cpp;
using MelonLoader;
using System;
using System.IO;

namespace GearLog.Log
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

        public static DateTime GetSessionStartDate(string sessionName)
        {
            string saveName = SaveGameSystem.GetCurrentSaveName();
            string relativePath;

            if (OperatingSystem.IsWindows())
            {
                // C:\Users\<username>\AppData\Local\Hinterland\TheLongDark\Survival\
                relativePath = "AppData\\Local\\Hinterland\\TheLongDark\\Survival";
            }
            else if (OperatingSystem.IsLinux())
            {
                // /home/<username>/.local/share/Hinterland/TheLongDark/Survival/
                relativePath = ".local/share/Hinterland/TheLongDark/Survival";
            }
            else if (OperatingSystem.IsMacOS())
            {
                // /users/<username>/.local/share/Hinterland/TheLongDark/Survival/
                relativePath = ".local/share/Hinterland/TheLongDark/Survival";
            }
            else
            {
                relativePath = string.Empty;
            }

            string savePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                relativePath
            );

            if (string.IsNullOrEmpty(saveName))
            {
                return DateTime.Now;
            }

            savePath = Path.Combine(savePath, saveName);
            return File.GetCreationTime(savePath);
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
