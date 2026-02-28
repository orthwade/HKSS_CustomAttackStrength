using System.Collections.Generic;
using BepInEx.Configuration;

namespace owd.CustomOutputDamage
{
    internal static class Configuration
    {
        private static ConfigEntry<float> Multiplier;
        private static ConfigEntry<float> MultiplierNeedle;
        private static ConfigEntry<float> MultiplierSkills;
        private static ConfigEntry<float> MultiplierTools;

        private static ConfigEntry<float> MultiplierBoss;

        public class ToolMultiplierConfig
        {
            public ToolItem ToolItem_ { get; set; }
            public ConfigEntry<float> Multiplier { get; set; }
        }

        public class SkillMultiplierConfig
        {
            public ToolItem ToolItem_ { get; set; }
            public ConfigEntry<float> Multiplier { get; set; }
        }

        private static List<ToolMultiplierConfig> SkillMultipliers = new();
        private static List<ToolMultiplierConfig> RedToolMultipliers = new();

        public static void InitSkill(ConfigFile config, ToolItem tool)
        {
            var skillConfig = new ToolMultiplierConfig
            {
                ToolItem_ = tool,
                Multiplier = config.Bind(
                    $"05 - Skills - {SkillMultipliers.Count:D2} - {tool.name}",
                    "Multiplier",
                    1f,
                    new ConfigDescription(
                        $"Damage multiplier applied to {tool.DisplayName} skill.",
                        new AcceptableValueRange<float>(0.01f, 330.0f)
                    )
                )
            };
            SkillMultipliers.Add(skillConfig);
            if(tool.name == ToolLibrary.ParryInternalName)
            {
                CachedParryConfig = skillConfig;
            }
        }
        public static void InitRedTool(ConfigFile config, ToolItem tool)
        {
            var toolConfig = new ToolMultiplierConfig
            {
                ToolItem_ = tool,
                Multiplier = config.Bind(
                    $"06 - Tools - {RedToolMultipliers.Count:D2} - {tool.DisplayName}",
                    "Multiplier",
                    1f,
                    new ConfigDescription(
                        $"Damage multiplier applied to {tool.DisplayName} tool.",
                        new AcceptableValueRange<float>(0.01f, 330.0f)
                    )
                )
            };
            RedToolMultipliers.Add(toolConfig);
        }

        public static float GetSkillMultiplier(ToolItem tool)
        {
            var config = SkillMultipliers.Find(c => c.ToolItem_ == tool);
            return config != null ? config.Multiplier.Value : 1f;
        }
        public static float GetRedToolMultiplier(ToolItem tool)
        {
            var config = RedToolMultipliers.Find(c => c.ToolItem_ == tool);
            return config != null ? config.Multiplier.Value : 1f;
        }
        private static ToolMultiplierConfig CachedParryConfig = null;

        public static float GetParryMultiplier()
        {
            return CachedParryConfig != null ? CachedParryConfig.Multiplier.Value : 1f;
        }

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
            MultiplierNeedle = config.Bind(
                "01 - Needle",
                "MultiplierNeedle",
                1f,
                new ConfigDescription(
                    "Damage multiplier applied to Needle attacks.",
                    new AcceptableValueRange<float>(0.01f, 330.0f)
                )
            );
            MultiplierSkills = config.Bind(
                "02 - Skills",
                "MultiplierSkills",
                1f,
                new ConfigDescription(
                    "Damage multiplier applied to Skill attacks.",
                    new AcceptableValueRange<float>(0.01f, 330.0f)
                )
            );
            MultiplierTools = config.Bind(
                "03 - Tools",
                "MultiplierTools",
                1f,
                new ConfigDescription(
                    "Damage multiplier applied to Tool attacks.",
                    new AcceptableValueRange<float>(0.01f, 330.0f)
                )
            );
            MultiplierBoss = config.Bind(
                "04 - Bosses",
                "MultiplierBoss",
                1f,
                new ConfigDescription(
                    "Damage multiplier applied to attacks towards bosses." +
                    "\nHigher values make bosses easier to defeat.",
                    new AcceptableValueRange<float>(0.01f, 330.0f)
                )
            );
        }

        public static float GetMultiplier() => Multiplier.Value;
        public static float GetMultiplierNeedle() => MultiplierNeedle.Value;
        public static float GetMultiplierSkills() => MultiplierSkills.Value;
        public static float GetMultiplierTools() => MultiplierTools.Value;
        public static float GetMultiplierBoss() => MultiplierBoss.Value;

        // public static float GetGlobalDamageMultiplier() => GlobalDamageMultiplier.Value;
    }
}
