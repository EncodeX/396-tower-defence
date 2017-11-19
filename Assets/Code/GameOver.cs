using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class GameOver : MonoBehaviour
    {
        public GameObject GameOverPanel;
        public Text GameOverText;
        bool over = false;

        void Start()
        {
            GameOverPanel.SetActive(false);
            Debug.Log("Started");
        }

        void Update()
        {
            Debug.Log("Updated");
            over = Game.Ctx.isOver();
            if (over)
            {
                Debug.Log("Received Over Signal.");
                GameOverPanel.SetActive(true);
            }
        }
    }
}
