using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Code {
    public class EnemyManager{
        private readonly Object _normalEnemy;
        private readonly Object _strongEnemy;
        private readonly Object _fastEnemy;
        private readonly Transform _holder;
        private readonly NavMeshAgent _agent;

        private int _checkCanWalkNext;
        private GameObject _selectedGo;

        //public static enemyManager eManager;
        private const float SpawnTime = 3f;
        private const int MaxEnemyCount = 8;
        private int waveNum = 1;
        private NavMeshPath path = new NavMeshPath();

        Dictionary<int, int> NormalInWaves = new Dictionary<int, int>(){
            {1,3},
            {2,5},
            {3,7}
        };
        Dictionary<int, int> StrongInWaves = new Dictionary<int, int>(){
            {1,0},
            {2,1},
            {3,2}
        };
        Dictionary<int, float> timeOut = new Dictionary<int, float>(){
            {0,5f},
            {1,10f},
            {2,20f},
            {3,30f}
        };

        private int NormalEnemiesCount = 3;
        private int StrongEnemiesCount = 0;
        private float _lastspawn = Time.time - SpawnTime;
        private float _waveover = 5f;
        private float timeLeft = 0f;
        private bool inWave = false;
        private bool IsGameOver = false;
        private int _waveTotalNum = 3;

        public static float NormalEnemySpeed = 0.5f;
        public static float StrongEnemySpeed = 0.3f;
        public static float FastEnemySpeed = 1.0f;
        public static float FastEnemyHealthpoints = 30f;
        public static float NormalEnemyHealthpoints = 600f;
        public static float StrongEnemyHealthpoints = 100f;

        private int normal;
        private int strong;
        private float waveOverTime = 0f;

        public EnemyManager(Transform holder, NavMeshAgent agent)
        {
        	_normalEnemy = Resources.Load("Normal_Enemy");
            _strongEnemy = Resources.Load("Strong_Enemy");
            _fastEnemy = Resources.Load("Fast_Enemy");
            _holder = holder;
            _agent = agent;
        }

        public void Update() {
            if (_checkCanWalkNext == 1) {
                _checkCanWalkNext = 2;
            }else if (_checkCanWalkNext == 2) {
                CheckCanWalk();
            }
            
            if (waveNum > _waveTotalNum)
            {
                IsGameOver = true;
                return;
            }
            if (Time.time - waveOverTime <= _waveover)
            {
                timeLeft = timeOut[waveNum-1] - (Time.time - waveOverTime); 
                return;
            }
            if (!inWave)
            {
                normal = NormalInWaves[waveNum];
                //strong = StrongInWaves[waveNum];
                inWave = true;
                timeLeft = 0;
            }

            if ((Time.time - _lastspawn) < SpawnTime) return;

            if (normal > 0)
            {
                SpawnNormal();
                normal--;
                _lastspawn = Time.time;
            }
            /*if (strong > 0)
            {
                SpawnStrong();
                normal--;
                _lastspawn = Time.time;
            }*/
            //if(normal == 0 && strong == 0)
            if (normal == 0 && Object.FindObjectsOfType<NormalEnemy>().Length == 0)
            {
                waveOverTime = Time.time;
                _waveover = timeOut[waveNum];
                waveNum++;
                if(waveNum <= _waveTotalNum)
                    NormalEnemiesCount = NormalInWaves[waveNum];
                inWave = false;
            }
//            bool result = Canwalk(new Vector3(2.0f, 0.3f, 2.0f), new Vector3(-2.0f, 0.3f, -2.0f));
            //Debug.Log(result);
        }

        private void SpawnNormal() {
            if (_holder == null) {
                //Debug.Log("true");
            }
            if (_holder.childCount >= MaxEnemyCount) {
                return;
            }

            Vector3 pos1 = new Vector3(2f, 0.2f, 2f);
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
            ForceSpawnNormal(pos1, rotation, NormalEnemySpeed, "NormalEnemy", NormalEnemyHealthpoints);
        }

        private void SpawnStrong()
        {
            if (_holder == null)
            {
                Debug.Log("true");
            }
            if (_holder.childCount >= MaxEnemyCount)
            {
                return;
            }
            Vector3 pos2 = new Vector3(1f, 0.3f, 2f);
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
            ForceSpawnStrong(pos2, rotation, StrongEnemySpeed, "StrongEnemy", StrongEnemyHealthpoints);
        }

        public int GetWaveNum()
        {
            return waveNum;
        }

        public int GetNormalNum()
        {
            return NormalEnemiesCount;
        }

        public int GetStrongNum()
        {
            return StrongEnemiesCount;
        }

        public void ForceSpawnNormal(Vector3 pos, Quaternion rotation, float speed, string type, float healthpoints) {
            NormalEnemy enemy;
            GameObject GameObj;
            GameObj = (GameObject) Object.Instantiate(_normalEnemy, pos, rotation, _holder);
            enemy = GameObj.GetComponent<NormalEnemy>();
            enemy.Initialize(speed, type, healthpoints);
        }

        public void ForceSpawnStrong(Vector3 pos, Quaternion rotation, float speed, string type, float healthpoints)
        {
            StrongEnemy enemy;
            GameObject GameObj;
            GameObj = (GameObject)Object.Instantiate(_strongEnemy, pos, rotation, _holder);
            enemy = GameObj.GetComponent<StrongEnemy>();
            enemy.Initialize(speed, type, healthpoints);
        }

        public void AskCanWalk(GameObject go) {
            _selectedGo = go;
            _selectedGo.GetComponent<NavMeshObstacle>().enabled = true;
            _checkCanWalkNext = 1;
        }

        private void CheckCanWalk() {
            _checkCanWalkNext = 0;

            bool pathFound = NavMesh.CalculatePath(
                Game.Ctx.CellManager.SpawnPos,
                Game.Ctx.CellManager.BasePos,
                NavMesh.AllAreas,
                path);
            
            GameObjectUtility.SetNavMeshArea(_selectedGo, NavMesh.GetAreaFromName("Walkable"));
            Game.Ctx.UI.OnCanWalkResult(path.status == NavMeshPathStatus.PathComplete);
            _selectedGo.GetComponent<NavMeshObstacle>().enabled = false;
        }
        
        

        public bool IsOver()
        {
            return IsGameOver;
        }

        public float GetTimeLeft()
        {
            return timeLeft;
        }

    }
}