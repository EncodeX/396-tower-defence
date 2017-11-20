using UnityEngine;
using UnityEngine.AI;

namespace Code {
    public class NormalEnemy : MonoBehaviour {
        private float _healthpoints;
        private string _type;
        private static int _value = 20;
        public Vector3 goal = new Vector3(-2f,0f,-2f);
        private NavMeshAgent _agent;

        public void Initialize(float speed, string type, float healthpoints) {
            var rb = GetComponent<Rigidbody>();
            //rb.velocity = speed;
            _healthpoints = healthpoints;
            _type = type;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            _agent.SetDestination(goal);
        }

        //void Start()
        //{
            //agent = GetComponent<NavMeshAgent>();
            //agent.SetDestination(goal);
        //}

        // Update is called once per frame
        void Update() {
            var rb = GetComponent<Rigidbody>();
            //bool result = Game.Ctx.EnemyManager.Canwalk(new Vector3(2.0f, 1.5f, 2.0f), new Vector3(-2.0f, 1.5f, -2.0f));
            //Debug.Log(result);
            //this.transform.position = this.transform.position + rb.velocity * Time.deltaTime;
        }

        internal void OnCollisionEnter(Collision other) {
            if (other.gameObject.name == "Base") {
                //other.gameObject.hitBase(healthpoints);
                Game.Ctx.BaseHealthBar.PerformDamage(_healthpoints / 10f);
                _healthpoints = 0;
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }

        public void PerformDamage(float points) {
            _healthpoints -= points;
            if (_healthpoints < 0.01f) {
                Game.Ctx.AddMoney(_value);
                Die();
            }
        }
    }
}