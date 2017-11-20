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
        }

        public void PlaceTower(int row, int col, CellType type) {
            _towerMap[row][col] = (int) type;
            switch (type) {
                case CellType.TowerA:
                    var gameObject = ((GameObject) Object.Instantiate(_towerTypeA, new Vector3(row - 2, 0, col - 2),
                        new Quaternion(), _holder)).GetComponent<TowerTypeA>().Initialize(row, col);
                    Game.Ctx.AddMoney(-gameObject.GetComponent<TowerTypeA>().Cost);
                    break;
                case CellType.TowerB:
                    break;
                case CellType.TowerC:
                    break;
            }
        }

        public int CheckCell(int row, int col) {
            return _towerMap[row][col];
        }

        public void SellTower(int row, int col) {
            switch (_towerMap[row][col]) {
                case (int)CellType.TowerA:
                    TowerTypeA[] towers = _holder.GetComponentsInChildren<TowerTypeA>();
                    foreach (TowerTypeA tower in towers) {
                        if (tower.Row == row && tower.Col == col) {
                            Game.Ctx.AddMoney((int)(.9f * tower.Cost));
                            GameObject.Destroy(tower.gameObject);
                            break;
                        }
                    }
                    break;
                case (int)CellType.TowerB:
                    break;
                case (int)CellType.TowerC:
                    break;
            }
            _towerMap[row][col] = (int) CellType.Empty;
        }
    }
}