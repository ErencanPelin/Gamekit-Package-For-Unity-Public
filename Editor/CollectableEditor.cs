using Gamekit2D.Runtime.Collectables;
using UnityEditor;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Collectable))]
    public class CollectableEditor : UnityEditor.Editor
    {
        private SerializedProperty m_onCollect;
        
        private void OnEnable()
        {
            m_onCollect = serializedObject.FindProperty("onCollect");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(m_onCollect);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}