using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Created by Jarrad

namespace DemoDelivery.UI
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private int startingIndex;

        public void LoadLevel(int levelNumber)
        {
            SceneManager.LoadScene(levelNumber + startingIndex);
        }

        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}