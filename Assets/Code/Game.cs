using UnityEngine;

namespace Code {
    public class Game : MonoBehaviour {
        public static Game Ctx;

        public BaseHealthBar BaseHealthBar;
        public EnemyManager EnemyManager;
        public BulletManager BulletManager;
        public UIManager UI;
        public CellManager CellManager;
        public Camera Camera;

        private int _money = 200;

        private void Start() {
            Ctx = this;
            BaseHealthBar = GameObject.Find("BaseHealthBar").GetComponent<BaseHealthBar>();
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            EnemyManager = new EnemyManager(GameObject.Find("Spawner").transform);
            BulletManager = new BulletManager(GameObject.Find("Bullets").transform);
            UI = new UIManager();
            CellManager = new CellManager(GameObject.Find("Towers").transform);
        }

        private void Update() {
            EnemyManager.Update();
        }

        public int GetPlayerMoney() {
            return _money;
        }

        public void AddMoney(int value) {
            _money += value;
        }
    }
}