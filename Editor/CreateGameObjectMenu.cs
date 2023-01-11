using Gamekit2D.Runtime.LevelTiles;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    public static class CreateGameObjectMenu
    {
        [MenuItem("GameObject/2D Object/Templates/Interactable/Switch", priority = -11)]
        private static void CreateSwitch() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Switch.prefab");
        [MenuItem("GameObject/2D Object/Templates/Interactable/Button")]
        private static void CreateButton() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Button.prefab");
        
        [MenuItem("GameObject/2D Object/Templates/Collectable")]
        private static void CreateCollectable() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Collectable.prefab");
        
        [MenuItem("GameObject/2D Object/Templates/Level Exit")]
        private static void CreateExit() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/LevelExit.prefab");
        
        [MenuItem("GameObject/2D Object/Templates/Checkpoint")]
        private static void CreateCheckpoint() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Checkpoint.prefab");
        
        [MenuItem("GameObject/2D Object/Templates/SpikeTile")]
        private static void CreateSpike() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/SpikeTile.prefab");
        
        [MenuItem("GameObject/2D Object/Templates/Air Gust")]
        private static void CreateAirGust() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/AirGust.prefab");
        
       /* [MenuItem("GameObject/2D Object/Templates/Platformer/2 Way Platform")]
        private static void CreatePlatform() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Platformer/One-WayPlatform.prefab");*/
            
        [MenuItem("GameObject/2D Object/Templates/Platformer/Moving Platform")]
        private static void CreateMovingPlatform()
        {
            var platform = CreateObjectByPath("DefaultAssets/Templates/Prefabs/Platformer/MovingPlatform.prefab").GetComponent<MovingPlatform>();
            platform.points.Add(platform.transform.position);
            platform.points.Add(platform.transform.position + (Vector3.right * 5f));
        }
        [MenuItem("GameObject/2D Object/Templates/Platformer/Enemy")]
        private static void CreateEnemy_PF() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Platformer/Demo_Enemy.prefab");
        [MenuItem("GameObject/2D Object/Templates/Platformer/Demo Player")]
        private static void CreatePlayer_PF() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Platformer/Demo_Player.prefab");
        [MenuItem("GameObject/2D Object/Templates/Platformer/Platformer World")]
        private static void CreateWorld_PF() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/Platformer/Platformer_World.prefab", Vector3.zero);
        
        [MenuItem("GameObject/2D Object/Templates/Topdown/Topdown World", priority = 0)]
        private static void CreateWorld_TD() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/TopDown/Topdown_World.prefab", Vector3.zero);
        
        [MenuItem("GameObject/2D Object/Templates/Topdown/Demo Player")]
        private static void CreatePlayer_TD() =>
            CreateObjectByPath("DefaultAssets/Templates/Prefabs/TopDown/Demo_Player.prefab");
        
        private static GameObject CreateObjectByPath(string path)
        {
            var go = Object.Instantiate(AssetSearch.GetPackagedAsset<GameObject>(path),
                Vector2.zero, Quaternion.identity);
            go.transform.position = GetScenePos();
            go.name = go.name.Remove(go.name.Length - 7);
                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go; //select the object in the hierarchy window
            EditorGUIUtility.PingObject(go); //highlight the object in the hierarchy window
            return go;
        }
        
        private static GameObject CreateObjectByPath(string path, Vector3 pos)
        {
            var go = Object.Instantiate(AssetSearch.GetPackagedAsset<GameObject>(path),
                Vector2.zero, Quaternion.identity);
            go.transform.position = pos;
            go.name = go.name.Remove(go.name.Length - 7);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
            EditorGUIUtility.PingObject(go);
            return go;
        }

        private static Vector2 GetScenePos()
        {
            var worldRay = SceneView.lastActiveSceneView.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1.0f));
            return worldRay.origin + worldRay.direction;
        }
    }
}