using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCharacterControl : MonoBehaviour {
	[SerializeField] private Animator _selfAnimator;
	[SerializeField] private Rigidbody _selfRigibody;
	// Use this for initialization
	void Start () {
		if (_selfAnimator == null) {
			_selfAnimator = GetComponent<Animator> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetAxis ("Jump") > 0) {
			_selfAnimator.SetFloat ("Jump", 1);
		} else {
			_selfAnimator.SetFloat ("Jump", 0);
		}
	}

	void FixedUpdate () {
		_selfAnimator.SetFloat ("Forward", CrossPlatformInputManager.GetAxis("Vertical"));
		_selfAnimator.SetFloat ("Turn", CrossPlatformInputManager.GetAxis("Horizontal"));
		//gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);

	}

	void OnAnimatorIK()
	{
	}
}
