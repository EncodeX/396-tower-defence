using System.Collections.Generic;
using UnityEngine;

namespace Code
{
	public class TowerTypeC : Tower
	{
		private static float _cooldown = .6f;
		private float _cooldownTime;

		private float _lightningDuration = .1f;
		private float _lightningTime;

		private Enemy _targetEnemy;
		private Transform _head;

		public Material Mat;
		
		public GameObject Initialize(int row, int col)
		{
			Cost = 100;
			Row = row;
			Col = col;
			return gameObject;
		}

		private void Start() {
			foreach (Transform t in transform) {
				if (t.gameObject.name == "TowerHead") {
					_head = t;
				}
			}
		}

		protected override void Attack(Enemy[] enemies) {
			Enemy closestEnemy = null;
			foreach (Enemy enemy in enemies) {
				if (closestEnemy == null) {
					closestEnemy = enemy;
				}
				if (Vector3.Distance(transform.position, enemy.transform.position) <=
				    Vector3.Distance(transform.position, closestEnemy.transform.position)) {
					closestEnemy = enemy;
				}
			}
			if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.transform.position) < 1.0f) {
				if(CanShoot)
					Shoot(closestEnemy);
			}
		}

		private void Shoot(Enemy enemy) {
			var time = Time.time;
			if (time > _cooldownTime) {
				_cooldownTime = Time.time + _cooldown;
				enemy.Shocked();
				_targetEnemy = enemy;
				_lightningTime = Time.time + _lightningDuration;
			}
		}

		private void OnGUI() {
			if (_targetEnemy != null && Time.time < _lightningTime) {
				Vector3 towerPos = Game.Ctx.Camera.WorldToScreenPoint(_head.position);
				Vector3 enemyPos = Game.Ctx.Camera.WorldToScreenPoint(_targetEnemy.transform.position);
				DrawLine(new Vector2(towerPos.x, towerPos.y), new Vector2(enemyPos.x, enemyPos.y));
			}
		}

		void DrawLine(Vector3 startVertex, Vector3 endVertex)
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
	}
}