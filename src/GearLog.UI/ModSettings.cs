using ModSettings;

namespace GearLog.UI
{
    public class ModSettings : JsonModSettings
    {   
        [Section("Advanced")]

        [Name("Enable Debug Logs")]
        [Description("Show detailed processing info in console")]
        public bool DebugLogs = false;

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