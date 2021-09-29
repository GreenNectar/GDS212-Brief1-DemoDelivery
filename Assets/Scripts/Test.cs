using DemoDelivery.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace DemoDelivery.Testing
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private float power = 10f;
        [SerializeField]
        private float torquePower = 5f;
        [SerializeField]
        private float radius = 2f;

        private int currentTap = 0;

        InputManager inputManager;

        private void OnEnable()
        {
            inputManager = InputManager.Instance;
            inputManager.onStartTouch.AddListener(Explode);
        }

        public void Explode(Vector2 position)
        {
            currentTap++;
            Debug.Log($"Taps {currentTap}");

            Explosion.Explode(position, radius, power, torquePower);
        }
    }
}