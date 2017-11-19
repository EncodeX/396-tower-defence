﻿using UnityEngine;

namespace Code {
    public class Bullet : MonoBehaviour {
        public const float Lifetime = 7.5f; // bullets last this long
        private float _deathtime;

        public void Initialize(Vector3 velocity, float deathtime) {
            GetComponent<Rigidbody>().velocity = velocity;
            _deathtime = deathtime;
        }

        private void Update() {
            if (Time.time > _deathtime) {
                Die();
            }
        }

        private void OnCollisionEnter(Collision other) {
            switch (other.gameObject.name) {
                case "Normal_Enemy(Clone)":
                    other.gameObject.GetComponent<NormalEnemy>().PerformDamage(12.5f);
                    break;
            }
            Die();
        }

        private void Die() {
            Destroy(gameObject);
        }
    }
}