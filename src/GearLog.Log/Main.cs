using GearLog.Log.Entities;
using GearLog.Log.Services;
using MelonLoader;
using System;
using UnityEngine;

namespace GearLog.Log
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
            Utils.SetLogger(_logger);

            _fileManager = new FileManager(Info.Name, _logger);
            _gearScanner = new GearScanner(_fileManager, _logger);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            _currentSceneName = sceneName;
            _updateSessionContext();

            Utils.Debug($"Scene loaded: {sceneName} | Session: {_currentSessionName}");
        }

        public override void OnUpdate()
        {
            _updateSessionContext();

            if (Utils.IsScenePlayable(_currentSceneName))
            {
                Utils.Debug($"Scene '{_currentSceneName}' is not playable");
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
            string newSessionName = Utils.GetCurrentSaveName();
            DateTime newSessionStart = Utils.GetSessionStartDate(newSessionName);

            if (newSessionName != _currentSessionName)
            {
                _currentSessionName = newSessionName;
                _currentSessionStart = newSessionStart;
                _currentSessionData = _fileManager.LoadSessionLog(_currentSessionName, _currentSessionStart);
                _logger.Msg(System.ConsoleColor.Cyan, $"Session switched: Session {_currentSessionName}");
            }
        }
    }
}
