using UnityEngine;

namespace Code {
    public class CellManager {
        public enum CellType {
            Empty,
            TowerA,
            TowerB,
            TowerC,
            Base,
            EnemySpawn
        }
        
        public Vector3 SpawnPos = new Vector3(2f, 0.3f, 2f);
        public Vector3 BasePos = new Vector3(-2f, 0.3f, -2f);

        private readonly int[][] _towerMap;
        private readonly Object _towerTypeA;
        private readonly Object _towerTypeB;
        private readonly Object _towerTypeC;
        private readonly Transform _holder;

        public CellManager(Transform holder) {
            _towerMap = new int[5][];
            for (int i = 0; i < 5; i++) {
                _towerMap[i] = new int[5];
                for (int j = 0; j < 5; j++) {
                    _towerMap[i][j] = (int) CellType.Empty;
                }
            }
            _towerMap[0][0] = (int) CellType.Base;
            _towerMap[4][4] = (int) CellType.EnemySpawn;
            _holder = holder;
            _towerTypeA = Resources.Load("TowerTypeA");
            _towerTypeB = Resources.Load("Freeze_Tower");
            _towerTypeC = Resources.Load("Lightning_Tower");
        }

        public void PlaceTower(int row, int col, CellType type) {
            _towerMap[row][col] = (int) type;
            switch (type) {
                case CellType.TowerA:
                    var gameObjectA = ((GameObject) Object.Instantiate(_towerTypeA, new Vector3(row - 2, 0, col - 2),
                        new Quaternion(), _holder)).GetComponent<TowerTypeA>().Initialize(row, col);
                    Game.Ctx.AddMoney(-gameObjectA.GetComponent<TowerTypeA>().Cost);
                    break;
                case CellType.TowerB:
					var gameObjectB = ((GameObject)Object.Instantiate(_towerTypeB, new Vector3(row - 2, 0, col - 2),
						new Quaternion(), _holder)).GetComponent<TowerTypeB>().Initialize(row, col);
					Game.Ctx.AddMoney(-gameObjectB.GetComponent<TowerTypeB>().Cost);
                    break;
                case CellType.TowerC:
					var gameObjectC = ((GameObject)Object.Instantiate(_towerTypeC, new Vector3(row - 2, 0, col - 2),
						new Quaternion(), _holder)).GetComponent<TowerTypeC>().Initialize(row, col);
					Game.Ctx.AddMoney(-gameObjectC.GetComponent<TowerTypeC>().Cost);
                    break;
            }
        }

        public int CheckCell(int row, int col) {
            return _towerMap[row][col];
        }

        public void SellTower(int row, int col, float ratio) {
            switch (_towerMap[row][col]) {
                case (int)CellType.TowerA:
                    TowerTypeA[] towerAs = _holder.GetComponentsInChildren<TowerTypeA>();
                    foreach (TowerTypeA tower in towerAs) {
                        if (tower.Row == row && tower.Col == col) {
                            Game.Ctx.AddMoney((int)(ratio * tower.Cost));
                            GameObject.Destroy(tower.gameObject);
                            break;
                        }
                    }
                    break;
                case (int)CellType.TowerB:
					TowerTypeB[] towerBs = _holder.GetComponentsInChildren<TowerTypeB>();
					foreach (TowerTypeB tower in towerBs)
					{
						if (tower.Row == row && tower.Col == col)
						{
							Game.Ctx.AddMoney((int)(ratio * tower.Cost));
							GameObject.Destroy(tower.gameObject);
							break;
						}
					}
                    break;
                case (int)CellType.TowerC:
					TowerTypeC[] towerCs = _holder.GetComponentsInChildren<TowerTypeC>();
					foreach (TowerTypeC tower in towerCs)
					{
						if (tower.Row == row && tower.Col == col)
						{
							Game.Ctx.AddMoney((int)(ratio * tower.Cost));
							GameObject.Destroy(tower.gameObject);
							break;
						}
					}
                    break;
            }
            _towerMap[row][col] = (int) CellType.Empty;
        }
    }
}