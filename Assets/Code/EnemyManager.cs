using UnityEngine;

namespace Code {
    public class EnemyManager{
        private readonly Object _normalEnemy;
        private readonly Transform _holder;

        //public static enemyManager eManager;
        private const float SpawnTime = 3f;
        private const int MaxEnemyCount = 8;
        
        private float _lastspawn;
        
        public static Vector3 NormalEnemySpeed = new Vector3(-0.2f, 0f, -0.2f);
        public static float NormalEnemyHealthpoints = 100f;

        public EnemyManager(Transform holder)
        {
        	_normalEnemy = Resources.Load("Normal_Enemy");
            _holder = holder;
        }

        public void Update() {
            if ((Time.time - _lastspawn) < SpawnTime) return;
            _lastspawn = Time.time;
            Spawn();
        }

        private void Spawn() {
            if (_holder == null) {
                Debug.Log("true");
            }
            if (_holder.childCount >= MaxEnemyCount) {
                return;
            }

            Vector3 pos = new Vector3(2f, 0.3f, 2f);
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
            //Quaternion rotation = transform.rotation;
            ForceSpawn(pos, rotation, NormalEnemySpeed, "NormalEnemy", NormalEnemyHealthpoints);
        }

        public void ForceSpawn(Vector3 pos, Quaternion rotation, Vector3 speed, string type, float healthpoints) {
            //Debug.Log(pos);
            NormalEnemy enemy;
            GameObject GameObj;
            GameObj = (GameObject) Object.Instantiate(_normalEnemy, pos, rotation, _holder);
            enemy = GameObj.GetComponent<NormalEnemy>();
            enemy.Initialize(speed, type, healthpoints);
        }
    }
}