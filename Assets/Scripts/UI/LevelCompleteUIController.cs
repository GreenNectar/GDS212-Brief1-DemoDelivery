using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created by Jarrad

namespace DemoDelivery.UI
{
    public class LevelCompleteUIController : MonoBehaviour
    {
        [SerializeField]
        private string homeScene;

        [SerializeField]
        private Button nextButton;

        [SerializeField]
        private Animator animator;

        private float timeToWait = 0.5f;
        private bool hasPressedButton;

        private void Awake()
        {
            // If the next level doesn't exist, disable the button
            if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount)
            {
                //nextButton.interactable = false;
                nextButton.gameObject.SetActive(false);
            }
        }

        public void NextLevel()
        {
            animator.SetBool("Show", false);
            StartCoroutine(NextLevelSequence());
        }

        private IEnumerator NextLevelSequence()
        {
            yield return new WaitForSecondsRealtime(timeToWait);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Home()
        {
            animator.SetBool("Show", false);
            StartCoroutine(HomeSequence());
        }

        private IEnumerator HomeSequence()
        {
            yield return new WaitForSecondsRealtime(timeToWait);
            Time.timeScale = 1f;
            SceneManager.LoadScene(homeScene);
            GameManager.Instance.SetToMenu();
        }

        public void Restart()
        {
            animator.SetBool("Show", false);
            StartCoroutine(RestartSequence());
        }

        private IEnumerator RestartSequence()
        {
            yield return new WaitForSecondsRealtime(timeToWait);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}