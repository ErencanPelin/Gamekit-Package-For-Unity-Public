using Gamekit2D.Runtime.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gamekit2D.Runtime.Cameras
{
    /// <summary>
    /// Attach to camera GameObject - applies the ability to zoom in/out towards the target
    /// </summary>
    [RequireComponent(typeof(Camera))]
    [DisallowMultipleComponent]
    [AddComponentMenu(" Eren Kit/Camera/Camera Zoom", -30)]
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float maxZoom = 1f; //closest the camera can be to the target
        [SerializeField] private float minZoom = 7f; //furthest the camera can be from the target
        [SerializeField] private float zoomSpeed = 0.01f;
        
        private Camera cam;
        private float zoomValue; //current zoom value
        
        private void Awake()
        {
            cam = GetComponent<Camera>();
            Inputs.scrollWheel.started += Zoom;
            zoomValue = cam.orthographicSize;
        }

        private void Zoom(InputAction.CallbackContext callbackContext)
        {
                    var val = Inputs.scrollWheel.ReadValue<Vector2>().y;
                    zoomValue = Mathf.Clamp(zoomValue + (-zoomSpeed * val), maxZoom, minZoom);
            switch (cam.orthographic)
            {
                case true:
                {
                    cam.orthographicSize = zoomValue;
                    return;
                }
                case false:
                {
                    var pos = cam.transform.position;
                    pos.z = -zoomValue;
                    cam.transform.position = pos;
                    return;
                }
            }

        }
    }
}