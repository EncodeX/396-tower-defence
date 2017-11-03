using UnityEngine;

namespace Code {
    public class Game : MonoBehaviour {
        public static Game Ctx;

        public BaseHealthBar BaseHealthBar;
        public EnemyManager EnemyManager;
        public BulletManager BulletManager;

        private void Start() {
            Ctx = this;
            BaseHealthBar = GameObject.Find("BaseHealthBar").GetComponent<BaseHealthBar>();
            EnemyManager = new EnemyManager(GameObject.Find("Spawner").transform);
            BulletManager = new BulletManager(GameObject.Find("Bullets").transform);
        }

        private void Update() {
            EnemyManager.Update();
        }
    }
}