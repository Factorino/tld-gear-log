using GearLog.Mod;
using GearLog.Mod.Entities;
using MelonLoader;
using MelonLoader.Utils;
using Newtonsoft.Json;
using System;
using System.IO;

namespace TLDTestMod.Source.Services
{
    public class FileManager
    {
        private readonly string _LOG_PREFIX = "TLD_Log_";
        private readonly string _LOG_EXT = ".json";

        private readonly JsonSerializerSettings _jsonSettings = new()
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly string _modDir;

        private readonly MelonLogger.Instance _logger;

        public FileManager(string modName, MelonLogger.Instance logger)
        {
            _logger = logger;
            _modDir = Path.Combine(MelonEnvironment.ModsDirectory, modName);
            Directory.CreateDirectory(_modDir);
        }

        public SessionData LoadSessionLog(string sessionName, DateTime sessionStart)
        {
            string path = _getSessionLogPath(sessionName);

            if (!File.Exists(path))
            {
                _logger.Msg($"[FileManager] New session log: {sessionName}");
                return new SessionData(sessionName, sessionStart);
            }

            try
            {
                string json = File.ReadAllText(path);
                SessionData? data = JsonConvert.DeserializeObject<SessionData>(json);
                _logger.Msg($"[FileManager] Loaded session log: {data?.SessionName} ({data?.Scenes?.Count} scenes)");
                return data ?? new SessionData(sessionName, sessionStart);
            }
            catch (Exception e)
            {
                _logger.Error($"[FileManager] Load session log error: {e.Message}");
                return new SessionData(sessionName, sessionStart);
            }
        }

        public void SaveSessionLog(SessionData data)
        {
            try
            {
                string path = _getSessionLogPath(data.SessionName);
                string json = JsonConvert.SerializeObject(data, _jsonSettings);
                File.WriteAllText(path, json);

                if (Settings.Options.DebugLogs)
                {
                    _logger.Msg($"[FileManager] Saved session log: {Path.GetFileName(path)}");
                }
            }
            catch (Exception e)
            {
                _logger.Error($"[FileManager] Save session log error: {e.Message}");
            }
        }

        private string _getSessionLogPath(string sessionName) =>
            Path.Combine(_modDir, $"{_LOG_PREFIX}Session_{sessionName}{_LOG_EXT}");
    }
}