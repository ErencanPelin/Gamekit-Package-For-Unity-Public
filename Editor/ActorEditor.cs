using Gamekit2D.Runtime.Actor;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Actor))]
    public class ActorEditor : UnityEditor.Editor
    {
        private SerializedProperty m_useCheckpoints;
        private SerializedProperty m_canResetSelf;
        
        private Actor x;

        private void OnEnable()
        {
            x = target as Actor;
            m_useCheckpoints = serializedObject.FindProperty("useCheckpoints");
            m_canResetSelf = serializedObject.FindProperty("canResetSelf");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            m_useCheckpoints.boolValue = EditorGUILayout.Toggle("Use Checkpoints", m_useCheckpoints.boolValue);
            m_canResetSelf.boolValue = EditorGUILayout.Toggle(
                new GUIContent("Can reset self", "when true, the user can press 'R' to reset the level and go to the last checkpoint"), 
                m_canResetSelf.boolValue);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}