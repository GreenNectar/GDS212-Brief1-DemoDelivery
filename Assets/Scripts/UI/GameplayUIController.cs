using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    public class GameplayUIController : MonoBehaviour
    {
        [Header("Play Button")]
        [SerializeField]
        private Image playButton;
        [SerializeField]
        private Sprite playSprite;
        [SerializeField]
        private Sprite stopSprite;


        public void DestroyAllExplosives()
        {
            EventManager.onDestroyAllExplosives.Invoke();
        }

        public void ChangeExplosive(Explosive explosive)
        {
            EventManager.onChangeExplosive.Invoke(explosive);
        }

        public void TogglePlay()
        {
            EventManager.onTogglePlay.Invoke();

            playButton.sprite = GameManager.Instance.CurrentState == GameManager.GameState.Play ? stopSprite : playSprite;
        }
    }
}