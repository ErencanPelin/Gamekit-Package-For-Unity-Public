using System.Collections;
using UnityEngine;

namespace Gamekit2D.Runtime.Effects.SceneEffects
{
    /// <summary>
    /// SceneEffect, temporarily freezes time for a very short period of time. Can be invoked as an event by other objects in the inspector
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu(" Eren Kit/Effects/Time Stutter")]
    public class TimeStutter : MonoBehaviour, IEffect
    {
        [SerializeField] private float originalDuration = 0.1f;
        private float duration;
        private float timeAtCurrentFrame;
        private float timeAtLastFrame;
        private float fakeDelta;
        
        private void Update() 
        {
            timeAtCurrentFrame = Time.realtimeSinceStartup;
            fakeDelta = timeAtCurrentFrame - timeAtLastFrame;
            timeAtLastFrame = timeAtCurrentFrame; 
        }
        
        public void Play()
        {
            duration = originalDuration;
            StopCoroutine(Stutter());
            StartCoroutine(Stutter());
        }

        private IEnumerator Stutter()
        {
            var endTime = Time.time + duration;
            while (duration > 0)
            {
                Time.timeScale = 0;
                duration -= fakeDelta;

                yield return null;
            }

            Time.timeScale = 1;
        }
    }
}