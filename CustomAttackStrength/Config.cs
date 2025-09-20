using BepInEx.Configuration;

namespace CustomAttackStrength
{
    internal static class Config
    {
        private static ConfigEntry<float> Multiplier;
      

        public static void Init(ConfigFile config)
        {
            Multiplier = config.Bind(
                "00 - Global",
                "Multiplier",
                1f,
                new ConfigDescription(
                    "Global damage multiplier applied to all outcoming attacks.",
                    new AcceptableValueRange<float>(0.01f, 330.0f)
                )
            );
        }

        public static float GetMultiplier() => Multiplier.Value;

        // public static float GetGlobalDamageMultiplier() => GlobalDamageMultiplier.Value;
    }
}
