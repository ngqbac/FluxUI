#if UNITY_EDITOR
using System.IO;
using FluxUI.Logging;
using FluxUI.Utilities;
using UnityEditor;
using UnityEngine;

namespace FluxUI.Editor
{
    public static class FluxUIEditor
    {
        [MenuItem("Tools/FluxUI/Create Config Asset")]
        public static void CreateConfig()
        {
            const string resourcesPath = "Assets/Resources";
            const string assetPath = "Assets/Resources/FluxUIConfig.asset";

            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
            }

            if (File.Exists(assetPath))
            {
                FluxUILogger.Info("Config already exists.");
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<FluxUIConfig>(assetPath);
                return;
            }

            var config = ScriptableObject.CreateInstance<FluxUIConfig>();
            AssetDatabase.CreateAsset(config, assetPath);
            AssetDatabase.SaveAssets();
            
            FluxUILogger.Info("Config created at Assets/Resources/FluxUIConfig.asset.");
            Selection.activeObject = config;
        }
    }
}
#endif