using GearLog.Mod.Entities;
using GearLog.Mod.Services;
using Il2Cpp;
using MelonLoader;
using System;
using System.IO;
using TLDTestMod.Source.Services;
using UnityEngine;

namespace GearLog.Mod
{
    public class Main : MelonMod
    {
        private FileManager _fileManager = null!;
        private GearScanner _gearScanner = null!;

        private SessionData _currentSessionData = null!;
        private string _currentSessionName = string.Empty;
        private DateTime _currentSessionStart = DateTime.MinValue;
        private string _currentSceneName = string.Empty;

        private bool _isScanPressed;
        private bool _isDeletePressed;

        private MelonLogger.Instance _logger = null!;

        public override void OnInitializeMelon()
        {
            _logger = new MelonLogger.Instance(Info.Name);
            _logger.Msg($"Version {Info.Version} loaded");

            Settings.Initialize();
            _fileManager = new FileManager(Info.Name, _logger);
            _gearScanner = new GearScanner(_fileManager, _logger);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _currentSceneName = sceneName;
            _updateSessionContext();

            if (Settings.Options.DebugLogs)
            {
                _logger.Msg($"Scene loaded: {sceneName} | Session: {_currentSessionName}");
            }
        }

        public override void OnUpdate()
        {
            _updateSessionContext();

            if (!_isScenePlayable(_currentSceneName))
            {
                if (Settings.Options.DebugLogs)
                {
                    _logger.Msg($"Scene '{_currentSceneName}' is not playable");
                }
                return;
            }

            if (Input.GetKeyDown(Settings.Options.ScanKey) && !_isScanPressed)
            {
                _isScanPressed = true;
                if (!string.IsNullOrEmpty(_currentSceneName) && _currentSessionData != null)
                {
                    _gearScanner.ScanScene(_currentSceneName, _currentSessionName, _currentSessionData);
                }
            }
            else if (Input.GetKeyUp(Settings.Options.ScanKey))
            {
                _isScanPressed = false;
            }

            if (Input.GetKeyDown(Settings.Options.DeleteKey) && !_isDeletePressed)
            {
                _isDeletePressed = true;
                if (!string.IsNullOrEmpty(_currentSceneName) && _currentSessionData != null)
                {
                    _gearScanner.DeleteSceneFromSession(_currentSceneName, _currentSessionName, _currentSessionData);
                }
            }
            else if (Input.GetKeyUp(Settings.Options.DeleteKey))
            {
                _isDeletePressed = false;
            }
        }

        private void _updateSessionContext()
        {
            string newSessionName = _getCurrentSaveName();
            DateTime newSessionStart = _getSessionStartDate(newSessionName);

            if (newSessionName != _currentSessionName)
            {
                _currentSessionName = newSessionName;
                _currentSessionStart = newSessionStart;
                _currentSessionData = _fileManager.LoadSessionLog(_currentSessionName, _currentSessionStart);
                _logger.Msg(System.ConsoleColor.Cyan, $"Session switched: Session {_currentSessionName}");
            }
        }

        private string _getCurrentSaveName()
        {
            return SaveGameSystem.GetCurrentSaveName();
        }

        private DateTime _getSessionStartDate(string sessionName)
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

        private bool _isScenePlayable(string scene)
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
