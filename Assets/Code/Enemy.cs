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
	}
}