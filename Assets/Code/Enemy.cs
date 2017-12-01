using UnityEngine;
using UnityEngine.AI;

namespace Code
{
	public class Enemy : MonoBehaviour
	{
		private float _healthpoints;
		private float _fullHealthPoints;
		private static int _value = 20;
		public Vector3 Goal = new Vector3(-2f, 0f, -2f);
		private NavMeshAgent _agent;
        private float _originalSpeed;
		private bool _shocked;
		private bool _frozen;

		public Material Mat;

		private Enemy _targetEnemy;
		private float _lightningDuration = .1f;
		private float _lightningTime;

        public void Initialize(float speed, float healthpoints, int enemyvalue)
		{
			var rb = GetComponent<Rigidbody>();
			//rb.velocity = speed;
			_healthpoints = healthpoints;
			_fullHealthPoints = healthpoints;
			_agent = GetComponent<NavMeshAgent>();
            _originalSpeed = speed;
            _agent.speed = speed;
			_agent.SetDestination(Goal);
            _value = enemyvalue;
		}

        //void Start()
        //{
        //agent = GetComponent<NavMeshAgent>();
        //agent.SetDestination(goal);
        //}

        private void Update()
        {
//            PerformDamage(Freezing() + Shocking());
	        Freezing();
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

        public void Freezing()
        {
            var myPositionX = Mathf.RoundToInt(transform.position.x);
            var myPositionZ = Mathf.RoundToInt(transform.position.z);
            float speedRatio = 1.0f;
	        _frozen = false;
			foreach (TowerTypeB t in FindObjectsOfType<TowerTypeB>())
			{
				var towerPosition = t.transform.position;
				if (Mathf.Abs(myPositionX - towerPosition.x) <= 1.0 && Mathf.Abs(myPositionZ - towerPosition.z) <= 1.0)
				{
					speedRatio = speedRatio * 0.6f;
					_frozen = true;
				}
			}
            _agent.speed = _originalSpeed * speedRatio;
        }

		public void Shocked() {
			foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
				enemy._shocked = false;
			}
			
			Shocking(3.0f, 0);
		}

        public void Shocking(float damage, int count) {
	        if (_shocked || count > 1) return;
	        
	        PerformDamage(damage);
	        _shocked = true;

	        Enemy closestEnemy = null;
	        foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
		        if (enemy._shocked) continue;
		        
		        if (closestEnemy == null) {
			        closestEnemy = enemy;
		        }
		        if (Vector3.Distance(transform.position, enemy.transform.position) <=
		            Vector3.Distance(transform.position, closestEnemy.transform.position)) {
			        closestEnemy = enemy;
		        }
	        }
	        if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) < 1f) {
		        closestEnemy.Shocking(damage * .6f, count + 1);
		        _targetEnemy = closestEnemy;
		        _lightningTime = Time.time + _lightningDuration;
	        }
        }

		private void OnGUI() {
			Vector3 fromPos = Game.Ctx.Camera.WorldToScreenPoint(transform.position);
			if (_targetEnemy != null && Time.time < _lightningTime) {
				Vector3 toPos = Game.Ctx.Camera.WorldToScreenPoint(_targetEnemy.transform.position);
				DrawLine(new Vector2(fromPos.x, fromPos.y), new Vector2(toPos.x, toPos.y));
			}
			
			DrawHealthPoint(fromPos);

			if (_frozen) {
				DrawArrow(fromPos);
			}
		}

		private void DrawLine(Vector3 startVertex, Vector3 endVertex)
		{
			if (!Mat)
			{
				Debug.LogError("Please Assign a material on the inspector");
				return;
			}
			GL.PushMatrix();
			Mat.SetPass(0);
			GL.LoadPixelMatrix();
			GL.Begin(GL.LINES);
			GL.Color(Color.white);
			GL.Vertex(startVertex);
			GL.Vertex(endVertex);
			GL.End();
			GL.PopMatrix();
		}

		private void DrawHealthPoint(Vector2 center) {
			if (!Mat)
			{
				Debug.LogError("Please Assign a material on the inspector");
				return;
			}
			Vector2 barCenter = new Vector2(center.x, center.y + 30f);
			
			GL.PushMatrix();
			Mat.SetPass(0);
			GL.LoadPixelMatrix();
			GL.Begin(GL.QUADS);
			GL.Color(Color.white);
			GL.Vertex(barCenter + new Vector2(-24f, -3f));
			GL.Vertex(barCenter + new Vector2(24f, -3f));
			GL.Vertex(barCenter + new Vector2(24f, 3f));
			GL.Vertex(barCenter + new Vector2(-24f, 3f));
			GL.End();
			GL.Begin(GL.QUADS);
			GL.Color(Color.red);
			GL.Vertex(barCenter + new Vector2(-23f, -2f));
			GL.Vertex(barCenter + new Vector2(-23f + 46f * _healthpoints / _fullHealthPoints, -2f));
			GL.Vertex(barCenter + new Vector2(-23f + 46f * _healthpoints / _fullHealthPoints, 2f));
			GL.Vertex(barCenter + new Vector2(-23f, 2f));
			GL.End();
			GL.PopMatrix();
		}

		private void DrawArrow(Vector2 center) {
			Vector2 arrowStart = new Vector2(center.x + 35f, center.y + 29f);
			
			GL.PushMatrix();
			Mat.SetPass(0);
			GL.LoadPixelMatrix();
			GL.Begin(GL.QUADS);
			GL.Color(Color.blue);
			GL.Vertex(arrowStart + new Vector2(2f, 7f));
			GL.Vertex(arrowStart + new Vector2(-2f, 7f));
			GL.Vertex(arrowStart + new Vector2(-2f, 0f));
			GL.Vertex(arrowStart + new Vector2(2f, 0f));
			GL.End();
			GL.Begin(GL.TRIANGLES);
			GL.Color(Color.blue);
			GL.Vertex(arrowStart + new Vector2(5f, 0f));
			GL.Vertex(arrowStart + new Vector2(-5f, 0f));
			GL.Vertex(arrowStart + new Vector2(0f, -5f));
			GL.End();
			GL.PopMatrix();
		}
    }


}