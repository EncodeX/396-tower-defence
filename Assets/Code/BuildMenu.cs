using UnityEngine;
using UnityEngine.UI;

namespace Code {
    partial class UIManager {
        private class BuildMenu : Menu {
            public BuildMenu(Vector3 position, int row, int col) {
                if (Canvas != null) {
                    Go = (GameObject) Object.Instantiate(Resources.Load("BuildMenu"), Canvas.transform);
                    Go.transform.position = position;
                    Row = row;
                    Col = col;
                    InitializeButtons();
                }
            }
            
            private void InitializeButtons() {
                foreach (Button button in Go.GetComponentsInChildren<Button>()) {
                    switch (button.name) {
                        case "ButtonTowerA":
                            button.onClick.AddListener(() => {
                                Game.Ctx.CellManager.PlaceTower(Row, Col, CellManager.CellType.TowerA);
                                Game.Ctx.UI.HideBuildMenu();
                            });
                            break;
                        case "ButtonTowerB":
                            button.interactable = false;
                            break;
                        case "ButtonTowerC":
                            button.interactable = false;
                            break;
                    }
                }
            }
        }
    }
}