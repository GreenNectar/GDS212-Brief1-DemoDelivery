using DemoDelivery.Input;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Stuff")]
        [SerializeField]
        private PlacementController placementController;

        [Header("Scenes")]
        [SerializeField]
        private string UIOverlaySceneName;
        [SerializeField]
        private string UILevelFinishSceneName;

        [Header("Level Values")]
        [SerializeField]
        private int maximumExplosives = 3;

        // Other stuff bb
        public static LevelManager current { get; private set; }
        private List<Explosive> explosives = new List<Explosive>();
        private bool canExplodeBombs;
        public bool CanCreateExplosive => explosives.Count < maximumExplosives;
        public int RemainingExplosives => maximumExplosives - explosives.Count;
        public int ExplosivesUsed => explosives.Count;
        public int MaximumExplosives => maximumExplosives;

        List<Vector3[]> previousBombPositions = new List<Vector3[]>();

        private void OnEnable()
        {
            EventManager.onDestroyAllExplosives.AddListener(DeleteAllExplosives);
            EventManager.onTogglePlay.AddListener(ResetGame);
            EventManager.onPlayFinish.AddListener(FinishGame);
            //EventManager.onPlaceExplosive.AddListener(AddToUndo);
            //EventManager.onUndo.AddListener(Undo);
            //EventManager.onDestroyAllExplosives.AddListener(Undo);

            InputManager.Instance.onStartTouch.AddListener(ExplodeNextBomb);
        }

        private void OnDisable()
        {
            EventManager.onDestroyAllExplosives.RemoveListener(DeleteAllExplosives);
            EventManager.onTogglePlay.RemoveListener(ResetGame);
            EventManager.onPlayFinish.RemoveListener(FinishGame);
            //EventManager.onPlaceExplosive.RemoveListener(AddToUndo);
            //EventManager.onUndo.RemoveListener(Undo);
            //EventManager.onDestroyAllExplosives.RemoveListener(Undo);

            InputManager.Instance.onStartTouch.RemoveListener(ExplodeNextBomb);
        }

        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            ////if (SceneManager.GetSceneByName(UISceneName) == null)
            ////{
                SceneManager.LoadScene(UIOverlaySceneName, LoadSceneMode.Additive);
            ////}

            GameManager.Instance.SetToSetup();

            SetupRigidbodies();
        }

        private void ResetGame()
        {
            ResetRigidbodies();
            ResetExplosives();
            StartCoroutine(SafeTime());
        }

        /// <summary>
        /// This is to stop the explosive going off when we press play
        /// </summary>
        /// <returns></returns>
        private IEnumerator SafeTime()
        {
            canExplodeBombs = false;
            yield return new WaitForFixedUpdate();
            yield return new WaitForEndOfFrame();
            canExplodeBombs = true;
        }

        private void FinishGame()
        {
            SceneManager.LoadScene(UILevelFinishSceneName, LoadSceneMode.Additive);
        }

        #region Consecutive Explosions / Gameplay

        int currentExplosive = 0;
        private void ExplodeNextBomb(Vector2 position)
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Play && canExplodeBombs)
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

            EventManager.onExplosivesAddedorRemoved.Invoke();
        }

        public void RemoveExplosive(Explosive explosive)
        {
            explosives.Remove(explosive);
            SetAllExplosiveNumbers();

            EventManager.onExplosivesAddedorRemoved.Invoke();
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

        #region Undo

        bool isFirstUndo = false;
        private void AddToUndo()
        {
            Vector3[] positions = explosives.Select(e => e.transform.position).ToArray();

            if (!previousBombPositions.Contains(positions))
            {
                isFirstUndo = true;

                // Add bombs and limit the amount stored
                previousBombPositions.Add(positions);

                if (previousBombPositions.Count > 5)
                {
                    previousBombPositions.RemoveAt(0);
                }

                EventManager.onUndoUpdated.Invoke();
            }
        }

        private void Undo()
        {
            if (!CanUndo) return;
            if (!isFirstUndo && previousBombPositions.Count == 0) return;

            for (int i = 0; i < explosives.Count; i++)
            {
                Destroy(explosives[i].gameObject);
            }

            explosives.Clear();

            if (previousBombPositions.Count > 1)
            {
                if (isFirstUndo)
                {
                    isFirstUndo = false;
                    previousBombPositions.Remove(previousBombPositions.Last());
                }

                // 'Pop' from list
                Vector3[] previousPositions = previousBombPositions.Last();
                previousBombPositions.Remove(previousPositions);

                // Add all the ones from the previous back
                for (int i = 0; i < previousPositions.Length; i++)
                {
                    AddExplosive(Instantiate(placementController.currentSelectedExplosive, previousPositions[i], Quaternion.identity));
                }
            }

            EventManager.onUndoUpdated.Invoke();
            EventManager.onExplosivesAddedorRemoved.Invoke();
        }

        public bool CanUndo
        {
            get
            {
                return previousBombPositions.Count > 0;
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

                rigidBodies[i].velocity = Vector2.zero;
                rigidBodies[i].angularVelocity = 0f;
            }
        }

        #endregion
    }
}