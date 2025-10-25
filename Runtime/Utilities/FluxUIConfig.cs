using FluxUI.Logging;
using UnityEngine;

namespace FluxUI.Utilities
{
    [CreateAssetMenu(fileName = "FluxUIConfig", menuName = "FluxUI/Config")]
    public class FluxUIConfig : ScriptableObject
    {
        public EFluxUILogLevel logLevel = EFluxUILogLevel.All;
    }
}