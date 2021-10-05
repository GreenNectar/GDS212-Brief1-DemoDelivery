using DemoDelivery.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Scenes")]
        [SerializeField]
        private string UISceneName;

        public static LevelManager current { get; private set; }


        private List<Explosive> explosives = new List<Explosive>();


        private void OnEnable()
        {
            EventManager.onDestroyAllExplosives.AddListener(DeleteAllExplosives);
            EventManager.onTogglePlay.AddListener(ResetGame);

            InputManager.Instance.onStartTouch.AddListener(ExplodeNextBomb);
        }

        private void OnDisable()
        {
            EventManager.onDestroyAllExplosives.RemoveListener(DeleteAllExplosives);
            EventManager.onTogglePlay.RemoveListener(ResetGame);

            InputManager.Instance.onStartTouch.RemoveListener(ExplodeNextBomb);
        }

        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            if (SceneManager.GetSceneByName(UISceneName) == null)
            {
                SceneManager.LoadScene(UISceneName, LoadSceneMode.Additive);
            }

            SetupRigidbodies();
        }

        private void ResetGame()
        {
            ResetRigidbodies();
            ResetExplosives();
        }

        #region Consecutive Explosions / Gameplay

        int currentExplosive = 0;
        private void ExplodeNextBomb(Vector2 position)
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Play)
            {
                if (currentExplosive < explosives.Count)
                {
                    explosives[currentExplosive].Explode();
                }

                currentExplosive++;
            }
        }

        private void ResetExplosives()
        {
            currentExplosive = 0;

            foreach (var explosive in explosives)
            {
                explosive.Refresh();
            }
        }

        #endregion

        #region Explosives

        public void AddExplosive(Explosive explosive)
        {
            explosive.SetBombNumber(explosives.Count + 1);
            explosives.Add(explosive);
        }

        public void RemoveExplosive(Explosive explosive)
        {
            explosives.Remove(explosive);
            SetAllExplosiveNumbers();
        }

        private void SetAllExplosiveNumbers()
        {
            for (int i = 0; i < explosives.Count; i++)
            {
                explosives[i].SetBombNumber(i + 1);
            }
        }

        public void DeleteAllExplosives()
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Setup)
            {
                for (int i = 0; i < explosives.Count; i++)
                {
                    Destroy(explosives[i].gameObject);
                }
                explosives.Clear();
            }
        }

        #endregion

        #region Rigidbody Handling

        private Rigidbody2D[] rigidBodies;
        private Vector3[] rigidBodyStartingPositions;
        private Quaternion[] rigidBodyStartingRotations;

        private void SetupRigidbodies()
        {
            rigidBodies = FindObjectsOfType<Rigidbody2D>().Where(r => r.bodyType == RigidbodyType2D.Dynamic).ToArray();
            rigidBodyStartingPositions = rigidBodies.Select(r => r.transform.position).ToArray();
            rigidBodyStartingRotations = rigidBodies.Select(r => r.transform.rotation).ToArray();
        }

        private void ResetRigidbodies()
        {
            for (int i = 0; i < rigidBodies.Length; i++)
            {
                rigidBodies[i].transform.position = rigidBodyStartingPositions[i];
                rigidBodies[i].transform.rotation = rigidBodyStartingRotations[i];
            }
        }

        #endregion
    }
}