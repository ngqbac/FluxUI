#if UNITY_EDITOR
using System.IO;
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
                // OrganicLog.Info("OrganicConfig already exists.");
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<FluxUIConfig>(assetPath);
                return;
            }

            var config = ScriptableObject.CreateInstance<FluxUIConfig>();
            AssetDatabase.CreateAsset(config, assetPath);
            AssetDatabase.SaveAssets();
            //
            // OrganicLog.Info("OrganicConfig created at Assets/Resources/OrganicConfig.asset");
            Selection.activeObject = config;
        }
    }
}
#endif