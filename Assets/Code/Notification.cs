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
            _lastshow = Time.time - 0.2f;
            ShowTime = 1.0f;
            display = false;
        }

        void Update()
        {
            var height = Screen.height * .5f - 300;
            var width = Screen.width * .5f;
            if (display)
            {
                _lastshow = Time.time;
                display = false;
            }

            if ((Time.time - _lastshow) < ShowTime)
            {
                _notificationRect.anchoredPosition = new Vector2(-width + 170, height);
                _notification.text = "Warning:" + Game.Ctx.DisplayNotification();
            }
            else
            {
                _notificationRect.anchoredPosition = new Vector2(-width + 170, height);
                _notification.text = "";
            }
        }
    }
}
