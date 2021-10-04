using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Michael, edited by Jarrad

namespace DemoDelivery.Gameplay
{
    public class Button : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D parent;
        [SerializeField]
        private int doorNumber;

        private float startDistance;
        private bool isPressed = false;

        private void Start()
        {
            startDistance = Vector2.Distance(transform.position, parent.position);
        }

        private void Update()
        {
            if (!isPressed)
            {
                // If the button is halfway pressed down, we are pressed
                if (Vector2.Distance(transform.position, parent.position) <= 0.5f * startDistance)
                {
                    isPressed = true;
                    EventManager.onButtonPress.Invoke(doorNumber);
                }
            }
            else
            {
                // If the button is more than halfway unpressed, we are unpressed
                if (Vector2.Distance(transform.position, parent.position) > 0.5f * startDistance)
                {
                    isPressed = false;
                    EventManager.onButtonRelease.Invoke(doorNumber);
                }
            }

        }
    }
}