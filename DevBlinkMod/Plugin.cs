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

        internal async void Awake()
        {
            Instance = this;
            BlinkLogger.LogMessage("DevBlinkMod awoken", BlinkLogger.LogType.Default);

            HarmonyPatches.ApplyHarmonyPatches();
            BlinkLogger.LogMessage("Applied harmony patches", BlinkLogger.LogType.Default);

            Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"DevBlinkMod.Resources.blinksheet.png");
            byte[] bytes = new byte[manifestResourceStream.Length];
            await manifestResourceStream.ReadAsync(bytes, 0, bytes.Length);

            faceSheet = new Texture2D(192, 65, TextureFormat.RGB24, false)
            {
                wrapMode = TextureWrapMode.Repeat,
                filterMode = FilterMode.Point,
                name = "Blink Sheet"
            };
            faceSheet.LoadImage(bytes);
            faceSheet.Apply();
            BlinkLogger.LogMessage("Generated face texture", BlinkLogger.LogType.Default);
        }
    }
}
