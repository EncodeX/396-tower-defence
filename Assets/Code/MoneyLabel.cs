using UnityEngine;
using UnityEngine.UI;

namespace Code {
	public class MoneyLabel : MonoBehaviour {
		private RectTransform _moneyLabelRect;
		private Text _moneyLabel;

		// Use this for initialization
		void Start () {
			_moneyLabelRect = GetComponent<RectTransform>();
			_moneyLabel = GetComponent<Text>();
		}
	
		// Update is called once per frame
		void Update () {
			var height = Screen.height * .5f;
			var width = Screen.width * .5f;
			_moneyLabelRect.anchoredPosition = new Vector2(-width + 170, height);
			_moneyLabel.text = "Money: " + Game.Ctx.GetPlayerMoney();
		}
	}
}
