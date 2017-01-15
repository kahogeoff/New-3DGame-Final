using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterHealthSystem))]
public class EnemySelfDestruct : MonoBehaviour {
	public GameObject DestroyFX;
	private CharacterHealthSystem _chs;
	// Use this for initialization
	void Start () {
		_chs = GetComponent<CharacterHealthSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_chs.CurrentHealth <= 0) {
			if (DestroyFX) {
				Instantiate (DestroyFX, transform.position, transform.rotation);
			}
			Destroy (gameObject);
		}
	}
}
