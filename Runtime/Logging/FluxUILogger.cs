using System.Runtime.CompilerServices;
using FluxUI.Utilities;
using UnityEngine;

namespace FluxUI.Logging
{
    public static class FluxUILogger
    {
        private static void InternalLog(object message, EFluxUILogLevel level, string file, int line)
        {
            if (!FluxUISetting.IsLogEnabled(level)) return;

            var tag = System.IO.Path.GetFileNameWithoutExtension(file);
            var log = $"[{tag}:{line}] {message}";

            switch (level)
            {
                case EFluxUILogLevel.Error:
                {
                    Debug.LogError(log);
                    break;
                }
                case EFluxUILogLevel.Warning:
                {
                    Debug.LogWarning(log);
                    break;
                }
                default:
                {
                    Debug.Log(log);
                    break;
                }
            }
        }

        public static void Verbose(object message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => InternalLog(message, EFluxUILogLevel.Verbose, file, line);
        public static void Info(object message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => InternalLog(message, EFluxUILogLevel.Info, file, line);
        public static void Warn(object message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => InternalLog(message, EFluxUILogLevel.Warning, file, line);
        public static void Error(object message, [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => InternalLog(message, EFluxUILogLevel.Error, file, line);
    }
}