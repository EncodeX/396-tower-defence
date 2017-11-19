using UnityEngine;
using UnityEngine.EventSystems;

namespace Code {
    public class GroundCell : MonoBehaviour {
        private Vector3 _currentMouseDown;

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
                        Mathf.RoundToInt(transform.position.z) + 2);
                }
            }
        }
    }
}