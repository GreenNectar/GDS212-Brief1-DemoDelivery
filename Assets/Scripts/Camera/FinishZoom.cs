using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    /// <summary>
    /// Zooms in on Gazza and the finish line when he reaches the end
    /// </summary>
    public class FinishZoom : MonoBehaviour
    {
        private Transform finishLine;
        private float cameraSize = 2f;
        private float lerpTime = 0.5f;
        private Transform gazza;

        private void Awake()
        {
            finishLine = FindObjectOfType<FinishLine>().transform;
            gazza = GameObject.Find("Gazza").transform;
        }

        private void OnEnable()
        {
            EventManager.onPlayFinish.AddListener(StartZoom);
        }

        private void OnDisable()
        {
            EventManager.onPlayFinish.RemoveListener(StartZoom);
        }

        private void StartZoom()
        {
            StartCoroutine(Zoom());
        }

        private IEnumerator Zoom()
        {
            float time = 0f;
            Camera main = Camera.main;
            float startingSize = main.orthographicSize;
            Vector2 startingPosition = main.transform.position;
            Vector3 finishPosition = (finishLine.position + gazza.position) / 2f;

            while (time < 1f)
            {
                time += Time.unscaledDeltaTime / lerpTime;
                time = Mathf.Clamp(time, 0f, 1f);

                Vector3 newPosition = Vector3.Lerp(startingPosition, finishPosition, time);
                newPosition.z = main.transform.position.z; // Keep the z position the same

                main.transform.position = newPosition;
                main.orthographicSize = Mathf.Lerp(startingSize, cameraSize, time);

                yield return null;
            }
        }
    }
}