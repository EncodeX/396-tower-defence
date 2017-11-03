using UnityEngine;

namespace Code {
    public class BulletManager {
        private readonly Transform _holder;
        private readonly Object _bullet;
        
        public BulletManager(Transform holder) {
            _holder = holder;
            _bullet = Resources.Load("Bullet");
        }
        
        public void ForceSpawn (Vector3 pos, Quaternion rotation, Vector3 velocity, float deathtime) {
            ((GameObject)Object.Instantiate(_bullet, pos, rotation, _holder))
                .GetComponent<Bullet>().Initialize(velocity, deathtime);
        }
    }
}