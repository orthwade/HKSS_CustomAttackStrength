using System;
using GenericVariableExtension;
using HarmonyLib;


namespace CustomAttackStrength.Patches
{
    [HarmonyPatch(typeof(HealthManager))]
    [HarmonyPatch("TakeDamage")]
    internal static class HealthManager_TakeDamage_Patch
    {
        static readonly AccessTools.FieldRef<HealthManager, int> initHpRef =
        AccessTools.FieldRefAccess<HealthManager, int>("initHp");

        private static void LogInfo(string s) => owd.BepinexPluginLogger.LogInfo(s);
        private static float GetMultiplier() => Config.GetMultiplier();
        private static bool Prefix(HealthManager __instance, ref HitInstance hitInstance)
        {
            if (__instance == null || __instance.gameObject == null || __instance.gameObject.name == null)
                return true;
                
            if (!hitInstance.IsHeroDamage)
            {
                return true;
            }

            int initHp = initHpRef(__instance);
            LogInfo($"[HealthManager_TakeDamage_Patch] {__instance.gameObject.name} taking damage={hitInstance.DamageDealt} initHp={initHp}");

            if (initHp > 0 && hitInstance.DamageDealt > 0)
            {
                hitInstance.Multiplier *= GetMultiplier();
                
                LogInfo($"[HealthManager_TakeDamage_Patch] {__instance.gameObject.name} multiplier={hitInstance.Multiplier}");
            }

            return true; // let the original TakeDamage run, now with modified data
        }

    }
}
