using System;
using System.Collections;
using Gamekit2D.Runtime.Enums;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Gamekit2D.Runtime.Utils
{
    public sealed class Scenes : Singleton<Scenes>
    {
        private static Scenes _instanceScenes;
        private static Animator transitionAnim;
        private static readonly int Show = Animator.StringToHash("show");

        protected override void Awake()
        {
            _instanceScenes = this;
            transitionAnim = GetComponentInChildren<Animator>();
            var settings = AssetSearch.FindGamekitSettings();
            transitionAnim.runtimeAnimatorController = GetTransitionFromSettings(settings);
            transitionAnim.GetComponentInChildren<Image>().color = settings.transitionColor;
            
            SceneManager.sceneLoaded += (_, _) => { transitionAnim.SetBool(Show, false); };
        }

        private static RuntimeAnimatorController GetTransitionFromSettings(GamekitSettings settings)
        {
            return settings.sceneTransition switch
            {
                SceneTransition.fade => AssetSearch.GetAsset<RuntimeAnimatorController>(
                    "SceneTransitions/fade/Transition_Fade_AnimControl"),
                SceneTransition.swipe => AssetSearch.GetAsset<AnimatorOverrideController>(
                    "SceneTransitions/swipe/Transition_Swipe_AnimControl"),
                SceneTransition.radialSwipe => AssetSearch.GetAsset<AnimatorOverrideController>(
                    "SceneTransitions/radialSwipe/Transition_RSwipe_AnimControl"),
                SceneTransition.circleGrow => AssetSearch.GetAsset<AnimatorOverrideController>(
                    "SceneTransitions/circleGrow/Transition_CircleGrow_AnimControl"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Loads a given scene with a scene transition
        /// </summary>
        /// <exception cref="MissingReferenceException">Calling this method and then trying to retrieve a value of a gameObject in the scene will throw an error since Unity is unloading the scene.</exception>
        /// <param name="sceneName">Scene to be loaded</param>
        public static void LoadScene(string sceneName)
        {
            if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
            {
                Debug.LogWarning($"Can't load that scene: {sceneName}! Make sure you've added that scene into the build settings first\n" +
                                 "'File/Build Settings'");
                return;
            }
            _instanceScenes.StartCoroutine(Transition(sceneName));
        }
        
        static IEnumerator Transition(string sceneName)
        {
            transitionAnim.SetBool(Show, true);
            yield return new WaitForSecondsRealtime(1f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}