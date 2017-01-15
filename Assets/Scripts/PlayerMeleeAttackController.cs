using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMeleeAttackController : MonoBehaviour {
	public TrailRenderer SlashingFX;
	public AudioSource SlashingSFX;
	public SwordHitScript HitScript;
	public float ResetCooldown = 1.0f;
	public float SlashCooldown = 0.2f;
	public float AnimationMultiplier = 1.0f;

	public float MaximumSwordStep = 2.0f;

	private Animator _selfAnimator;

	private float _swordStep;
	private float _reachResetCooldownCounter = 0.0f;
	private float _reachSlashCooldownCounter = 0.0f;

	// Use this for initialization
	void Start () {
		_selfAnimator = GetComponent<Animator> ();
		_swordStep = 1;
		_reachResetCooldownCounter = 0.0f;
		_reachSlashCooldownCounter = 0.0f;
		_selfAnimator.SetFloat ("MeleeAttackSpeed", AnimationMultiplier);
	}
	
	// Update is called once per frame
	void Update () {
		if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Grounded")) {
			SlashingFX.enabled = false;
			HitScript.enabled = false;
			if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
				_reachResetCooldownCounter = 0;
				SlashingFX.enabled = true;
				HitScript.enabled = true;

				Ray tmp_cameraRay = new Ray(
					Camera.main.transform.position, Camera.main.transform.forward
				);
				transform.LookAt (new Vector3 (tmp_cameraRay.GetPoint (100).x, transform.position.y, tmp_cameraRay.GetPoint (100).z));

				if (_reachSlashCooldownCounter >= SlashCooldown) {
					SlashingSFX.Play ();
					_reachSlashCooldownCounter = 0;
					_selfAnimator.SetBool ("SwingSword", true);
					_selfAnimator.SetFloat ("SwordStep", _swordStep);
					_swordStep += 1;
					if(_swordStep > MaximumSwordStep){
						_swordStep = 1;
					}
				}
			}
		}
		if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MeleeAttack")) {
			SlashingFX.enabled = true;
			HitScript.enabled = true;
		}

		if (CrossPlatformInputManager.GetButtonUp ("Fire1")) {
			_selfAnimator.SetBool ("SwingSword", false);
		}

		_reachResetCooldownCounter += Time.deltaTime;
		_reachSlashCooldownCounter += Time.deltaTime;
		if (_reachResetCooldownCounter >= ResetCooldown) {
			_swordStep = 1;
			_selfAnimator.SetFloat ("SwordStep", _swordStep);
			_reachResetCooldownCounter = 0;
		}
	}
}
