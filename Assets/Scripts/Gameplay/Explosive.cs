using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Created by Jarrad

namespace DemoDelivery.Gameplay
{
    public class Explosive : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField]
        private float radius = 4f;
        [SerializeField]
        private float power = 15f;
        [SerializeField]
        private float torque = 5f;

        [Header("Text")]
        [SerializeField]
        private TextMeshPro bombNumber;

        [Header("Images")]
        [SerializeField]
        private SpriteRenderer bomb;
        [SerializeField]
        private SpriteRenderer explosionRadius;

        [Header("Particle Effects")]
        [SerializeField]
        private ParticleSystem explosionEffects;

        public void SetBombNumber(int bombNumber)
        {
            this.bombNumber.text = bombNumber.ToString();
        }

        public void Explode()
        {
            bombNumber.enabled = false;
            bomb.enabled = false;

            explosionEffects.gameObject.SetActive(true);
            explosionEffects.Play();

            Explosion.Explode(transform.position, radius, power, torque);
        }

        public void Refresh()
        {
            bombNumber.enabled = true;
            bomb.enabled = true;

            explosionEffects.gameObject.SetActive(false);
        }

        public void ShowRadius(bool show)
        {
            explosionRadius.enabled = show;
        }
    }
}