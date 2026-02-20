using GearLog.Mod.Entities;
using Il2Cpp;
using Il2CppRewired.Utils;
using MelonLoader;
using System;
using TLDTestMod.Source.Services;
using UnityEngine;

namespace GearLog.Mod.Services
{
    public class GearScanner
    {
        private readonly FileManager _fileManager;

        private readonly MelonLogger.Instance _logger;

        public GearScanner(FileManager fileManager, MelonLogger.Instance logger)
        {
            _logger = logger;
            _fileManager = fileManager;
        }

        public void ScanScene(string sceneName, string sessionName, SessionData sessionData)
        {
            SceneData sceneData = new SceneData(sceneName);

            int processed = 0;

            GearItem[] items = UnityEngine.Object.FindObjectsByType<GearItem>(FindObjectsSortMode.None);
            processed += _processItems(sceneData, items);

            Container[] containers = UnityEngine.Object.FindObjectsByType<Container>(FindObjectsSortMode.None);
            foreach (Container container in containers)
            {
                if (!container.gameObject.activeInHierarchy)
                {
                    continue;
                }

                GearItem[] containerItems = container.GetComponentsInChildren<GearItem>(includeInactive: true);
                processed += _processItems(sceneData, containerItems);
            }

            sessionData.SaveScene(sceneData);
            _fileManager.SaveSessionLog(sessionData);

            _logger.Msg(ConsoleColor.Green, $"Scanned: {sceneName} | Items: {processed}/{items.Length} | Session: {sessionName}");
        }

        public bool DeleteSceneFromSession(string sceneName, string sessionName, SessionData sessionData)
        {
            if (!sessionData.DeleteScene(sceneName))
            {
                _logger.Warning($"Not found: {sceneName} in Session {sessionName}");
                return false;
            }

            _fileManager.SaveSessionLog(sessionData);
            _logger.Msg(ConsoleColor.Yellow, $"Deleted: {sceneName} from Session {sessionName}");
            return true;
        }

        private int _processItems(SceneData sceneData, GearItem[] items)
        {
            int processed = 0;

            foreach (GearItem item in items)
            {
                if (item == null || !item.gameObject.activeInHierarchy)
                {
                    continue;
                }
                if (!Settings.Options.IncludeDestroyed && item.IsNullOrDestroyed())
                {
                    continue;
                }

                try
                {
                    ItemData itemData = new ItemData(item);
                    sceneData.AddItem(itemData);
                    processed++;
                }
                catch (Exception ex)
                {
                    _logger.Warning($"Error processing '{item?.name}': {ex.Message}");
                }
            }

            return processed;
        }
    }
}
