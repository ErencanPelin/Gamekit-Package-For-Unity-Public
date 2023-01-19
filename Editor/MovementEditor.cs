using Gamekit2D.Runtime.Actor;
using Gamekit2D.Runtime.Enums;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Movement))]
    public class MovementEditor : UnityEditor.Editor
    {
        private Movement x;
        
        private SerializedProperty m_actorType;
        private SerializedProperty m_gravityForce;
        private SerializedProperty m_maxFallSpeed;
        private SerializedProperty m_maxJumpVelocity;
        private SerializedProperty m_jumpForce;
        private SerializedProperty m_airControl;
        private SerializedProperty m_canDoubleJump;
        private SerializedProperty m_doubleJumpMin;
        private SerializedProperty m_doubleJumpMax;
        private SerializedProperty m_inputType;
        private SerializedProperty m_target;
        private SerializedProperty m_moveSpeed;
        private SerializedProperty m_moveSmoothing;
        private SerializedProperty m_canDash;
        private SerializedProperty m_dashForce;
        private SerializedProperty m_dashCooldown;
        private SerializedProperty m_rayCastSettings;
        private SerializedProperty m_OnJump;
        private SerializedProperty m_OnDash;
        private SerializedProperty m_OnDoubleJump;
        
        private bool showRaySettings;
        private bool showMotion;
        private bool showDash;
        private bool showJumping;
        private bool showEvents;
        
        private void OnEnable()
        {
            x = target as Movement;

            m_actorType = serializedObject.FindProperty("actorType");
            m_gravityForce = serializedObject.FindProperty("gravityForce");
            m_maxFallSpeed = serializedObject.FindProperty("maxFallSpeed");
            m_maxJumpVelocity = serializedObject.FindProperty("maxJumpVelocity");
            m_jumpForce = serializedObject.FindProperty("jumpForce");
            m_airControl = serializedObject.FindProperty("airControlPercent");
            m_canDoubleJump = serializedObject.FindProperty("canDoubleJump");
            m_doubleJumpMin = serializedObject.FindProperty("doubleJumpMinThreshold");
            m_doubleJumpMax = serializedObject.FindProperty("doubleJumpMaxThreshold");
            m_inputType = serializedObject.FindProperty("inputType");
            m_target = serializedObject.FindProperty("target");
            m_moveSpeed = serializedObject.FindProperty("moveSpeed");
            m_moveSmoothing = serializedObject.FindProperty("moveSmoothing");
            m_canDash = serializedObject.FindProperty("canDash");
            m_dashForce = serializedObject.FindProperty("dashForce");
            m_dashCooldown = serializedObject.FindProperty("dashCooldown");
            m_rayCastSettings = serializedObject.FindProperty("groundCheckSettings");
            m_OnJump = serializedObject.FindProperty("onJump");
            m_OnDash = serializedObject.FindProperty("onDash");
            m_OnDoubleJump = serializedObject.FindProperty("onDoubleJump");
        } 

        public override void OnInspectorGUI()
        {
            var defCol = GUI.backgroundColor;
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            m_actorType.enumValueIndex = (int)(ActorType)EditorGUILayout.EnumPopup("Actor Type", (ActorType)m_actorType.enumValueIndex);
            m_inputType.enumValueIndex = (int)(InputType)EditorGUILayout.EnumPopup(
                new GUIContent("Inputs Type", "Type of input this actor uses to move"), 
                (InputType)m_inputType.enumValueIndex);
            
            if(m_inputType.enumValueIndex is (int)InputType.target)
            {
                m_target.objectReferenceValue = EditorGUILayout.ObjectField(
                    new GUIContent("Follow Target", "The transform for this actor to track"), 
                    m_target.objectReferenceValue, typeof(Transform), true) as Transform;
            }
            
            EditorGUILayout.Space(10f);
            
            //motion settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showMotion = EditorGUILayout.BeginFoldoutHeaderGroup(showMotion, "Motion");
            GUI.backgroundColor = defCol;
            if (showMotion)
            {
                m_moveSpeed.floatValue = Mathf.Clamp(EditorGUILayout.FloatField("Move Speed", m_moveSpeed.floatValue),
                    0, float.MaxValue);
                m_moveSmoothing.floatValue =
                    EditorGUILayout.Slider("Move Smoothing", m_moveSmoothing.floatValue, 0f, 1f);
                EditorGUILayout.Space(10f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            //dash settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showDash = EditorGUILayout.BeginFoldoutHeaderGroup(showDash, "Dashing");
            GUI.backgroundColor = defCol;
            if (showDash)
            {
                m_canDash.boolValue = EditorGUILayout.Toggle("Can Dash", m_canDash.boolValue);
                if (m_canDash.boolValue)
                {
                    m_dashForce.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Dash Force", m_dashForce.floatValue), 0,
                            float.MaxValue);
                    m_dashCooldown.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Dash Cooldown", m_dashCooldown.floatValue), 0,
                            float.MaxValue);
                }
                EditorGUILayout.Space(10f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            //jumping & gravity settings
            if (m_actorType.enumValueIndex is (int)ActorType.platformer)
            {
                GUI.backgroundColor = new Color32(200, 200, 200, 255);
                showJumping = EditorGUILayout.BeginFoldoutHeaderGroup(showJumping, "Jumping & Gravity");
                GUI.backgroundColor = defCol;
                if (showJumping)
                {
                    m_gravityForce.floatValue = Mathf.Clamp(EditorGUILayout.FloatField(
                        new GUIContent("Gravity Force", "Force of gravity applied to the actor"),
                        m_gravityForce.floatValue), 0, float.MaxValue);
                    m_jumpForce.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Jump Force", m_jumpForce.floatValue), 0,
                            float.MaxValue);
                    m_airControl.floatValue = EditorGUILayout.Slider("Air Control", m_airControl.floatValue, 0, 1);
                    EditorGUILayout.Space(10f);
                    m_maxFallSpeed.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Max Fall Speed", m_maxFallSpeed.floatValue), 0,
                            float.MaxValue);
                    m_maxJumpVelocity.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Max Jump Speed", m_maxJumpVelocity.floatValue), 0,
                            float.MaxValue);
                    EditorGUILayout.Space(10f);

                    m_canDoubleJump.boolValue = EditorGUILayout.Toggle("Can Double Jump", m_canDoubleJump.boolValue);

                    if (m_canDoubleJump.boolValue)
                    {
                        EditorGUILayout.LabelField(new GUIContent("Double jump Threshold",
                            "The threshold in velocity in which the actor can double jump"));
                        EditorGUILayout.MinMaxSlider(ref x.doubleJumpMinThreshold, ref x.doubleJumpMaxThreshold,
                            -m_maxFallSpeed.floatValue, m_jumpForce.floatValue);
                        EditorGUI.BeginDisabledGroup(true);
                        EditorGUILayout.LabelField($"Min Velocity: {Mathf.RoundToInt(m_doubleJumpMin.floatValue)}");
                        EditorGUILayout.LabelField($"Max Velocity: {Mathf.RoundToInt(m_doubleJumpMax.floatValue)}");
                        EditorGUI.EndDisabledGroup();
                    }
                    EditorGUILayout.Space(10f);

                    EditorGUILayout.PropertyField(m_rayCastSettings);
                    EditorGUILayout.Space(10f);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
            else
            {
                m_canDoubleJump.boolValue = false;
            }

            //event settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(showEvents, "Events");
            GUI.backgroundColor = defCol;
            if (showEvents)
            {
                if (m_canDash.boolValue || m_canDoubleJump.boolValue ||
                    m_actorType.enumValueIndex is (int)ActorType.platformer)
                {
                    if (m_actorType.enumValueIndex is (int)ActorType.platformer)
                        EditorGUILayout.PropertyField(m_OnJump);
                    if (m_canDash.boolValue) EditorGUILayout.PropertyField(m_OnDash);
                    if (m_canDoubleJump.boolValue) EditorGUILayout.PropertyField(m_OnDoubleJump);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnSceneGUI()
        {
            if (m_actorType.enumValueIndex is not (int)ActorType.platformer) return;
            Handles.color = new Color(.6f, .8f, 0.9f);
            Handles.DrawLine((Vector2)x.transform.position + x.groundCheckSettings.rayStartOffset, 
                (Vector2)x.transform.position + x.groundCheckSettings.rayStartOffset + Vector2.down * x.groundCheckSettings.rayLength);
        }
    }
}