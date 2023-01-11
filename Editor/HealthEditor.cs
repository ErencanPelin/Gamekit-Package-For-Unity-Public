using Gamekit2D.Runtime.Actor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Health))]
    public class HealthEditor : UnityEditor.Editor
    {
        private Health x;
        
        private SerializedProperty m_maxHealth;
        private SerializedProperty m_healthSlider;
        private SerializedProperty m_onDeath;
        
        private void OnEnable()
        {
            x = target as Health;
            m_maxHealth = serializedObject.FindProperty("maxHealth");
            m_healthSlider = serializedObject.FindProperty("healthSlider");
            m_onDeath = serializedObject.FindProperty("onDeath");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            m_maxHealth.floatValue = Mathf.Clamp(EditorGUILayout.FloatField("Max Health", m_maxHealth.floatValue), 0, float.MaxValue);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.LabelField($"Current Heath:\t{x.curHealth}");
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space(20f);
            m_healthSlider.objectReferenceValue = EditorGUILayout.ObjectField("Health Slider", m_healthSlider.objectReferenceValue, typeof(Slider), true) as Slider;
            EditorGUILayout.Space(20f);
            EditorGUILayout.LabelField("Events");
            EditorGUILayout.PropertyField(m_onDeath);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}