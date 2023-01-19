using System;
using Gamekit2D.Runtime.Actor;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Effects))]
    public class EffectsEditor : UnityEditor.Editor
    {
        private SerializedProperty m_isGlobal;
        private SerializedProperty m_lifeTime;

        private void OnEnable()
        {
            m_isGlobal = serializedObject.FindProperty("isGlobal");
            m_lifeTime = serializedObject.FindProperty("lifeTime");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            m_isGlobal.boolValue = EditorGUILayout.Toggle("Is Global", m_isGlobal.boolValue);
            m_lifeTime.floatValue = Mathf.Clamp(EditorGUILayout.FloatField("Lifetime", m_lifeTime.floatValue), 0,
                float.MaxValue);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}