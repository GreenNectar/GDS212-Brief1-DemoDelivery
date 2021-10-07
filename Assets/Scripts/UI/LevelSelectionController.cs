using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Created by Jarrad

namespace DemoDelivery.LevelSelect
{
    public class LevelSelectionController : MonoBehaviour
    {
        [SerializeField]
        private Button[] levelButtons;

        private void Awake()
        {
            SetLevels();

            // Just to make sure we create one if we have this active
            var levelUnlockController = LevelUnlockController.Instance;
        }

        public void DeleteData()
        {
            LevelUnlockController.ResetLevels();

            SetLevels();
        }

        private void SetLevels()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i != 0 && !PlayerPrefs.HasKey("Level" + i))
                {
                    levelButtons[i].interactable = false;
                }
            }
        }
    }
}