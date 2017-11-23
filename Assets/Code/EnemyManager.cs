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
            {1,5},
            {2,10},
            {3,10},
            {4,10},
            {5,20},
            {6,10},
            {7,15},
            {8,15},
            {9,20},
            {10,20}
        };
        Dictionary<int, int> StrongInWaves = new Dictionary<int, int>(){
            {1,0},
            {2,0},
            {3,0},
            {4,2},
            {5,4},
            {6,4},
            {7,6},
            {8,6},
            {9,8},
            {10,10}
        };
        Dictionary<int, int> FastInWaves = new Dictionary<int, int>(){
            {1,0},
            {2,0},
            {3,5},
            {4,0},
            {5,0},
            {6,10},
            {7,10},
            {8,12},
            {9,12},
            {10,15}
        };
        Dictionary<int, float> timeOut = new Dictionary<int, float>(){
            {0,5f},
            {1,10f},
            {2,10f},
            {3,15f},
            {4,15f},
            {5,20f},
            {6,20f},
            {7,25f},
            {8,25f},
            {9,30f},
            {10,30f},
        };

        private int NormalEnemiesCount = 5;
        private int StrongEnemiesCount = 0;
        private int FastEnemiesCount = 0;
        private float _lastspawn = Time.time - SpawnTime;
        private float _waveover = 5f;
        private float timeLeft = 0f;
        private bool inWave = false;
        private bool IsGameOver = false;
        private int _waveTotalNum = 10;

        public static float NormalEnemySpeed = 0.5f;
        public static float StrongEnemySpeed = 0.3f;
        public static float FastEnemySpeed = 1.0f;
        public static float FastEnemyHealthpoints = 30f;
        public static float NormalEnemyHealthpoints = 50f;
        public static float StrongEnemyHealthpoints = 70f;

        private int normal;
        private int strong;
        private int fast;
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
                strong = StrongInWaves[waveNum];
                fast = FastInWaves[waveNum];
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
            if (strong > 0)
            {
                SpawnStrong();
                strong--;
                _lastspawn = Time.time;
            }
            if (fast > 0)
            {
                SpawnFast();
                fast--;
                _lastspawn = Time.time;
            }
            //if(normal == 0 && strong == 0)
            if (normal == 0 && Object.FindObjectsOfType<Enemy>().Length == 0 && strong == 0 && fast == 0)
            {
                waveOverTime = Time.time;
                _waveover = timeOut[waveNum];
                waveNum++;
                if (waveNum <= _waveTotalNum)
                {
                    NormalEnemiesCount = NormalInWaves[waveNum];
                    StrongEnemiesCount = StrongInWaves[waveNum];
                    FastEnemiesCount = FastInWaves[waveNum];
                }
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
            ForceSpawn(pos1, rotation, NormalEnemySpeed, "NormalEnemy", NormalEnemyHealthpoints);
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
            ForceSpawn(pos2, rotation, StrongEnemySpeed, "StrongEnemy", StrongEnemyHealthpoints);
        }

        private void SpawnFast()
        {
            if (_holder == null)
            {
                Debug.Log("true");
            }
            if (_holder.childCount >= MaxEnemyCount)
            {
                return;
            }
            Vector3 pos3 = new Vector3(2f, 0.3f, 1f);
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
            ForceSpawn(pos3, rotation, FastEnemySpeed, "FastEnemy", FastEnemyHealthpoints);
        }

        public int GetWaveNum()
        {
            return waveNum;
        }

        public int GetNormalNum()
        {
            return NormalEnemiesCount;
        }

        public int GetFastNum()
        {
            return FastEnemiesCount;
        }

        public int GetStrongNum()
        {
            return StrongEnemiesCount;
        }

        public void ForceSpawn(Vector3 pos, Quaternion rotation, float speed, string type, float healthpoints) {
            GameObject GameObj;      
            if (type.Equals("NormalEnemy"))
            {
                GameObj = (GameObject)Object.Instantiate(_normalEnemy, pos, rotation, _holder);
                Enemy e;
                e = GameObj.GetComponent<Enemy>();
                e.Initialize(speed, healthpoints, 20);
            }
            else if (type.Equals("StrongEnemy"))
            {
                GameObj = (GameObject) Object.Instantiate(_strongEnemy, pos, rotation, _holder);
                Enemy e;
                e = GameObj.GetComponent<Enemy>();
                e.Initialize(speed, healthpoints, 50);
            }
            else if (type.Equals("FastEnemy"))
            {
                GameObj = (GameObject)Object.Instantiate(_fastEnemy, pos, rotation, _holder);
                Enemy e;
                e = GameObj.GetComponent<Enemy>();
                e.Initialize(speed, healthpoints, 30);
            }
        }

        public void AskCanWalk(GameObject go) {
            _selectedGo = go;
            if (_selectedGo != null) {
                _selectedGo.GetComponent<NavMeshObstacle>().enabled = true;
            }
            _checkCanWalkNext = 1;
        }

        private void CheckCanWalk() {
            _checkCanWalkNext = 0;

            if (_selectedGo == null) {
                Game.Ctx.UI.OnCanWalkResult(false);
                return;
            }

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