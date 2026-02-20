using MelonLoader;

namespace GearLog.UI
{
    public class Main : MelonMod
    {
        private MelonLogger.Instance _logger = null!;

        public override void OnInitializeMelon()
        {
            _logger = new MelonLogger.Instance(Info.Name);
            _logger.Msg($"Version {Info.Version} loaded");

            Settings.Initialize();
            Utils.SetLogger(_logger);
        }
    }
}