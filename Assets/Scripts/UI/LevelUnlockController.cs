using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Jarrad

namespace DemoDelivery.LevelSelect
{
    public class LevelUnlockController : Singleton<LevelUnlockController>
    {
        [SerializeField]
        private int startingIndex;

        private void Start()
        {
            SetAsPersistent();
            //ResetLevels();
        }

        private void OnEnable()
        {
            EventManager.onLevelComplete.AddListener(UnlockLevel);
        }

        private void OnDisable()
        {
            EventManager.onLevelComplete.RemoveListener(UnlockLevel);
        }

        private void UnlockLevel(int level)
        {
            if (!PlayerPrefs.HasKey("Level" + (level - startingIndex + 1)))
            {
                PlayerPrefs.SetInt("Level" + (level - startingIndex + 1), 1);
            }

            PlayerPrefs.Save();
        }

        public static void ResetLevels()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (PlayerPrefs.HasKey("Level" + i))
                {
                    PlayerPrefs.DeleteKey("Level" + i);
                }
            }
        }
    }
}