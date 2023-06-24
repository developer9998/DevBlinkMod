using System;
using WebSocketSharp;

namespace DevBlinkMod.Scripts
{
    public class BlinkLogger
    {
        public enum LogType
        {
            Default = 0,
            Warning = 1,
            Error = 2
        }

        public static void LogMessage(string message, LogType type)
        {
            string outputMessage = "No message to output.";
            LogType outputType = LogType.Error;

            if (!message.IsNullOrEmpty()) outputMessage = message;
            if (!type.ToString().IsNullOrEmpty()) outputType = type;

            switch (outputType)
            {
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(string.Format("[{0}, {1}] {2}", PluginInfo.Name, DateTime.Now, outputMessage));
                    break;
                case LogType.Error:
                    UnityEngine.Debug.LogError(string.Format("[{0}, {1}] {2}", PluginInfo.Name, DateTime.Now, outputMessage));
                    break;
                default:
                    UnityEngine.Debug.Log(string.Format("[{0}, {1}] {2}", PluginInfo.Name, DateTime.Now, outputMessage));
                    break;
            }
        }
    }
}