using HarmonyLib;
using System;
using BepInEx.Logging;
using Unity.Netcode;
using GameNetcodeStuff;

namespace LethalModding
{
    public static class FlashlightPatch
    {
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("FlashlightPatch");
        public static void Explosion(FlashlightItem __instance)
        {
            float explosionRate = LethalFlashlights.configExplosionRate.Value;
            Random rand = new();
            float flashLightFailure = (float)(rand.Next(0, 10000) / 100);
            if (flashLightFailure <= explosionRate)
            {
                logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID}: Flashlight failure");
                UnityEngine.Vector3 playerpos = (UnityEngine.Vector3)__instance.playerHeldBy?.transform.position;
                logger.LogDebug($"Plugin {PluginInfo.PLUGIN_GUID}: Explosion spawned at {playerpos}");

                Landmine.SpawnExplosion(playerpos, spawnExplosionEffect: true, 3f, 5f);
                return;
            }
        }

        [HarmonyPatch(typeof(GrabbableObject), "ActivateItemServerRpc")]
        [HarmonyPostfix]
        private static void ServerRpcPatch(GrabbableObject __instance)
        {
            NetworkManager networkManager = __instance.NetworkManager;
            if ((object)networkManager != null && networkManager.IsListening)
            {
                if ((networkManager.IsServer || networkManager.IsHost) && __instance is FlashlightItem flashlight && __instance.playerHeldBy != null)
                {
                    logger.LogDebug($"Plugin {PluginInfo.PLUGIN_GUID}: Flashlight detected");
                    Explosion(flashlight);

                }
            }
        }
    }
}
