using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace DemoDelivery.Input
{
    public class InputManager : Singleton<InputManager>
    {
        private TouchControls touchControls;

        public UnityEvent<Vector2> onStartTouch;
        public UnityEvent<Vector2> onEndTouch;

        private void Awake()
        {
            touchControls = new TouchControls();
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
            touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
            touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
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
    }
}