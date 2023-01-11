using System;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gamekit2D.Editor
{
    public static class CreatePrefabTemplateMenu
    {
        [MenuItem("Assets/Create/Actor Animator Controller/Platformer", priority = 410)]
        private static void Create_Animator_PF()
        {
            CheckRequiredFolders("Platformer");
            var sourcePath =
                GetActualAssetPath("DefaultAssets/Platformer/Animations/Player/PF_Player.controller");
            TryCopyFile(sourcePath, "Animations/Platformer/Platformer_AnimController");
        }
        [MenuItem("Assets/Create/Actor Animator Controller/TopDown [4 Directional]", priority = 410)]
        private static void Create_Animator_TD_4Way()
        {
            CheckRequiredFolders("TopDown");
            var sourcePath =
                GetActualAssetPath("DefaultAssets/TopDown/Animations/Player/TD_Player.controller");
            TryCopyFile(sourcePath, "Animations/TopDown/Topdown_AnimController");
        }
        [MenuItem("Assets/Create/Actor Animator Controller/TopDown [2 Directional]", priority = 410)]
        private static void Create_Animator_TD_2Way()
        {
            CheckRequiredFolders("TopDown");
            var sourcePath =
                GetActualAssetPath("DefaultAssets/TopDown/Animations/Player/TD_Player_2D.controller");
            TryCopyFile(sourcePath, "Animations/TopDown/Topdown_2Way_AnimController");
        }

        #region Utils
        private static void TryCopyFile(string assetPath, string destPath)
        {
            for (var i = 0; i < 20; i++)
            {
                if (AssetDatabase.LoadMainAssetAtPath(
                        $"Assets/{destPath}{(i == 0 ? "" : $" {i}")}.controller") is null) //this suffix is free
                {
                    //try copying the file
                    if (AssetDatabase.CopyAsset(assetPath, $"Assets/{destPath}{(i == 0 ? "" : $" {i}")}.controller"))
                    {
                        //successfully copied the asset
                        var t = AssetDatabase.LoadMainAssetAtPath(
                            $"Assets/{destPath}{(i == 0 ? "" : $" {i}")}.controller");
                        EditorGUIUtility.PingObject(t); //show the asset
                        AssetDatabase.SaveAssets(); //save changes
                        return;
                    }

                    throw new Exception($"Failed to copy {assetPath}");
                }
            }
            Debug.LogWarning("Out of available suffixes! {Max 20} Rename some files!");
        }

        private static void CheckRequiredFolders(string type)
        {
            //create animations folder
            if (!Directory.Exists("Assets/Animations"))
                AssetDatabase.CreateFolder("Assets", "Animations");
            
            //create topdown/platformer folder
            if (!Directory.Exists($"Assets/Animations/{type}"))
                AssetDatabase.CreateFolder("Assets/Animations", $"{type}");
        }
        
                
        private static string GetActualAssetPath(string path)
        {
            var target = AssetDatabase.LoadAssetAtPath<Object>("Packages/com.eren.2dcomplete/" + path);
            if (target is not null)
                return "Packages/com.eren.2dcomplete/" + path;
            //used if we are in development editor project
            return "Assets/2DGamekitComplete/" + path;
        }
        #endregion
    }
}
