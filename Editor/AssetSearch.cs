using System;
using System.IO;
using Gamekit2D.Runtime.Utils;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gamekit2D.Editor
{
    public class AssetSearch
    {
        public static GamekitSettings FindGamekitSettings()
        {
            if (!Directory.Exists("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            var settingsAsset = AssetDatabase.LoadAssetAtPath<GamekitSettings>("Assets/Resources/GamekitSettings.asset");
            if (settingsAsset is not null) return settingsAsset;
            //if not in asset folder, create a new one in the asset folder by copying the one in the package folder
            if (!AssetDatabase.CopyAsset("Packages/com.eren.2dcomplete/GamekitSettings.asset",
                    "Assets/Resources/GamekitSettings.asset"))
                Debug.Log("Failed to copy Gamekit Settings asset. File not found.");
            settingsAsset = AssetDatabase.LoadAssetAtPath<GamekitSettings>("Assets/Resources/GamekitSettings.asset");
            return settingsAsset;
        }
        
        /// <summary>
        /// Finds and returns an asset from within the Gamekit package
        /// </summary>
        /// <param name="path">Path of the asset inside the gamekit package</param>
        /// <typeparam name="T">Object type to return</typeparam>
        /// <returns></returns>
        /// <exception cref="System.Exception">Asset not found at path</exception>
        public static T GetPackagedAsset<T>(string path) where T : Object
        {
            var target = AssetDatabase.LoadAssetAtPath<T>("Packages/com.eren.2dcomplete/" + path);
            if (target is null)
                target = AssetDatabase.LoadAssetAtPath<T>("Assets/2DGamekitComplete/" + path);
            if (target is null)
                throw new Exception(@$"Asset not found at: {path}");
            return target;
        }
    }
}