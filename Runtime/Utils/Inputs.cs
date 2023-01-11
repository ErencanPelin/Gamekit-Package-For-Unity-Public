using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Gamekit2D.Runtime.Utils
{
    public sealed class Inputs : Singleton<Inputs>
    {
        [SerializeField] private PlayerInput playerInput;

        public static InputAction move;
        public static InputAction dash;
        public static InputAction look;
        public static InputAction leftMouse;
        public static InputAction reset;
        public static InputAction rightMouse;
        public static InputAction scrollWheel;
        private Camera mainCam;
        /// <summary>
        /// position of the mouse in the game scene - in game units, not screen units
        /// </summary>
        public static Vector3 mousePos;

        private Transform playerTransform;

        protected override void Awake()
        {
            base.Awake();
            playerInput.actions = AssetSearch.FindGamekitSettings().inputActions; //AssetSearch.GetPackagedAsset<GamekitSettings>("GamekitSettings.asset").inputActions;
            move = playerInput.actions["Move"];
            dash = playerInput.actions["Dash"];
            look = playerInput.actions["Look"];
            leftMouse = playerInput.actions["Lmb"];
            rightMouse = playerInput.actions["Rmb"];
            reset = playerInput.actions["Reset"];
            scrollWheel = playerInput.actions["ScrollWheel"];
            
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene arg0, LoadSceneMode loadSceneMode)
        {
            mainCam = Camera.main;
            if (mainCam is null)
            {
                enabled = false;
                throw new Exception(
                    "Couldn't find main camera! Make sure the camera in the scene has the tag \"MainCamera\"");
            }
        }
        
        private void Update()
        {
            UpdateMousePosition();
        }

        private void UpdateMousePosition()
        {
            switch (playerInput.currentControlScheme)
            {
                case "Gamepad":
                    mousePos = mainCam.ScreenToWorldPoint(new Vector3(mainCam.scaledPixelWidth / 2f,
                        mainCam.scaledPixelHeight / 2f)) + (Vector3)look.ReadValue<Vector2>();
                    break;
                default:
                    mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    mousePos.z = 0;
                    break;
            }
        }
    }
}