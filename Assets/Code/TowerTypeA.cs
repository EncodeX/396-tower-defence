using System.Collections.Generic;
using UnityEngine;

namespace Code {
    public class TowerTypeA : Tower {
        private static float _cooldown = .6f;
        private float _cooldownTime;
        
        private Transform _head;
        private Transform _gun;

        public GameObject Initialize(int row, int col) {
            Cost = 100;
            Row = row;
            Col = col;
            return gameObject;
        }

        // Use this for initialization
        void Start() {
            foreach (Transform t in transform) {
                if (t.gameObject.name == "TowerHead") {
                    _head = t;
                    foreach (Transform tr in t) {
                        if (tr.gameObject.name == "Gun") {
                            _gun = tr;
                        }
                    }
                }
            }
            _cooldownTime = Time.time;
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
            if (closestEnemy != null) {
                Vector3 relativePos = closestEnemy.transform.position - _head.position;
                _head.transform.rotation = Quaternion.LookRotation(relativePos);
                if(CanShoot)
                    Shoot();
            }
        }

        private void Shoot() {
            var time = Time.time;
            if (time > _cooldownTime) {
                _cooldownTime = Time.time + _cooldown;
                Game.Ctx.BulletManager.ForceSpawn(_gun.position + _gun.up * .2f, _gun.rotation, _gun.up * 6f, time + Bullet.Lifetime);
            }
        }
    }
}