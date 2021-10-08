using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Jarrad

namespace DemoDelivery.UI {
    public class SetupOutlineController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private void OnEnable()
        {
            EventManager.onTogglePlay.AddListener(ShowOutline);
        }

        private void OnDisable()
        {
            EventManager.onTogglePlay.RemoveListener(ShowOutline);
        }

        private void ShowOutline()
        {
            animator.SetBool("Show", GameManager.Instance.CurrentState == GameManager.GameState.Setup);
        }
    }
}