using UnityEngine;
using System.Collections;

public class SwordHitScript : MonoBehaviour {
	public string TagToCheck = "Enemy";
	public float HealthReducing = 1.0f;
    public GameObject HittedObject;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (this.enabled) {
			if (c.CompareTag (TagToCheck)) {
				Debug.Log ("Hit: "+TagToCheck+" - "+ c.name);
                HittedObject = c.gameObject;
				HittedObject.SendMessage("SetAttacked",SendMessageOptions.DontRequireReceiver);
				HittedObject.SendMessage("ReduceHealth", HealthReducing, SendMessageOptions.DontRequireReceiver);
            }
		}
	}

	void OnTriggerExit(Collider c){
		HittedObject = null;
	}
}
