using BepInEx;
using DevBlinkMod.Scripts;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DevBlinkMod
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }
        public Texture2D faceSheet;

        internal void Awake()
        {
            Instance = this;
            BlinkLogger.LogMessage("DevBlinkMod awoken", BlinkLogger.LogType.Default);

            HarmonyPatches.HarmonyPatches.ApplyHarmonyPatches();
            BlinkLogger.LogMessage("Applied harmony patches", BlinkLogger.LogType.Default);

            faceSheet = new Texture2D(512, 130, TextureFormat.RGBA32, false) { filterMode = FilterMode.Point };

            Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"DevBlinkMod.Resources.blinksheet.png");
            byte[] bytes = new byte[manifestResourceStream.Length];
            manifestResourceStream.Read(bytes, 0, bytes.Length);

            faceSheet.name = "gorillachestface";
            faceSheet.LoadImage(bytes);
            faceSheet.Apply();
            BlinkLogger.LogMessage("Generated face texture", BlinkLogger.LogType.Default);
        }
    }
}
