using UnityEditor;
using Gamekit2D.Runtime.Actor;
using Gamekit2D.Runtime.Enums;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SpriteDirection))]
    public class DirectionEditor : UnityEditor.Editor
    {
        private SpriteDirection x;
        
        private SerializedProperty m_invertDirection;
        private SerializedProperty m_faceDirection;
        private SerializedProperty m_lookAtTarget;
 
        private void OnEnable()
        {
            x = target as SpriteDirection;
            m_invertDirection = serializedObject.FindProperty("invertDirection");
            m_faceDirection = serializedObject.FindProperty("faceDirection");
            m_lookAtTarget = serializedObject.FindProperty("lookAtTarget");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            m_faceDirection.enumValueIndex = (int)(DirectionType)EditorGUILayout.EnumPopup("Look At", (DirectionType)m_faceDirection.enumValueIndex);
            if (m_faceDirection.enumValueIndex is (int)DirectionType.target)
            {
                m_lookAtTarget.objectReferenceValue = EditorGUILayout.ObjectField("Look at Target",
                    m_lookAtTarget.objectReferenceValue, typeof(Transform), true) as Transform;
                EditorGUILayout.Space(10f);
            }

            m_invertDirection.boolValue = EditorGUILayout.Toggle("Invert SpriteDirection", m_invertDirection.boolValue);

            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}