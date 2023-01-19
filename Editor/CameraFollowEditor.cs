using Gamekit2D.Runtime.Cameras;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CameraFollow))]
    public class CameraFollowEditor : UnityEditor.Editor
    {
        private SerializedProperty m_target;
        private SerializedProperty m_followSpeed;
        private void OnEnable()
        {
            m_target = serializedObject.FindProperty("target");
            m_followSpeed = serializedObject.FindProperty("followSpeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            m_target.objectReferenceValue =
                EditorGUILayout.ObjectField("Target", m_target.objectReferenceValue, typeof(Transform), true);
            m_followSpeed.floatValue = EditorGUILayout.Slider("Follow Speed", m_followSpeed.floatValue, 0f, 1f);
            
            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();            
        }
    }
}