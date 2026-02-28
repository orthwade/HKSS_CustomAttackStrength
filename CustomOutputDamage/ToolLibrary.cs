using System.Collections.Generic;
using UnityEngine;
using BepInEx;
using BepInEx;
using HarmonyLib;

namespace owd.CustomOutputDamage
{
    public static class ToolLibrary
    {
        public static readonly HashSet<ToolItem> Tools = new();

        public static readonly HashSet<ToolItem> Skills = new();
        public static readonly HashSet<ToolItem> RedTools = new();

        public static readonly string ParryInternalName = "Parry";

        private static bool initialized = false;

        public static void Init()
        {
            if (initialized)
                return;

            ToolItem[] tools = Resources.FindObjectsOfTypeAll<ToolItem>();

            foreach (ToolItem t in tools)
            {
                if (t == null)
                    continue;

                Tools.Add(t);

                if (t.Type == ToolItemType.Skill)
                {
                    if(Skills.Add(t))
                    {
                        Configuration.InitSkill(CachedObjects.Config, t);
                    }
                }
                else if (t.Type == ToolItemType.Red)
                {
                    if(RedTools.Add(t))
                    {
                        Configuration.InitRedTool(CachedObjects.Config, t);
                    }
                }
            }

            PluginLogger.LogInfo($"[ToolLibrary] Found {Tools.Count} unique tools.");
            PluginLogger.LogInfo($"[ToolLibrary] Found {Skills.Count} skills.");
            PluginLogger.LogInfo($"[ToolLibrary] Found {RedTools.Count} red tools.");

            initialized = true;
        }
        

        public static void PrintAllTools()
        {
            PluginLogger.LogInfo("[ToolLibrary] Known tools:");
            foreach (var tool in Tools)
            {
                PluginLogger.LogInfo($"  {tool.name}");
            }
            foreach (var skill in Skills)
            {
                PluginLogger.LogInfo($"  Skill:\n Internal Name: {skill.name}\nDisplayed Name: {skill.DisplayName}\n CanDamage: {skill.DamageFlags}");
            }
            foreach (var redTool in RedTools)
            {
                PluginLogger.LogInfo($"  Red Tool:\nInternal Name: {redTool.name}\nDisplayed Name: {redTool.DisplayName}");
            }
        }
    }
}