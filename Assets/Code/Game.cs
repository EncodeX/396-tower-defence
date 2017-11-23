using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Code {
    public class Game : MonoBehaviour {
        public static Game Ctx;

        public BaseHealthBar BaseHealthBar;
        public EnemyManager EnemyManager;
        public BulletManager BulletManager;
        public UIManager UI;
        public CellManager CellManager;
        public Camera Camera;
//        public Notification Notification;

        public GameObject GameOverPanel;
        public Text GameOverText;

        private int _money = 200;
        private int _wave = 1;
        private bool over = false;

        public int LastTowerRow = 0;
        public int LastTowerCol = 0;
//        public bool towerNotification;

        private void Start() {
            Ctx = this;
            BaseHealthBar = GameObject.Find("BaseHealthBar").GetComponent<BaseHealthBar>();
            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            EnemyManager = new EnemyManager(
                GameObject.Find("Spawner").transform,
                GameObject.Find("Base").GetComponent<NavMeshAgent>());
            BulletManager = new BulletManager(GameObject.Find("Bullets").transform);
            UI = new UIManager();
            CellManager = new CellManager(GameObject.Find("Towers").transform);

            GameOverPanel.SetActive(false);
            
//            towerNotification = false;
        }

        private void Update() {

            if (isOver())
            {
                GameOverPanel.SetActive(true);
                GameOverText.text = "You Win!";
                return;
            }else if (BaseHealthBar.lost())
            {
                GameOverPanel.SetActive(true);
                GameOverText.text = "You Lose!";
                halt();
                return;
            }

            /* halt everything after game is over */

            EnemyManager.Update();
        }

        public int GetPlayerMoney() {
            return _money;
        }

        public void AddMoney(int value) {
            _money += value;
        }

        public int GetWave()
        {
            return EnemyManager.GetWaveNum();
        }

        public int GetNormalCount()
        {
            return EnemyManager.GetNormalNum();
        }

        public int GetStrongCount()
        {
            return EnemyManager.GetStrongNum();
        }

        public int GetFastCount()
        {
            return EnemyManager.GetFastNum();
        }

        //        public bool DisplayNotification()
        //        {
        //            return towerNotification;
        //        }

        public bool isOver()
        {
            return EnemyManager.IsOver();
        }

        public float getTimeLeft()
        {
            return EnemyManager.GetTimeLeft();
        }

        public void halt()
        {
            NormalEnemy[] allNormalEnemy = Object.FindObjectsOfType<NormalEnemy>();
            TowerTypeA[] allTowerA = Object.FindObjectsOfType<TowerTypeA>();
            foreach (NormalEnemy a in allNormalEnemy)
            {
                a.GetComponent<NavMeshAgent>().speed = 0;
            }
            foreach (TowerTypeA t in allTowerA)
            {
                t.canShoot = false;
            }
        }
    }
}