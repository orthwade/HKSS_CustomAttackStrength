using BepInEx;
using HarmonyLib;

namespace owd.CustomOutputDamage
{
    [BepInPlugin("com.orthwade.CustomOutputDamage", "Custom Output Damage", "1.0.3")]
    public class CustomOutputDamage : BaseUnityPlugin
    {
        internal static CustomOutputDamage Instance;
        private void Awake()
        {
            Instance = this;

            PluginLogger.Init(Config, "CustomOutputDamage");

            Configuration.Init(Config);

            PluginLogger.LogInfo("CustomOutputDamage loaded!");

            // Apply Harmony patches
            var harmony = new Harmony("com.orthwade.CustomOutputDamage");
            harmony.PatchAll();

            CachedObjects.Config = Config;
        }
    }
}
