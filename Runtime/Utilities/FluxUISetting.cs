using FluxUI.Logging;
using UnityEngine;

namespace FluxUI.Utilities
{
    public static class FluxUISetting
    {
        private static FluxUIConfig _config;
        
        private static FluxUIConfig Config
        {
            get
            {
                if (_config != null) return _config;
                _config = Resources.Load<FluxUIConfig>("FluxUIConfig");
#if UNITY_EDITOR
                if (_config == null) Debug.LogWarning("[FluxUI] Missing FluxUIConfig. Create it via Tools > FluxUI > Create Config Asset");
#endif
                return _config;
            }
        }

        public static bool IsLogEnabled(EFluxUILogLevel level) => (Config.logLevel & level) != 0;
    }
}