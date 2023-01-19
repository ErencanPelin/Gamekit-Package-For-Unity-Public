using System;
using Gamekit2D.Runtime.Effects.SceneEffects;
using UnityEditor;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CameraShake))]
    public class CameraShakeEditor : UnityEditor.Editor
    {
        private SerializedProperty m_shakeDuration;
        private SerializedProperty m_strength;
        
        private void OnEnable()
        {
            m_shakeDuration = serializedObject.FindProperty("shakeDuration");
            m_strength = serializedObject.FindProperty("strength");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            m_shakeDuration.floatValue = EditorGUILayout.FloatField("Duration", m_shakeDuration.floatValue);
            m_strength.floatValue = EditorGUILayout.FloatField("Strength", m_strength.floatValue);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}