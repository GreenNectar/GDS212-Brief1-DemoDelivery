using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using static UnityEngine.InputSystem.InputAction;

// Created by Jarrad

// Reference
// Samyam [YouTube]
// How to use Touch with NEW Input System - Unity Tutorial
// https://www.youtube.com/watch?v=ERAN5KBy2Gs&ab_channel=samyam

namespace DemoDelivery.Input
{
    public class InputManager : Singleton<InputManager>
    {
#if UNITY_WEBGL
        private TouchControlsWebgl touchControls;
#else
        private TouchControls touchControls;
#endif

        public UnityEvent<Vector2> onStartTouch;
        public UnityEvent<Vector2> onEndTouch;
        public UnityEvent<Vector2> onStartSecondTouch;
        public UnityEvent<Vector2> onEndSecondTouch;

        private void Awake()
        {

#if UNITY_WEBGL
            touchControls = new TouchControlsWebgl();
#else
            touchControls = new TouchControls();
#endif

            EnhancedTouchSupport.Enable();
        }

        private void OnEnable()
        {
            touchControls.Enable();
        }

        private void OnDisable()
        {
            touchControls.Disable();
        }

        private void Start()
        {
            touchControls.Touch.TouchPress.started += StartTouch;
            touchControls.Touch.TouchPress.canceled += EndTouch;
            touchControls.Touch.SecondTouch.started += StartSecondTouch;
            touchControls.Touch.SecondTouch.canceled += EndSecondTouch;
        }

        private void StartTouch(CallbackContext context)
        {
            if (onStartTouch != null)
            {
                Vector2 worldpos = Camera.main.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
                onStartTouch.Invoke(worldpos);
            }
        }

        private void EndTouch(CallbackContext context)
        {
            if (onEndTouch != null)
            {
                Vector2 worldpos = Camera.main.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
                onEndTouch.Invoke(worldpos);
            }
        }

        public Vector2 GetWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        }

        public Vector2 GetScreenPosition()
        {
            return touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        }

        private void StartSecondTouch(CallbackContext context)
        {
            if (onStartSecondTouch != null)
            {
                Vector2 worldpos = Camera.main.ScreenToWorldPoint(touchControls.Touch.SecondTouchPosition.ReadValue<Vector2>());
                onStartSecondTouch.Invoke(worldpos);
            }
        }

        private void EndSecondTouch(CallbackContext context)
        {
            if (onEndSecondTouch != null)
            {
                Vector2 worldpos = Camera.main.ScreenToWorldPoint(touchControls.Touch.SecondTouchPosition.ReadValue<Vector2>());
                onEndSecondTouch.Invoke(worldpos);
            }
        }

        public Vector2 GetSecondWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(touchControls.Touch.SecondTouchPosition.ReadValue<Vector2>());
        }

        public Vector2 GetSecondScreenPosition()
        {
            return touchControls.Touch.SecondTouchPosition.ReadValue<Vector2>();
        }
    }
}