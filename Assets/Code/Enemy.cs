using UnityEngine;
using UnityEngine.AI;

namespace Code
{
	public class Enemy : MonoBehaviour
	{
		private float _healthpoints;
		private string _type;
		private static int _value = 20;
		public Vector3 goal = new Vector3(-2f, 0f, -2f);
		private NavMeshAgent _agent;

		public void Initialize(float speed, string type, float healthpoints, int enemyvalue)
		{
			var rb = GetComponent<Rigidbody>();
			//rb.velocity = speed;
			_healthpoints = healthpoints;
			_type = type;
			_agent = GetComponent<NavMeshAgent>();
			_agent.speed = speed;
			_agent.SetDestination(goal);
            _value = enemyvalue;
		}

		//void Start()
		//{
		//agent = GetComponent<NavMeshAgent>();
		//agent.SetDestination(goal);
		//}


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

        public void Freezing()
        {
            var myPositionX = Mathf.RoundToInt(this.transform.position.x);
            var myPositionY = Mathf.RoundToInt(this.transform.position.y);
            var myPositionZ = Mathf.RoundToInt(this.transform.position.z);
            var posPoint = new Vector3(myPositionX, myPositionY, myPositionZ);
            float speedRatio = 1.0f;
            float damage = 0.0f;
            foreach (TowerTypeB t in FindObjectsOfType<TowerTypeB>())
            {
                var towerPosition = t.transform.position;
                var direction = (posPoint - towerPosition);
                var distance = direction.magnitude;

                if(distance <= 1.0f)
                {
                    speedRatio = speedRatio * 0.9f;
                    damage += 1.0f;
                }
            }

            var spd = _agent.speed;
            _agent.speed = spd * speedRatio;
            PerformDamage(damage);
        }
	}
}