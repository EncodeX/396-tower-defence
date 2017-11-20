using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Code {
    public partial class UIManager {
        public static Transform Canvas { get; private set; }

        private BuildMenu _build;
        private UpgradeMenu _upgrade;
        
        private bool ShowingBuildMenu{ get { return _build != null && _build.Showing; } }
        private bool ShowingUpgradeMenu{ get { return _upgrade != null && _upgrade.Showing; } }

        private Vector3 _selectedPos;
        private int _selectedRow;
        private int _selectedCol;

        public UIManager() {
            Canvas = GameObject.Find("Canvas").transform;
        }

        public void ShowCellMenu(Vector3 position, int row, int col, GameObject go) {
            if (ShowingBuildMenu) {
                HideBuildMenu();
                return;
            }
            if (ShowingUpgradeMenu) {
                HideUpgradeMenu();
                return;
            }
            var cellObj = Game.Ctx.CellManager.CheckCell(row, col);
            if (cellObj == (int) CellManager.CellType.Base || cellObj == (int) CellManager.CellType.EnemySpawn) {
                return;
            }
            if (cellObj != (int) CellManager.CellType.Empty) {
                ShowUpgradeMenu(position, row, col);
                return;
            }

            _selectedPos = position;
            _selectedCol = col;
            _selectedRow = row;
            Game.Ctx.EnemyManager.AskCanWalk(go);
        }

        public void OnCanWalkResult(bool canWalk) {
            ShowBuildMenu(_selectedPos, _selectedRow, _selectedCol, canWalk);
        }

        private void ShowBuildMenu(Vector3 position, int row, int col, bool canBuildOnCell) {
            _build = new BuildMenu(position, row, col, canBuildOnCell);
            _build.Show();
        }

        private void HideBuildMenu() {
            _build.Hide();
            _build = null;
        }

        private void ShowUpgradeMenu(Vector3 position, int row, int col) {
            _upgrade = new UpgradeMenu(position, row, col);
            _upgrade.Show();
        }

        private void HideUpgradeMenu() {
            _upgrade.Hide();
            _upgrade = null;
        }

        private abstract class Menu
        {
            protected GameObject Go;
            protected int Col;
            protected int Row;
            public bool Showing { get; private set; }

            /// <summary>
            /// Show this menu
            /// </summary>
            public virtual void Show () {
                Showing = true;
                Go.SetActive(true);
            }

            /// <summary>
            /// Hide this menu
            /// </summary>
            public virtual void Hide () {
                GameObject.Destroy(Go);
                Showing = false;
            }
        }
    }
}