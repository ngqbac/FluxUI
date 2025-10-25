using FluxUI.Utilities;
using UnityEngine;

namespace FluxUI.Logging
{
    public static class FluxUILogger
    {
        public static void Verbose(object sender, object message) => Log(sender, message, EFluxUILogLevel.Verbose);

        public static void Info(object sender, object message) => Log(sender, message, EFluxUILogLevel.Info);

        public static void Warn(object sender, object message) => Log(sender, message, EFluxUILogLevel.Warning);

        public static void Error(object sender, object message) => Log(sender, message, EFluxUILogLevel.Error);
        
        private static void Log(object sender, object message, EFluxUILogLevel level = EFluxUILogLevel.Info)
        {
            if (!FluxUISetting.IsLogEnabled(level)) return;

            var prefix = $"[{sender?.GetType().Name ?? "Unknown"}]";
            var content = $"{prefix} {message}";

            switch (level)
            {
                case EFluxUILogLevel.Error:
                {
                    Debug.LogError(content);
                    break;
                }
                case EFluxUILogLevel.Warning:
                {
                    Debug.LogWarning(content);
                    break;
                }
                case EFluxUILogLevel.Info:
                case EFluxUILogLevel.Verbose:
                {
                    Debug.Log(content);
                    break;
                }
            }
        }
    }
}