using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBar : MonoBehaviour {
	private RectTransform _baseHealthBarRect;
	private Slider _slider;

	// Use this for initialization
	void Start () {
		_baseHealthBarRect = GetComponent<RectTransform>();
		_slider = GetComponent<Slider>();
		_slider.normalizedValue = 1f;
		_slider.maxValue = 100f;
		_slider.minValue = 0f;
        _slider.value = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		var height = Screen.height * .5f;
		var width = Screen.width * .5f;
		_baseHealthBarRect.anchoredPosition = new Vector2(-width, height);
	}

	private void Die()
	{
		Destroy(gameObject);
	}

	public void PerformDamage(float points) {
		_slider.value -= points;
		//if (_slider.value < 0.01f)
		//{
		//	Die();
		//}
	}
}
