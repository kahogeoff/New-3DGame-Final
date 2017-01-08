using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public float OriginSpeed = 400;
	public float Torque = 100;
	public float DestroyTime = 10;

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

		Debug.Log ("Destroy");
		Destroy (gameObject);
	}

	void OnCollisionExit(Collision collision){
	}
}
