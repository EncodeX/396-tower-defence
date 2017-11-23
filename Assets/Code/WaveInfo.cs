using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class WaveInfo : MonoBehaviour
    {

        private RectTransform _waveInfoRect;
        private Text _waveInfo;

        void Start()
        {
            _waveInfoRect = GetComponent<RectTransform>();
            _waveInfo = GetComponent<Text>();
        }

        void Update()
        {
            var height = Screen.height * .5f - 500;
            var width = -(Screen.width * .5f) + 30;
            float timeLeft = Game.Ctx.getTimeLeft();
            if (timeLeft > 0)
            {
                _waveInfoRect.anchoredPosition = new Vector2(width + 300 * (1.0f / timeLeft), height + 300*(1.0f/timeLeft));
                _waveInfo.text = "Wave: " + Game.Ctx.GetWave() + "\n" + "Normal Enemies: " + Game.Ctx.GetNormalCount() + "\n" + "Strong Enemies: " + Game.Ctx.GetStrongCount() + "\n" + "Fast Enemies: " + Game.Ctx.GetFastCount() + "\nTime to Next Wave: " + timeLeft;
            }
            else
            {
                _waveInfoRect.anchoredPosition = new Vector2(width, height);
                _waveInfo.text = "";
            }
            
        }
    }
}
