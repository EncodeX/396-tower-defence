﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code {
    public abstract class Tower : MonoBehaviour {
        public int Cost;
        public int Row;
        public int Col;
        
        private Vector3 _currentMouseDown;
        
        public bool CanShoot = true;

        private void OnMouseOver() {
            if (Input.GetMouseButtonDown(0)) {
                _currentMouseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && _currentMouseDown != null) {
                if (EventSystem.current.IsPointerOverGameObject()) {
                    return;
                }
                if (_currentMouseDown == Input.mousePosition) {
                    Game.Ctx.UI.ShowCellMenu(
                        Game.Ctx.Camera.WorldToScreenPoint(new Vector3(transform.position.x, 0, transform.position.z)),
                        Mathf.RoundToInt(transform.position.x) + 2,
                        Mathf.RoundToInt(transform.position.z) + 2,
                        null);
                }
            }
        }

        private void Update() {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Attack(enemies);
        }

        protected abstract void Attack(Enemy[] enemies);
    }
}