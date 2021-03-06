using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Jarrad

namespace DemoDelivery.Gameplay {
    public class FinishLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                EventManager.onPlayFinish.Invoke();
            }
        }
    }
}