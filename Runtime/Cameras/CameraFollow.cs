using System;
using UnityEngine;

namespace Gamekit2D.Runtime.Cameras
{
    /// <summary>
    /// Attach to Camera GameObject - applies basic Camera movement to follow a specified target
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu(" Eren Kit/Camera/Camera Follow", -30)]
    public class CameraFollow : MonoBehaviour
    {
        [Tooltip("Target for this camera to follow")]
        [SerializeField] private Transform target;
        [Tooltip("Speed which the camera moves at to keep up with its target")]
        [SerializeField] [Range(0, 1)] private float followSpeed = 0.5f;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            
            if (target != null) return;
            try
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch
            {
                Debug.LogWarning($"No Camera target set, camera follow will be disabled on {gameObject.name}");
                enabled = false; //disable the camera follow to reduce errors
            }
        }

        private void LateUpdate()
        {
            var pos = _transform.position;
            pos.x = Mathf.Lerp(pos.x, target.position.x, followSpeed);
            pos.y = Mathf.Lerp(pos.y, target.position.y, followSpeed);
            _transform.position = pos;
        }
    }
}