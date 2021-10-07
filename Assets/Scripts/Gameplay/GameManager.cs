using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Jarrad

namespace DemoDelivery
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameState { Setup, Play, End, Menu }
        public GameState CurrentState { get; private set; } = GameState.Menu;

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

            Application.targetFrameRate = 60;
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
            if (CurrentState == GameState.Play || CurrentState == GameState.Menu)
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

        public void SetToMenu()
        {
            CurrentState = GameState.Menu;
        }

        public void SetToSetup()
        {
            CurrentState = GameState.Setup;
        }
    }
}