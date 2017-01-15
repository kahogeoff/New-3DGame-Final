using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public string TagToCheck = "Enemy";
	public float HealthReducing = 1.0f;
	public float OriginSpeed = 400;
	public float Torque = 100;
	public float DestroyTime = 10;
	public GameObject HittedObject;

	private float _destroyCounter = 0.0f;
	private Rigidbody _selfRigibody;
	//private Collider _selfCollider;

	// Use this for initialization
	void Start () {
		_destroyCounter = 0.0f;
		_selfRigibody = GetComponent<Rigidbody> ();
		//_selfCollider = GetComponentInChildren<Collider> ();

		_selfRigibody.velocity = transform.forward * OriginSpeed;
		_selfRigibody.AddRelativeTorque (transform.forward * Torque);
	}
	
	// Update is called once per frame
	void Update () {
		_destroyCounter += Time.deltaTime;
		if (_destroyCounter >= DestroyTime) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		_selfRigibody.velocity = transform.forward;
		if (collision.gameObject.CompareTag (TagToCheck)) {
			Debug.Log ("Bullet Hit: "+TagToCheck+" - "+ collision.gameObject.name);
			HittedObject = collision.gameObject;
			HittedObject.SendMessage("SetAttacked",SendMessageOptions.DontRequireReceiver);
			HittedObject.SendMessage("ReduceHealth", HealthReducing, SendMessageOptions.DontRequireReceiver);

		}
	}

	void OnCollisionExit(Collision collision){
		HittedObject = null;
		StartCoroutine ("DestroyAfterSecond");
	}

	IEnumerator DestroyAfterSecond(){
		yield return new WaitForSeconds(0.1f);
		Destroy (gameObject);
	}
}
