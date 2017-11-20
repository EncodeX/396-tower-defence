using UnityEngine;
using UnityEngine.UI;

namespace Code {
    partial class UIManager {
        private class UpgradeMenu : Menu {
            public UpgradeMenu(Vector3 position, int row, int col) {
                if (Canvas != null) {
                    Go = (GameObject) Object.Instantiate(Resources.Load("UpgradeMenu"), Canvas.transform);
                    Go.transform.position = position;
                    Row = row;
                    Col = col;
                    InitializeButtons();
                }
            }
            
            private void InitializeButtons() {
                foreach (Button button in Go.GetComponentsInChildren<Button>()) {
                    switch (button.name) {
                        case "UpgradeButton":
                            button.interactable = false;
                            break;
                        case "SellButton":
                            button.onClick.AddListener(() => {
                                Game.Ctx.CellManager.SellTower(Row, Col,0.9f);
                                Game.Ctx.UI.HideUpgradeMenu();
                            });
                            break;
                    }
                }
            }
        }
    }
}