using DemoDelivery.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoDelivery.Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class Zoom : MonoBehaviour
    {
        [SerializeField]
        private float translationSensitivity = 0.01f;
        [SerializeField]
        private float zoomSensitivity = 0.01f;
        [SerializeField]
        private float maximumZoom = 2f;

        new Camera camera;
        InputManager inputManager;

        // Store some values of the camera
        float initialSize;
        Vector3 initialPosition;
        float initialVerticalExtent;
        float initialHorizontalExtent;


        // When pressed
        Vector3 startingPosition;
        float startingZoom;
        Vector2 firstFinger;
        Vector2 secondFinger;
        Vector2 midPoint;
        float startingTouchDistance;

        int pressed = 0; // So we can keep track if we're touching with two fingers


        private void Awake()
        {
            camera = GetComponent<Camera>();
            initialSize = camera.orthographicSize;
            initialPosition = camera.transform.position;
            initialVerticalExtent = camera.orthographicSize;
            initialHorizontalExtent = initialVerticalExtent * Screen.width / Screen.height;

            inputManager = InputManager.Instance;
        }

        private void OnEnable()
        {
            inputManager.onStartTouch.AddListener(AddPress);
            inputManager.onEndTouch.AddListener(RemovePress);
            inputManager.onStartSecondTouch.AddListener(AddPress);
            inputManager.onEndSecondTouch.AddListener(RemovePress);

            EventManager.onTogglePlay.AddListener(ResetAll);
        }

        private void OnDisable()
        {
            inputManager.onStartTouch.RemoveListener(AddPress);
            inputManager.onEndTouch.RemoveListener(RemovePress);
            inputManager.onStartSecondTouch.RemoveListener(AddPress);
            inputManager.onEndSecondTouch.RemoveListener(RemovePress);

            EventManager.onTogglePlay.RemoveListener(ResetAll);
        }

        private void Update()
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Setup)
            {
                if (pressed == 2) // If two fingers are touching
                {
                    Vector2 currentTouchFirst = inputManager.GetScreenPosition();
                    Vector2 currentTouchSecond = inputManager.GetSecondScreenPosition();

                    // Scale (the best I could get it :/)
                    float distance = Vector2.Distance(currentTouchFirst, currentTouchSecond);
                    float delta = startingTouchDistance - distance;

                    float newSize = startingZoom * (1f + (delta / startingTouchDistance));

                    camera.orthographicSize = Mathf.Clamp(newSize, initialSize / maximumZoom, initialSize);

                    // Translation
                    Vector2 newMidPoint = (currentTouchFirst + currentTouchSecond) / 2f;
                    Vector2 midPointDifference = midPoint - newMidPoint;

                    Vector3 difference = midPointDifference;
                    difference.z = camera.transform.position.z;

                    Vector3 newPosition = startingPosition + (difference * translationSensitivity) * camera.orthographicSize;


                    float vertExtent = camera.orthographicSize;
                    float horzExtent = vertExtent * Screen.width / Screen.height;

                    float vertDiff = (initialVerticalExtent - vertExtent);
                    float horzDiff = (initialHorizontalExtent - horzExtent);

                    newPosition.x = Mathf.Clamp(newPosition.x, initialPosition.x - horzDiff, initialPosition.x + horzDiff);
                    newPosition.y = Mathf.Clamp(newPosition.y, initialPosition.y - vertDiff, initialPosition.y + vertDiff);

                    //float width = Screen.width / 2f;
                    //newPosition.x = Mathf.Clamp(newPosition.x, initialPosition.x - width, initialPosition.x + width);
                    //float height = Screen.height / 2f;
                    //newPosition.x = Mathf.Clamp(newPosition.x, initialPosition.x - width, initialPosition.x + width);





                    camera.transform.position = newPosition;
                }
            }
        }

        private void ResetAll()
        {
            ResetCamera();
        }

        private void ResetCamera()
        {
            //StopCoroutine(ResetCameraSequence());
            StartCoroutine(ResetCameraSequence());
        }

        private IEnumerator ResetCameraSequence()
        {
            float time = 0f;
            Vector3 startingPosition = camera.transform.position;
            float startingSize = camera.orthographicSize;

            while(time < 1f)
            {
                time += Time.unscaledDeltaTime / 0.1f; // It takes a tenth of a second to reach 1f

                camera.transform.position = Vector3.Lerp(startingPosition, initialPosition, time);
                camera.orthographicSize = Mathf.Lerp(startingSize, initialSize, time);

                Debug.Log("Is here");

                yield return null;
            }
        }

        private void AddPress(Vector2 position)
        {
            pressed++;

            if (pressed == 2 && GameManager.Instance.CurrentState == GameManager.GameState.Setup)
            {
                StartPress();
            }
        }

        private void RemovePress(Vector2 position)
        {
            pressed--;
        }

        private void StartPress()
        {
            EventManager.onDestroyPlacingExplosive.Invoke();

            startingZoom = camera.orthographicSize;
            startingPosition = camera.transform.position;

            firstFinger = inputManager.GetScreenPosition();
            secondFinger = inputManager.GetSecondScreenPosition();

            startingTouchDistance = Vector2.Distance(firstFinger, secondFinger);
            midPoint = (firstFinger + secondFinger) / 2f;
        }
    }
}