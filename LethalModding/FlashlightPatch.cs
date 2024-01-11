using HarmonyLib;
using System;
using BepInEx.Logging;
using LethalModding;
using BepInEx.Configuration;

namespace LethalModding
{
    public static class FlashlightPatch
    {
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("FlashlightPatch");

        [HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.ItemActivate))] [HarmonyPrefix]
        public static void Prefix(FlashlightItem __instance)
        {
            float explosionRate = LethalFlashlights.configExplosionRate.Value;
            Random rand = new Random();
            float flashLightFailure = (float)(rand.Next(0, 10000) / 100);
            if (flashLightFailure <= explosionRate)
            {
                logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID}: Flashlight breaky whoops");
                UnityEngine.Vector3 playerpos = GameNetworkManager.Instance.localPlayerController.gameObject.transform.position;
                Landmine.SpawnExplosion(playerpos, spawnExplosionEffect: true, 1f, 1f);
                return;
            }
        }

    }
}
