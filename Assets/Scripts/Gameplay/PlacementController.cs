using DemoDelivery.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    public class PlacementController : MonoBehaviour
    {
        public Explosive currentSelectedExplosive;
        private Explosive placingExplosive;

        private Material initialMaterial;
        public Material incorrectPlacementMaterial;

        private Vector2 selectionOffset = Vector2.zero;
        private InputManager inputManager;


        private void OnEnable()
        {
            inputManager.onStartTouch.AddListener(StartPlacement);
            inputManager.onEndTouch.AddListener(EndPlacement);

            //EventManager.onChangeExplosive.AddListener(ChangeSelectedExplosive);
            EventManager.onDestroyPlacingExplosive.AddListener(StopPlacement);
        }

        private void OnDisable()
        {
            inputManager.onStartTouch.RemoveListener(StartPlacement);
            inputManager.onEndTouch.RemoveListener(EndPlacement);

            //EventManager.onChangeExplosive.RemoveListener(ChangeSelectedExplosive);
            EventManager.onDestroyPlacingExplosive.AddListener(StopPlacement);
        }

        private void Awake()
        {
            inputManager = InputManager.Instance;
        }

        private void Start()
        {
            if (currentSelectedExplosive == null)
            {
                throw new System.Exception("Please add an explosive prefab into the 'currentExplosive' variable");
            }
        }

        void Update()
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Setup)
            {
                // Handles the moving of the explosive, and the colour overlay
                if (placingExplosive != null)
                {
                    placingExplosive.transform.position = inputManager.GetWorldPosition() + selectionOffset;
                    
                    if (CanPlace())
                    {
                        placingExplosive.GetComponent<SpriteRenderer>().material = initialMaterial;
                        placingExplosive.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        placingExplosive.GetComponent<SpriteRenderer>().material = incorrectPlacementMaterial;
                        placingExplosive.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }
            }
            // If we are not longer in the setup phase, destroy the placing explosive
            else
            {
                if (placingExplosive != null)
                {
                    StopPlacement();
                }
            }
        }

        /// <summary>
        /// Creates, or grabs, an explosive on the position
        /// </summary>
        /// <param name="position">Where to create/grab in world position</param>
        private void StartPlacement(Vector2 position)
        {
            if (GameManager.Instance.CurrentState != GameManager.GameState.Setup) return;

            // If we are still placing an explosive, we don't want to do anything below
            if (placingExplosive != null) return;

            // If we are over a UI element, don't do anything below
            if (IsPointerOverUIObject()) return;

            // Get all objects that are underneath our cursor/pointer position
            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetScreenPosition());
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, float.PositiveInfinity, LayerMask.GetMask("Bomb"));

            // If we hit an explosive, we want to set that as our object we are placing
            if (hit)
            {
                GameObject pressedObject = hit.collider.gameObject;
                if (pressedObject.GetComponent<Explosive>())
                {
                    placingExplosive = pressedObject.GetComponent<Explosive>();
                }
            }

            // Otherwise, we want to create a bomb
            if (placingExplosive == null && LevelManager.current.CanCreateExplosive)
            {
                placingExplosive = Instantiate(currentSelectedExplosive, position, Quaternion.identity);
                LevelManager.current.AddExplosive(placingExplosive);
            }

            if (placingExplosive != null)
            {
                placingExplosive.ShowRadius(true);

                // Store the explosives material, this is so we can switch between the overlay and the original
                initialMaterial = placingExplosive.GetComponent<SpriteRenderer>().material;

                // Allows us to grab the object at any point without it snapping to the pointer position
                selectionOffset = (Vector2)placingExplosive.transform.position - position;
            }
        }

        /// <summary>
        /// If the explosive is placeable it will place it, otherwise it will destroy it
        /// </summary>
        /// <param name="position"></param>
        private void EndPlacement(Vector2 position)
        {
            if (placingExplosive)
            {
                placingExplosive.ShowRadius(false);

                if (CanPlace())
                {
                    placingExplosive.transform.position = position + selectionOffset;
                    EventManager.onPlaceExplosive.Invoke();
                }
                else
                {
                    LevelManager.current.RemoveExplosive(placingExplosive);
                    Destroy(placingExplosive.gameObject);
                }
                placingExplosive = null;
            }
        }

        /// <summary>
        /// Destroys the placing explosive
        /// </summary>
        private void StopPlacement()
        {
            LevelManager.current.RemoveExplosive(placingExplosive);
            Destroy(placingExplosive.gameObject);
            placingExplosive = null;
            //EventManager.onStopPlacing.Invoke();
        }

        //public void ChangeSelectedExplosive(Explosive explosive)
        //{
        //    currentSelectedExplosive = explosive;
        //}

        /// <summary>
        /// Determines whether or not the explosive is over a placeable object and not on a UI element
        /// </summary>
        public bool CanPlace()
        {
            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetScreenPosition());
            return
                Physics2D.GetRayIntersection(ray, float.PositiveInfinity, LayerMask.GetMask("Map")) &&
                !IsPointerOverUIObject();
        }

        // Shh https://answers.unity.com/questions/967170/detect-if-pointer-is-over-any-ui-element.html
        public bool IsPointerOverUIObject()
        {
            if (EventSystem.current)
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                eventDataCurrentPosition.position = inputManager.GetScreenPosition();
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                return results.Count > 0;
            }
            else
            {
                return false;
            }
        }
    }
}