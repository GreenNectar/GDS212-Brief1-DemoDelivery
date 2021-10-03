using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoDelivery.Gameplay
{
    public class Explosive : MonoBehaviour
    {
        [SerializeField]
        private float radius = 4f;
        [SerializeField]
        private float power = 15f;
        [SerializeField]
        private float torque = 5f;

        public void Explode()
        {
            Explosion.Explode(transform.position, radius, power, torque);
        }
    }
}