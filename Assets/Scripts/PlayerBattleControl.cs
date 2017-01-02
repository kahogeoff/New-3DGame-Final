using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
public class PlayerBattleControl : MonoBehaviour {
	public GameObject Redball;
	public Transform GunPoint;
	public float MaximumDistance = 1000f;

	private Animator _selfAnimator;
	private RaycastHit _hit;
	private Ray _cameraRay;
	// Use this for initialization
	void Start () {
		_selfAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Fire2")) {
			_selfAnimator.SetBool ("Aiming", true);
		} else if (CrossPlatformInputManager.GetButtonUp ("Fire2")) {
			_selfAnimator.SetBool ("Aiming", false);
		}

		if(_selfAnimator.GetBool("Aiming")){
			_cameraRay = new Ray(
				Camera.main.transform.position, Camera.main.transform.forward
			);
			transform.LookAt (new Vector3(_cameraRay.GetPoint (10).x, 0, _cameraRay.GetPoint (10).z));

			Vector3 p1 = GunPoint.position;
			float distanceToObstacle = 0;

			if (Physics.SphereCast(_cameraRay, 0.1f, out _hit, MaximumDistance)) {
				Redball.transform.position = _hit.point;
			}
		}

		if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("RangeAttack")) {
			transform.LookAt (new Vector3(_cameraRay.GetPoint (10).x, 0, _cameraRay.GetPoint (10).z));
		}
	}

	void OnAnimatorIK()
	{
		
		if(_selfAnimator) {

			//if the IK is active, set the position and rotation directly to the goal. 
			if(_selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("RangeAttack")) {

				_selfAnimator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1);
				_selfAnimator.SetLookAtWeight (1);

				_selfAnimator.SetLookAtPosition (_cameraRay.GetPoint(10));
				_selfAnimator.SetIKPosition(AvatarIKGoal.LeftHand, _cameraRay.GetPoint(MaximumDistance));

			}

			//if the IK is not active, set the position and rotation of the hand and head back to the original position
			else {
				_selfAnimator.SetLookAtWeight (0);
				_selfAnimator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0);       
				
			}
		}
	}   
}
