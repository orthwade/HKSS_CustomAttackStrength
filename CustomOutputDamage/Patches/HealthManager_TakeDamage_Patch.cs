using System;
using GenericVariableExtension;
using HarmonyLib;


namespace owd.CustomOutputDamage.Patches
{
    [HarmonyPatch(typeof(HealthManager))]
    [HarmonyPatch("TakeDamage")]
    internal static class HealthManager_TakeDamage_Patch
    {
        static readonly AccessTools.FieldRef<HealthManager, int> initHpRef =
        AccessTools.FieldRefAccess<HealthManager, int>("initHp");

        private static void LogInfo(string s) => owd.PluginLogger.LogInfo(s);
        private static float GetMultiplier() => Configuration.GetMultiplier();
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

                if (hitInstance.RepresentingTool)
                {
                    if (hitInstance.RepresentingTool.Type == ToolItemType.Skill)
                    {
                        hitInstance.Multiplier *= Configuration.GetMultiplierSkills();
                        LogInfo($"[HealthManager_TakeDamage_Patch] {__instance.gameObject.name} skill multiplier={hitInstance.Multiplier}");
                        LogInfo($"[HealthManager_TakeDamage_Patch] Skill name={hitInstance.RepresentingTool.name}");
                    }
                    else if (hitInstance.RepresentingTool.Type == ToolItemType.Red)
                    {
                        hitInstance.Multiplier *= Configuration.GetMultiplierTools();
                        LogInfo($"[HealthManager_TakeDamage_Patch] {__instance.gameObject.name} tool multiplier={hitInstance.Multiplier}");
                        LogInfo($"[HealthManager_TakeDamage_Patch] Tool name={hitInstance.RepresentingTool.name}");
                    }
                }
                else if (hitInstance.IsUsingNeedleDamageMult)
                {
                    hitInstance.Multiplier *= Configuration.GetMultiplierNeedle();
                    LogInfo($"[HealthManager_TakeDamage_Patch] {__instance.gameObject.name} needle multiplier={hitInstance.Multiplier}");
                }
                if (owd.BossNames.IsBossName(__instance.gameObject.name))
                {
                    hitInstance.Multiplier *= Configuration.GetMultiplierBoss();
                    LogInfo($"[HealthManager_TakeDamage_Patch] {__instance.gameObject.name} boss multiplier={hitInstance.Multiplier}");
                }
            }

            return true; // let the original TakeDamage run, now with modified data
        }

    }
}
