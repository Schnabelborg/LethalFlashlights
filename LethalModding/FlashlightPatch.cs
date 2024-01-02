using HarmonyLib;
using System;
using BepInEx.Logging;

namespace LethalModding
{
    public static class FlashlightPatch
    {
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("FlashlightPatch");

        [HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.ItemActivate))] [HarmonyPrefix]
        public static void Prefix(FlashlightItem __instance)
        {
            Random rand = new Random();
            int flashLightFailure = rand.Next(0, 9);
            if (flashLightFailure == 0)
            {
                UnityEngine.Vector3 playerpos = GameNetworkManager.Instance.localPlayerController.gameObject.transform.position;
                Landmine.SpawnExplosion(playerpos, spawnExplosionEffect: true, 1f, 1f);
                return;
            }
        }

    }
}
