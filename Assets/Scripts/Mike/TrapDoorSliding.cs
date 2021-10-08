using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Michael

namespace DemoDelivery.Gameplay
{
    public class TrapDoorSliding : TrapDoor
    {
        private Vector3 startPosition;
        public bool inverted;
        public float speed;
        public float distance;
        public bool startsOpen;


        private float currentDistance;

        private void Start()
        {
            startPosition = transform.position;
            ResetTrapdoor();
        }

        private void Update()
        {
            if (isOpen)
            {
                currentDistance += speed * Time.deltaTime * (startsOpen ? -1f : 1f);
            }
            else
            {
                currentDistance -= speed * Time.deltaTime * (startsOpen ? -1f : 1f);
            }

            currentDistance = Mathf.Clamp(currentDistance, 0f, distance);
            transform.position = startPosition + transform.up * currentDistance * (inverted ? 1f : -1f );
        }

        protected override void ResetTrapdoor()
        {
            currentDistance = startsOpen ? distance : 0;
        }
    }
}