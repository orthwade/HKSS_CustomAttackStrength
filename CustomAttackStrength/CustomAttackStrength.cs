using BepInEx;
using HarmonyLib;
using owd;

namespace CustomAttackStrength
{
    [BepInPlugin("com.orthwade.CustomAttackStrength", "Custom Attack Strength", "1.0.0")]
    public class CustomAttackStrength : BaseUnityPlugin
    {
        internal static CustomAttackStrength Instance;
        private void Awake()
        {
            Instance = this;

            BepinexPluginLogger.Init(Config, "Custom Attack Strength");

            global::CustomAttackStrength.Config.Init(Config);

            BepinexPluginLogger.LogInfo("CustomAttackStrength loaded!");

            // Apply Harmony patches
            var harmony = new Harmony("com.orthwade.CustomAttackStrength");
            harmony.PatchAll();
        }
    }
}
