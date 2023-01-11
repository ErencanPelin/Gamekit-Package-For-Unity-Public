using Gamekit2D.Runtime.Utils;
using UnityEditor;

namespace Gamekit2D.Editor
{
    [CustomEditor(typeof(GamekitSettings))]
    public class GamekitSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Go to \"Eren's Gamekit\" > Gamekit Settings to easily modify the kit settings.",
                MessageType.Info, true);
           // base.OnInspectorGUI();
        }
    }
}