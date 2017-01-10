using UnityEngine;
using System.Collections;

public class SwordHitScript : MonoBehaviour {
	public string TagToCheck = "Enemy";
    public GameObject AttackedEnemy;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if (this.enabled) {
			if (c.CompareTag (TagToCheck)) {
				Debug.Log ("Hit: "+TagToCheck+"-"+ c.name);
                AttackedEnemy = GameObject.Find(c.name).gameObject;
                AttackedEnemy.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().attacked = true;
            }
		}
	}
}
