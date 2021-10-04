using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Michael, edited by Jarrad

namespace DemoDelivery.Gameplay
{
    public class TrapDoor : MonoBehaviour
    {
        [SerializeField]
        private int doorNumber;
        protected bool isOpen;

        private void OnDisable()
        {
            EventManager.onButtonPress.RemoveListener(OpenTrapDoor);
            EventManager.onButtonRelease.RemoveListener(CloseTrapDoor);
        }

        private void OnEnable()
        {
            EventManager.onButtonPress.AddListener(OpenTrapDoor);
            EventManager.onButtonRelease.AddListener(CloseTrapDoor);
        }


        void OpenTrapDoor(int doorNumber)
        {
            if (this.doorNumber == doorNumber)
            {
                isOpen = true;
            }
        }

        void CloseTrapDoor(int doorNumber)
        {
            if (this.doorNumber == doorNumber)
            {
                isOpen = false;
            }
        }
    }
}