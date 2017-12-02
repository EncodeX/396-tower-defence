using System.Collections.Generic;
using UnityEngine;

namespace Code
{
	public class TowerTypeB : Tower
	{
		private static float _cooldown = .6f;
		private float _cooldownTime;
		
		public GameObject Initialize(int row, int col)
		{
			Cost = 150;
			Row = row;
			Col = col;
			return gameObject;
		}

		protected override void Attack(Enemy[] enemies) {
			List<Enemy> targetEnemies = new List<Enemy>();
			
			foreach (Enemy enemy in enemies) {
				if (Vector3.Distance(transform.position, enemy.transform.position) <= 1.4f) {
					targetEnemies.Add(enemy);
				}
			}
			if (targetEnemies.Count != 0) {
				if(CanShoot)
					Shoot(targetEnemies);
			}
		}

		private void Shoot(List<Enemy> targetEnemies) {
			var time = Time.time;
			if (time > _cooldownTime) {
				_cooldownTime = Time.time + _cooldown;
				foreach (Enemy enemy in targetEnemies) {
					enemy.PerformDamage(2f);
				}
			}
		}
	}
}