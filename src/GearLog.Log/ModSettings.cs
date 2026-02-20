using ModSettings;
using UnityEngine;

namespace GearLog.Log
{
    public class ModSettings : JsonModSettings
    {
        [Section("Controls")]

        [Name("Generate Location Log")]
        [Description("Press to create/update log for current location")]
        public KeyCode ScanKey = KeyCode.Insert;

        [Name("Delete Location Log")]
        [Description("Press to remove current location from session log")]
        public KeyCode DeleteKey = KeyCode.Delete;

        [Section("Advanced")]

        [Name("Enable Debug Logs")]
        [Description("Show detailed processing info in console")]
        public bool DebugLogs = false;

        [Name("Include Destroyed Items")]
        [Description("Add items with 0% condition to report")]
        public bool IncludeDestroyed = false;

        protected override void OnConfirm()
        {
            base.OnConfirm();
        }
    }

    public static class Settings
    {
        public static ModSettings Options { get; private set; } = null!;

        public static void Initialize()
        {
            if (Options == null)
            {
                Options = new ModSettings();
                Options.AddToModSettings(BuildInfo.GUIName);
            }
        }
    }
}