using System;
using UnityEngine;

namespace Code {
    public partial class UIManager {
        public static Transform Canvas { get; private set; }

        private BuildMenu _build;
        private UpgradeMenu _upgrade;
        
        private bool ShowingBuildMenu{ get { return _build != null && _build.Showing; } }
        private bool ShowingUpgradeMenu{ get { return _upgrade != null && _upgrade.Showing; } }

        public UIManager() {
            Canvas = GameObject.Find("Canvas").transform;
        }

        public void ShowCellMenu(Vector3 position, int row, int col) {
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
            ShowBuildMenu(position, row, col);
        }

        private void ShowBuildMenu(Vector3 position, int row, int col) {
            _build = new BuildMenu(position, row, col);
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