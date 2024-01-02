using HarmonyLib;
using System;

namespace LethalModding
{
    public static class FlashlightPatch
    {
        [HarmonyPatch(typeof(FlashlightItem), nameof(FlashlightItem.ItemActivate))] [HarmonyPrefix]
        public static void Prefix(FlashlightItem __instance)
        {
            Random rand = new Random();
            int flashLightFailure = rand.Next(0, 9);
            if (flashLightFailure == 0)
            {
                FlashlightItem flashLight = __instance;
                __instance.UseUpBatteries();
                UnityEngine.Vector3 playerpos = GameNetworkManager.Instance.localPlayerController.gameObject.transform.position;
                Landmine.SpawnExplosion(playerpos, spawnExplosionEffect: true, 1f, 7f);
                return;
            }
        }

    }
}
