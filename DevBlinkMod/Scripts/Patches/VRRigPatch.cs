using DevBlinkMod.Scripts;
using HarmonyLib;
using System.Threading.Tasks;
using UnityEngine;

namespace DevBlinkMod.Scripts.Patches
{
    [HarmonyPatch(typeof(VRRig))]
    internal class VRRigPatch
    {
        [HarmonyWrapSafe]
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        internal async static void VRRigPostfix(VRRig __instance)
        {
            await Task.Delay(500);

            GameObject instanceObject = __instance.gameObject;
            if (instanceObject.GetComponent<BlinkManager>() != null && instanceObject.activeSelf) return;
            instanceObject.AddComponent<BlinkManager>();
        }
    }
}
