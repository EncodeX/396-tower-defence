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
        private float originalSpeed;
        private int chainNo = 0;

		public void Initialize(float speed, string type, float healthpoints, int enemyvalue)
		{
			var rb = GetComponent<Rigidbody>();
			//rb.velocity = speed;
			_healthpoints = healthpoints;
			_type = type;
			_agent = GetComponent<NavMeshAgent>();
            originalSpeed = speed;
            _agent.speed = speed;
			_agent.SetDestination(goal);
            _value = enemyvalue;
		}

        //void Start()
        //{
        //agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(goal);
        //}

        private void Update()
        {
            PerformDamage(Freezing() + Shocking()); 
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

        public float Freezing()
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
				if (Mathf.Abs(myPositionX - towerPosition.x) <= 1.0 && Mathf.Abs(myPositionZ - towerPosition.z) <= 1.0)
				{
					speedRatio = speedRatio * 0.6f;
					damage += 0.1f;
				}
			}
            _agent.speed = originalSpeed * speedRatio;
            return damage;
        }

        public float Shocking()
		{
			var myPositionX = Mathf.RoundToInt(this.transform.position.x);
			var myPositionY = Mathf.RoundToInt(this.transform.position.y);
			var myPositionZ = Mathf.RoundToInt(this.transform.position.z);
			float damage = 0.0f;
            chainNo = 0;
			foreach (TowerTypeC t in FindObjectsOfType<TowerTypeC>())
			{
                var towerPositionX = Mathf.RoundToInt(t.transform.position.x);
                var towerPositionZ = Mathf.RoundToInt(t.transform.position.z);
                if (Mathf.Abs(myPositionX-towerPositionX) <= 1.0 && Mathf.Abs(myPositionZ - towerPositionZ) <= 1.0)
				{
					damage += 0.1f;
                    chainNo = 1;
                    break;
				}

			}
            if(chainNo == 0){
                foreach (Enemy e in FindObjectsOfType<Enemy>())
				{
					var enemyPosition = e.transform.position;
                    if (Mathf.Abs(myPositionX - enemyPosition.x) <= 1.0 && Mathf.Abs(myPositionZ - enemyPosition.z) <= 1.0 && e.chainNo > 0 && e.chainNo < 3)
					{
                        if(chainNo == 0 || e.chainNo < chainNo)
						    chainNo = e.chainNo;
					}	
				}
            }

            if (chainNo > 0)
                damage = 0.1f; 
            else
                damage = 0.0f;
            return damage;
		}

	}


}