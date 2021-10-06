using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    public class GameplayUIController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Animator animator;

        [Header("Play Button")]
        [SerializeField]
        private UnityEngine.UI.Button playButton;
        [SerializeField]
        private Image playButtonImage;
        [SerializeField]
        private Sprite playSprite;
        [SerializeField]
        private Sprite stopSprite;

        [Header("Other Buttons")]
        [SerializeField]
        private UnityEngine.UI.Button trashButton;
        [SerializeField]
        private UnityEngine.UI.Button optionsButton;

        [Header("Bomb Stuff")]
        [SerializeField]
        private Image explosiveBackground;
        [SerializeField]
        private TextMeshProUGUI explosivesRemaining;


        private void OnEnable()
        {
            EventManager.onPlayFinish.AddListener(HideUI);
            EventManager.onExplosivesAddedorRemoved.AddListener(ChangeExplosivesRemaining);
        }

        private void OnDisable()
        {
            EventManager.onPlayFinish.RemoveListener(HideUI);
            EventManager.onExplosivesAddedorRemoved.RemoveListener(ChangeExplosivesRemaining);
        }


        private void HideUI()
        {
            playButton.interactable = false;
            trashButton.interactable = false;
            optionsButton.interactable = false;

            animator.SetBool("Show", false);
        }

        private void ShowUI()
        {
            playButton.interactable = true;
            trashButton.interactable = true;
            optionsButton.interactable = true;

            animator.SetBool("Show", true);
        }

        private void ChangeExplosivesRemaining()
        {
            explosivesRemaining.text = $"{LevelManager.current.ExplosivesUsed}/{LevelManager.current.MaximumExplosives}";

            // Red when maxed, white when not
            explosiveBackground.color = LevelManager.current.ExplosivesUsed == LevelManager.current.MaximumExplosives ? Color.red : Color.white;
        }

        public void DestroyAllExplosives()
        {
            EventManager.onDestroyAllExplosives.Invoke();
        }

        public void TogglePlay()
        {
            if (GameManager.Instance.CurrentState != GameManager.GameState.End)
            {
                EventManager.onTogglePlay.Invoke();

                playButtonImage.sprite = GameManager.Instance.CurrentState == GameManager.GameState.Play ? stopSprite : playSprite;

                trashButton.interactable = GameManager.Instance.CurrentState == GameManager.GameState.Setup;
            }
        }
    }
}