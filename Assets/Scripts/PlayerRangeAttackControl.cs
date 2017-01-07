using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
public class PlayerRangeAttackControl : MonoBehaviour {
	public GameObject BulletObject;

	public Transform Redball;
	public Transform RangeWeapon;
	public Transform GunPoint;
	public Transform RangeWeaponHandle;
	public Transform LeftHandPivot;
	public Transform ShoulderPoint;

	public float MaximumDistance = 1000f;

	public bool IKMode = true;

	private Animator _selfAnimator;
	private RaycastHit _hit;
	private Ray _cameraRay;

	// Use this for initialization
	void Start () {
		_selfAnimator = GetComponent<Animator> ();
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Fire2")) {
			_selfAnimator.SetBool ("Aiming", true);
		} else if (CrossPlatformInputManager.GetButtonUp ("Fire2")) {
			_selfAnimator.SetBool ("Aiming", false);
		}

		Vector3 tmp_heading = _cameraRay.GetPoint (MaximumDistance) - ShoulderPoint.position;
		Vector3 tmp_direction = tmp_heading / tmp_heading.magnitude;

		// In Aiming State
		if(_selfAnimator.GetBool("Aiming")){
			_cameraRay = new Ray(
				Camera.main.transform.position, Camera.main.transform.forward
			);
			transform.LookAt (new Vector3(_cameraRay.GetPoint (10).x, 0, _cameraRay.GetPoint (10).z));

			if (Physics.SphereCast (ShoulderPoint.position, 0.1f, tmp_direction, out _hit)) {
				Redball.position = _hit.point;
			} else {
				Redball.position = ShoulderPoint.position + tmp_direction * MaximumDistance;
			}
		}

		if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("RangeAttack")) {
			Vector3 tmp_targetPoint = new Vector3 (_cameraRay.GetPoint (10).x, 0, _cameraRay.GetPoint (10).z);
			transform.LookAt (tmp_targetPoint);

			RangeWeapon.LookAt (Redball.position);
			RangeWeapon.position = ShoulderPoint.position + tmp_direction*0.9f;
			if (CrossPlatformInputManager.GetButtonDown ("Fire1") && CrossPlatformInputManager.GetButton ("Fire2")) {
				Instantiate (BulletObject, GunPoint.position, GunPoint.rotation);
			}
		} else {
			RangeWeapon.position = LeftHandPivot.position;
			RangeWeapon.rotation = LeftHandPivot.rotation;
		}
	}

	void OnAnimatorIK()
	{
		
		if (_selfAnimator && IKMode) {

			//if the IK is active, set the position and rotation directly to the goal. 
			if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("RangeAttack")) {
				_selfAnimator.SetLookAtWeight (1);
				_selfAnimator.SetLookAtPosition (_cameraRay.GetPoint (10));

				_selfAnimator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1);
				_selfAnimator.SetIKPosition (AvatarIKGoal.LeftHand, RangeWeaponHandle.position);

				_selfAnimator.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1);
				_selfAnimator.SetIKRotation (AvatarIKGoal.LeftHand, RangeWeaponHandle.rotation);

			}

			//if the IK is not active, set the position and rotation of the hand and head back to the original position
			else {
				_selfAnimator.SetLookAtWeight (0);
				_selfAnimator.SetIKRotationWeight (AvatarIKGoal.LeftHand, 0);
				_selfAnimator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0);  
			}
		} else {
		}
	}   
}
