using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Michael, edited by Jarrad

namespace DemoDelivery.Gameplay
{
    public class TrapDoorRotate : TrapDoor
    {
        private float startRotation;
        public float rotation;
        public bool turnsLeft;
        public float speed;

        private float currentRotation;

        private void Start()
        {
            // setting the starting rotation to startRotation
            startRotation = transform.eulerAngles.z;
        }

        private void Update()
        {
            currentRotation += Time.deltaTime * speed * (isOpen ? 1f : -1f);
            currentRotation = Mathf.Clamp(currentRotation, 0f, rotation);

            transform.rotation = Quaternion.Euler(0f, 0f, startRotation + (turnsLeft ? currentRotation : -currentRotation));

            // Completely unnecessary
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, startRotation), );

            //// the desired rotation equals if isOpen is true startRotation plus number given for rotation
            //float wantedRotation = isOpen ? startRotation + rotation : startRotation;

            //// absolute number (no negatives) the difference between the current angle on the z axis compared to the wanted angle
            //// if it is greater (unsure at this point need clarification)
            //if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, wantedRotation)) > speed * Time.deltaTime)
            //{
            //    // the rotation equals the z axis for rotation adding if it is still considered open 1f or -1f depending (possibly to make it stop need clarification)
            //    transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + ((isOpen ? 1f : -1f) * (turnsLeft ? 1f : -1f) * speed * Time.deltaTime));
            //}
            //else
            //{
            //    // sets the transform rotation to the wanted angle
            //    transform.rotation = Quaternion.Euler(0, 0, wantedRotation);
            //}
        }

        protected override void ResetTrapdoor()
        {
            currentRotation = 0f;
            //transform.rotation = Quaternion.Euler(0, 0, startRotation);
        }
    }
}