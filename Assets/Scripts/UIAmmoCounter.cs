using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAmmoCounter : MonoBehaviour {
	public Text CounterText;
	public Text ReloadingText;

	private PlayerRangeAttackControl _prac;
	// Use this for initialization
	void Start () {
		_prac = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRangeAttackControl> ();
		CounterText.text = string.Format ("{0}/{1}", _prac.GetRemainBullet (), _prac.MaximumNumberOfBullet);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CounterText.text = string.Format ("{0}/{1}", _prac.GetRemainBullet (), _prac.MaximumNumberOfBullet);

		if (_prac.GetRemainBullet () <= 0) {
			CounterText.enabled = false;
			ReloadingText.enabled = true;
		} else if(_prac.GetRemainBullet () > 0 && !CounterText.enabled){
			CounterText.enabled = true;
			ReloadingText.enabled = false;
		}
	}
}
