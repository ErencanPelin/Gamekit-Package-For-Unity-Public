using Gamekit2D.Runtime.Actor;
using Gamekit2D.Runtime.Enums;
using UnityEditor;
using UnityEngine;

namespace Gamekit2D.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Combat))]
    public class CombatEditor : UnityEditor.Editor
    {
        private Combat x;
        
        private SerializedProperty m_combatType;
        private SerializedProperty m_bulletParticles;
        private SerializedProperty m_gunTransform;
        private SerializedProperty m_attackType;
        private SerializedProperty m_attackDirection;
        private SerializedProperty m_invertAttackDirection;
        private SerializedProperty m_target;
        private SerializedProperty m_holdToAttack;
        private SerializedProperty m_doesTakeDamage;
        private SerializedProperty m_damageDealt;
        private SerializedProperty m_attackCooldown;
        private SerializedProperty m_invulnerabilityCooldown;
        private SerializedProperty m_knockBackForce;
        private SerializedProperty m_attackRaySettings;
        private SerializedProperty m_onGetHit;
        private SerializedProperty m_onAttack;

        private bool showDirection;
        private bool showCombat;
        private bool showEvents;
        
        private void OnEnable()
        {
            x = target as Combat;
            
            m_combatType = serializedObject.FindProperty("combatType");
            m_bulletParticles = serializedObject.FindProperty("bulletParticles");
            m_gunTransform = serializedObject.FindProperty("gunTransform");
            m_attackType = serializedObject.FindProperty("attackType");
            m_attackDirection = serializedObject.FindProperty("attackDirection");
            m_invertAttackDirection = serializedObject.FindProperty("invert");
            m_target = serializedObject.FindProperty("target");
            m_holdToAttack = serializedObject.FindProperty("holdToAttack");
            m_doesTakeDamage = serializedObject.FindProperty("doesTakeDamage");
            m_damageDealt = serializedObject.FindProperty("damageDealt");
            m_attackCooldown = serializedObject.FindProperty("attackCooldown");
            m_invulnerabilityCooldown = serializedObject.FindProperty("invulnerableCooldown");
            m_knockBackForce = serializedObject.FindProperty("knockBackForce");
            m_attackRaySettings = serializedObject.FindProperty("attackRaySettings");
            m_onGetHit = serializedObject.FindProperty("onGetHit");
            m_onAttack = serializedObject.FindProperty("onAttack");
        }

        public override void OnInspectorGUI()
        {
            var defCol = GUI.backgroundColor;
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            
            m_attackType.enumValueIndex = (int)(AttackType)EditorGUILayout.EnumPopup("Attack Type", (AttackType)m_attackType.enumValueIndex);
            m_combatType.enumValueIndex = (int)(CombatType)EditorGUILayout.EnumPopup("Combat Type", (CombatType)m_combatType.enumValueIndex);
            if (m_combatType.enumValueIndex is (int)CombatType.shooter)
            {
                EditorGUI.BeginDisabledGroup(true);
                m_gunTransform.objectReferenceValue = EditorGUILayout.ObjectField("Gun Transform", m_gunTransform.objectReferenceValue, typeof(Transform), false);
                EditorGUI.EndDisabledGroup();
                if (m_bulletParticles.objectReferenceValue is null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(new GUIContent());
                    if (GUILayout.Button("Create Bullet Particles"))
                    {
                        Debug.Log("Created new Bullet Particles object!");
                        var newParticles = Instantiate(
                            AssetSearch.GetPackagedAsset<GameObject>("DefaultAssets/Templates/Prefabs/Gun.prefab"), x.transform);
                        newParticles.transform.localPosition = Vector3.zero;
                        newParticles.name = newParticles.name.Remove(newParticles.name.Length - 7);
                        m_bulletParticles.objectReferenceValue = newParticles.GetComponentInChildren<ParticleSystem>();
                        m_gunTransform.objectReferenceValue = newParticles;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.Space(10f);

            //direction settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showDirection = EditorGUILayout.BeginFoldoutHeaderGroup(showDirection, "SpriteDirection");
            GUI.backgroundColor = defCol;
            if (showDirection)
            {
                m_attackDirection.enumValueIndex = (int)(AttackDirection)EditorGUILayout.EnumPopup("Attack SpriteDirection",
                    (AttackDirection)m_attackDirection.enumValueIndex);
                if (m_attackDirection.enumValueIndex is (int)AttackDirection.targetDirectionX
                    or (int)AttackDirection.targetDirectionXY)
                    m_target.objectReferenceValue = EditorGUILayout.ObjectField("Attack Target",
                        m_target.objectReferenceValue, typeof(Transform), true) as Transform;
                m_invertAttackDirection.boolValue =
                    EditorGUILayout.Toggle("Invert SpriteDirection", m_invertAttackDirection.boolValue);
                EditorGUILayout.Space(10f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            //combat settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showCombat = EditorGUILayout.BeginFoldoutHeaderGroup(showCombat, "Combat & RayCast");
            GUI.backgroundColor = defCol;
            if (showCombat)
            {
                m_holdToAttack.boolValue = EditorGUILayout.Toggle("Hold to Attack", m_holdToAttack.boolValue);
                m_doesTakeDamage.boolValue = EditorGUILayout.Toggle("Take Damage", m_doesTakeDamage.boolValue);
                m_damageDealt.floatValue = EditorGUILayout.FloatField("Damage Dealt", m_damageDealt.floatValue);
                if (m_doesTakeDamage.boolValue)
                {
                    //add health component if its missing
                    if (!x.TryGetComponent(out Health _))
                        x.gameObject.AddComponent<Health>();
                }
                
                m_attackCooldown.floatValue =
                    Mathf.Clamp(EditorGUILayout.FloatField("Attack Cooldown", m_attackCooldown.floatValue), 0,
                        float.MaxValue);
                m_invulnerabilityCooldown.floatValue = Mathf.Clamp(
                    EditorGUILayout.FloatField("Invulnerable Cooldown", m_invulnerabilityCooldown.floatValue), 0,
                    float.MaxValue);
                m_knockBackForce.floatValue =
                    Mathf.Clamp(EditorGUILayout.FloatField("Knockback Force", m_knockBackForce.floatValue), 0f,
                        float.MaxValue);
                EditorGUILayout.Space(10f);
                EditorGUILayout.PropertyField(m_attackRaySettings);
                EditorGUILayout.Space(10f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            //event settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(showEvents, "Events");
            GUI.backgroundColor = defCol;
            if (showEvents)
            {
                EditorGUILayout.PropertyField(m_onGetHit);
                EditorGUILayout.PropertyField(m_onAttack);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }

        public void OnSceneGUI()
        {
            Handles.color = Color.green;
            Handles.DrawLine(x.transform.position + (Vector3)x.attackRaySettings.rayStartOffset, 
                x.transform.position + (Vector3)x.attackRaySettings.rayStartOffset + Vector3.right * x.attackRaySettings.rayLength, 
                1f);
        }
    }
}