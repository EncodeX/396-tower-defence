using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code {
    public class Notification : MonoBehaviour
    {
        private RectTransform _notificationRect;
        private Text _notification;
        public bool display;
        private float ShowTime;
        private float _lastshow;

        void Start()
        {
            _notificationRect = GetComponent<RectTransform>();
            _notification = GetComponent<Text>();
            _lastshow = Time.time - 1.0f;
            ShowTime = 1.0f;
            display = false;
        }

        void Update()
        {
            display = Game.Ctx.DisplayNotification();
            var height = Screen.height * .5f - 300;
            var width = Screen.width * .5f;

            if (display)
            {
                _notificationRect.anchoredPosition = new Vector2(-width + 30, height);
                _notification.text = "Warning: Unable to build tower";
            }
            else
            {
                _notificationRect.anchoredPosition = new Vector2(-width + 30, height);
                _notification.text = "";
            }
        }
    }
}
