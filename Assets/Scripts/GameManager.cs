using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoDelivery.Gameplay
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameState { Setup, Play, End }

        public GameState CurrentState { get; private set; } = GameState.Setup;

        [SerializeField]
        private List<Explosive> explosives = new List<Explosive>();

        public void AddExplosive(Explosive explosive)
        {
            explosives.Add(explosive);
        }

        public void RemoveExplosive(Explosive explosive)
        {
            explosives.Remove(explosive);
        }
    }
}