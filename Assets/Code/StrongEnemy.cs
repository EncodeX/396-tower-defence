using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class StrongEnemy : MonoBehaviour
    {
        private float _healthpoints;
        private string _type;
        private static int _value = 40;

        public void Initialize(float speed, string type, float healthpoints)
        {
            var rb = GetComponent<Rigidbody>();
            //rb.velocity = speed;
            _healthpoints = healthpoints;
            _type = type;
        }

        void Update()
        {
            var rb = GetComponent<Rigidbody>();
            this.transform.position = this.transform.position + rb.velocity * Time.deltaTime;
        }

        internal void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Base")
            {
                //other.gameObject.hitBase(healthpoints);
                Game.Ctx.BaseHealthBar.PerformDamage(_healthpoints / 10f);
                _healthpoints = 0;
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public void PerformDamage(float points)
        {
            _healthpoints -= points;
            if (_healthpoints < 0.01f)
            {
                Game.Ctx.AddMoney(_value);
                Die();
            }
        }
    }
}
