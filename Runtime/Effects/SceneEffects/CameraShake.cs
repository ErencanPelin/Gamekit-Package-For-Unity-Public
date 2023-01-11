using System.Collections;
using UnityEngine;

namespace Gamekit2D.Runtime.Effects.SceneEffects
{
    /// <summary>
    /// Attach to the Camera GameObject, the Perform method can be called to do the shake behaviour
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu(" Eren Kit/Effects/Camera Shake")]
    public class CameraShake : MonoBehaviour, IEffect
    {
        [Tooltip("How long the camera shakes for")]
        [SerializeField] private float shakeDuration;
        [Tooltip("How much the camera shakes")]
        [SerializeField] private float strength = 1f;
        
        private float duration;
        private Vector3 originalPos;
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
            duration = shakeDuration;
            originalPos = transform.localPosition;
            StopCoroutine(Shake());
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            var endTime = Time.time + duration;
            while (duration > 0) 
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * strength;
                duration -= fakeDelta;

                yield return null;
            }
            transform.localPosition = originalPos;
        }
    }
}