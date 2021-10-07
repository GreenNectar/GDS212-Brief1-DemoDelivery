using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Michael

namespace DemoDelivery.Gameplay
{
    public class TrapDoorSliding : TrapDoor
    {
        //private float startRotation;
        //public float rotation;
        //public bool turnsLeft;
        //public float speed;

        //private void Start()
        //{
        //    startRotation = transform.eulerAngles.z;
        //}

        //private void Update()
        //{
        //    float wantedRotation = isOpen ? startRotation + rotation : startRotation;
        //    if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, wantedRotation)) > speed * Time.deltaTime)
        //    {
        //        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + ((isOpen ? 1f : -1f) * (turnsLeft ? 1f : -1f) * speed * Time.deltaTime));
        //    }
        //    else
        //    {
        //        transform.rotation = Quaternion.Euler(0, 0, wantedRotation);
        //    }
        //}
        private Vector3 startPosition;
        public bool inverted;
        public float speed;
        public float distance;
        private float currentDistance;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            if (isOpen)
            {
                currentDistance += speed * Time.deltaTime;
            }
            else
            {
                currentDistance -= speed * Time.deltaTime;
            }

            currentDistance = Mathf.Clamp(currentDistance, 0f, distance);
            transform.position = startPosition + transform.up * currentDistance * (inverted ? 1f : -1f );
        }

        protected override void ResetTrapdoor()
        {
            currentDistance = 0;
        }
    }
}