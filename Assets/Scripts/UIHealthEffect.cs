using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthEffect : MonoBehaviour {
	[SerializeField] private Text DeathMessage;
	private Image _selfImage;
	private CharacterHealthSystem _chs;
	private float RedValue;
	// Use this for initialization
	void Start () {
		_selfImage = GetComponent<Image> ();
		DeathMessage.enabled = false;
		_chs = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterHealthSystem> ();
		RedValue = 80.0f / 255.0f;
	}
	
	// Update is called once per frame
	void Update () {
		float tmp_colorAlpha = 128.0f / 255.0f - ((_chs.CurrentHealth / _chs.MaxHealth) * 128.0f/255.0f);
		_selfImage.color = new Color (RedValue, 0, 0, tmp_colorAlpha);

		if (_chs.CurrentHealth <= 0) {
			DeathMessage.enabled = true;
		}
	}
}
