using System;
using System.Collections.Generic;
using Gamekit2D.Runtime.Enums;
using Gamekit2D.Runtime.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gamekit2D.Editor
{
    public class GamekitSettingsWindow : EditorWindow
    {
        private static GamekitSettings kitSettings;
        private static SerializedObject s_kitSettings;
        private static SerializedProperty m_sceneTransition;
        private static SerializedProperty m_transitionColor;
        private static SerializedProperty m_inputActions;

        private bool showInput;
        private bool showTransitions;
        
        [MenuItem ("Eren's Gamekit/Gamekit Settings", false, -100)]
        public static void ShowWindow ()
        {
            GetWindow(typeof(GamekitSettingsWindow), mouseOverWindow, "Gamekit Settings");
            kitSettings = AssetSearch.FindGamekitSettings(); //AssetSearch.GetPackagedAsset<GamekitSettings>("GamekitSettings.asset");
            s_kitSettings = new SerializedObject(kitSettings);
            m_sceneTransition = s_kitSettings.FindProperty("sceneTransition");
            m_transitionColor = s_kitSettings.FindProperty("transitionColor");
            m_inputActions = s_kitSettings.FindProperty("inputActions");
        }

        private void OnEnable()
        {
            GetWindow(typeof(GamekitSettingsWindow), mouseOverWindow, "Gamekit Settings");
            kitSettings = AssetSearch.FindGamekitSettings(); //AssetSearch.GetPackagedAsset<GamekitSettings>("GamekitSettings.asset");
            s_kitSettings = new SerializedObject(kitSettings);
            m_sceneTransition = s_kitSettings.FindProperty("sceneTransition");
            m_transitionColor = s_kitSettings.FindProperty("transitionColor");
            m_inputActions = s_kitSettings.FindProperty("inputActions");
        }

        private void OnGUI()
        {
            var defCol = GUI.backgroundColor;
            s_kitSettings.Update();
            EditorGUI.BeginChangeCheck();

            var style = GUI.skin;
            style.label.wordWrap = true;
            style.label.margin = new RectOffset(10, 10, 10, 10);
            var labelSize = style.label.fontSize;
            style.label.fontSize = 24;
            EditorGUILayout.LabelField("Eren's Complete 2D Game kit", new GUIStyle(style.label));
            style.label.fontSize = labelSize;
            EditorGUILayout.LabelField("Global settings the game kit uses to work. Adjustments made here reflect throughout the entire game kit" +
                                       " and incorrect configuration of some settings may result in unexpected outcomes or issues. If you are unsure" +
                                       " what the settings do, please refer to the documentation.", new GUIStyle(style.label));
            EditorGUILayout.Separator();
            if (m_inputActions.objectReferenceValue is null)
            {
                EditorGUILayout.HelpBox("No input action selected. You must select an input action asset. Expect errors otherwise.", MessageType.Error, true);
            }
            else
            {
                var issueList = VerifyInputAsset();
                if (issueList.Count > 0)
                {
                    var warning = CreateWarning(issueList);
                    EditorGUILayout.HelpBox(warning, MessageType.Error, true);
                }
            }
            
            //input settings
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showInput = EditorGUILayout.BeginFoldoutHeaderGroup(showInput, "Input");
            GUI.backgroundColor = defCol;
            if (showInput)
            {
                m_inputActions.objectReferenceValue = EditorGUILayout.ObjectField("    Input Bindings",
                    m_inputActions.objectReferenceValue, typeof(InputActionAsset), true);
                EditorGUILayout.Space(10f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            //transition settings
            
            GUI.backgroundColor = new Color32(200, 200, 200, 255);
            showTransitions = EditorGUILayout.BeginFoldoutHeaderGroup(showTransitions, "Scene Transitions");
            GUI.backgroundColor = defCol;
            if (showTransitions)
            {
                m_sceneTransition.enumValueIndex = (int)(SceneTransition)EditorGUILayout.EnumPopup("    Scene Transition Style", (SceneTransition)m_sceneTransition.enumValueIndex);
                m_transitionColor.colorValue = EditorGUILayout.ColorField(
                    new GUIContent("    Transition Color", "The color of the scene transition"),
                    m_transitionColor.colorValue);
                EditorGUILayout.Space(10f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            EditorGUI.EndChangeCheck();
            s_kitSettings.ApplyModifiedProperties();
        }

        private List<string> VerifyInputAsset()
        {
            var issueList = new List<string>();
            var actionMap = (InputActionAsset)m_inputActions.objectReferenceValue;
            try { _ = actionMap["Move"]; }catch { issueList.Add("Move"); }
            try { _ = actionMap["Dash"]; }catch { issueList.Add("Dash"); }
            try { _ = actionMap["Look"]; }catch { issueList.Add("Look"); }
            try { _ = actionMap["Lmb"]; }catch { issueList.Add("Lmb"); }
            try { _ = actionMap["Reset"]; }catch { issueList.Add("Reset"); }
            try { _ = actionMap["ScrollWheel"]; }catch { issueList.Add("ScrollWheel"); }
            return issueList;
        }

        private string CreateWarning(List<string> issueList)
        {
            var s = "The selected input action asset is missing the following input bindings; ";
            foreach (var i in issueList)
                s += i + ", ";
            s += "\nExpect errors to be thrown on play.";
            return s;
        }
    }
}