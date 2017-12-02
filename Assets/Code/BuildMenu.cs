﻿﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Code
{
    partial class UIManager
    {
        private class BuildMenu : Menu
        {
            Dictionary<string, int> TowerCost = new Dictionary<string, int>(){
               {"TowerA",100},
               {"TowerB",100},
               {"TowerC",100}
            };

            public BuildMenu(Vector3 position, int row, int col, bool canBuildOnCell)
            {
                if (Canvas != null)
                {
                    Go = (GameObject)Object.Instantiate(Resources.Load("BuildMenu"), Canvas.transform);
                    Go.transform.position = position;
                    Row = row;
                    Col = col;
                    InitializeButtons(canBuildOnCell);
                }
            }


            private void InitializeButtons(bool canBuildOnCell)
            {
                foreach (Button button in Go.GetComponentsInChildren<Button>())
                {
                    switch (button.name)
                    {
                        case "ButtonTowerA":
                            if (Game.Ctx.GetPlayerMoney() < TowerCost["TowerA"] || !canBuildOnCell)
                            {
                                button.interactable = false;
                                break;
                            }
                            button.onClick.AddListener(() => {
                                Game.Ctx.CellManager.PlaceTower(Row, Col, CellManager.CellType.TowerA);
                                Game.Ctx.UI.HideBuildMenu();
                            });
                            break;
                        case "ButtonTowerB":
							if (Game.Ctx.GetPlayerMoney() < TowerCost["TowerB"] || !canBuildOnCell)
							{
								button.interactable = false;
								break;
							}
							button.onClick.AddListener(() => {
								Game.Ctx.CellManager.PlaceTower(Row, Col, CellManager.CellType.TowerB);
								Game.Ctx.UI.HideBuildMenu();
							});
                            break;
                        case "ButtonTowerC":
							if (Game.Ctx.GetPlayerMoney() < TowerCost["TowerC"] || !canBuildOnCell)
							{
								button.interactable = false;
								break;
							}
							button.onClick.AddListener(() => {
								Game.Ctx.CellManager.PlaceTower(Row, Col, CellManager.CellType.TowerC);
								Game.Ctx.UI.HideBuildMenu();
							});
                            break;
                    }
                }
            }


        }
    }
}