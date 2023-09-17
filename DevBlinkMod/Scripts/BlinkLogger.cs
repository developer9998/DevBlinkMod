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
        
        public static void LogMessage(object message, LogType type)
        {
            UnityEngine.LogType logType = type switch
            {
                LogType.Default => UnityEngine.LogType.Log,
                LogType.Warning => UnityEngine.LogType.Warning,
                LogType.Error => UnityEngine.LogType.Error,
                _ => throw new IndexOutOfRangeException()
            };

            UnityEngine.Debug.unityLogger.Log(logType, string.Format("[{0}, {1}] {2}", PluginInfo.Name, DateTime.Now, message));
        }
    }
}