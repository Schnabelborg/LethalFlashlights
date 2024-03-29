﻿using BepInEx;
using HarmonyLib;
using LethalConfig;
using LethalConfig.ConfigItems.Options;
using LethalConfig.ConfigItems;
using BepInEx.Configuration;
using UnityEngine;

namespace LethalModding
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("ainavt.lc.lethalconfig")]
    public class LethalFlashlights : BaseUnityPlugin
    {
        public static AssetBundle MainAssetBundle;
        public static ConfigEntry<float> configExplosionRate;
        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            configExplosionRate = Config.Bind("General", "ExplosionRate", 10f, "Changes the likelihood of your flashlight exploding");
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID}: Config generated!");

            FloatStepSliderConfigItem explosionRateSlider = new FloatStepSliderConfigItem(configExplosionRate, new FloatStepSliderOptions
            {
                RequiresRestart = false,
                Min = 0f,
                Max = 100f,
                Step = 0.01f
            });
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID}: Created config slider!");
            LethalConfigManager.AddConfigItem(explosionRateSlider);

            Harmony.CreateAndPatchAll(typeof(FlashlightPatch), PluginInfo.PLUGIN_GUID);
        }
    }
}
