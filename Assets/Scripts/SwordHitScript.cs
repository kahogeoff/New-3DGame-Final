using UnityEngine;
using System.Collections;

public class SwordHitScript : MonoBehaviour {
	public string TagToCheck = "Enemy";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (this.enabled) {
			if (c.CompareTag (TagToCheck)) {
				Debug.Log ("Hit: "+TagToCheck);
			}
		}
	}
}
