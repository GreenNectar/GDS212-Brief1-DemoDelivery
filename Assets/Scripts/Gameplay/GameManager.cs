using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Jarrad

namespace DemoDelivery
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameState { Setup, Play, End }
        public GameState CurrentState { get; private set; } = GameState.Setup;

        private void OnEnable()
        {
            EventManager.onTogglePlay.AddListener(TogglePlayState);
            EventManager.onPlayFinish.AddListener(FinishLevel);
        }

        private void OnDisable()
        {
            EventManager.onTogglePlay.RemoveListener(TogglePlayState);
            EventManager.onPlayFinish.RemoveListener(FinishLevel);
        }

        private void Start()
        {
            SetTimeScale();
        }

        public void TogglePlayState()
        {
            if (CurrentState != GameState.End)
            {
                CurrentState = CurrentState == GameState.Play ? GameState.Setup : GameState.Play;
            }
            //CurrentState = isPlay ? GameState.Play : GameState.Setup;

            SetTimeScale();
        }

        private void SetTimeScale()
        {
            if (CurrentState == GameState.Play)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
        }

        private void FinishLevel()
        {
            CurrentState = GameState.End;

            SetTimeScale();
        }
    }
}