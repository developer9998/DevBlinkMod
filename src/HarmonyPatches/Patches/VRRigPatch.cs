using DevBlinkMod.Scripts;
using HarmonyLib;

namespace DevBlinkMod.HarmonyPatches.Patches
{
    [HarmonyPatch(typeof(VRRig))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class VRRigPatch
    {
        internal static void Postfix(VRRig __instance)
        {
            if (__instance.GetComponent<BlinkManager>() == null)
            {
                __instance.gameObject.AddComponent<BlinkManager>();
            }
        }
    }
}
