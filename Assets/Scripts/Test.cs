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

        InputManager inputManager;

        private void OnEnable()
        {
            inputManager.onStartTouch.AddListener(Explode);
        }

        private void OnDisable()
        {
            inputManager.onStartTouch.RemoveListener(Explode);
        }

        private void Awake()
        {
            inputManager = InputManager.Instance;
        }

        public void Explode(Vector2 position)
        {
            Explosion.Explode(position, radius, power, torquePower);
        }
    }
}