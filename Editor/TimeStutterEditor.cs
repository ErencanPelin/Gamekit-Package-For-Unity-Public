using System;
using Gamekit2D.Runtime.Effects.SceneEffects;
using UnityEditor;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TimeStutter))]
    public class TimeStutterEditor : UnityEditor.Editor
    {
        private SerializedProperty m_duration;
        
        private void OnEnable()
        {
            m_duration = serializedObject.FindProperty("originalDuration");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            m_duration.floatValue = EditorGUILayout.FloatField("Duration", m_duration.floatValue);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}