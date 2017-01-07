using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMeleeAttackController : MonoBehaviour {
	public float ResetCooldown = 1.0f;
	public float SlashCooldown = 0.2f;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Grounded")) {
			if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
				_reachResetCooldownCounter = 0;

				if (_reachSlashCooldownCounter >= SlashCooldown) {
					Debug.Log ("Slash");
					_reachSlashCooldownCounter = 0;
					_selfAnimator.SetBool ("SwingSword", true);
					_selfAnimator.SetFloat ("SwordStep", _swordStep);
					_swordStep += 1;
					if(_swordStep > 3){
						_swordStep = 1;
					}
				}
			}
		}
		if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("MeleeAttack")) {
			
		}
		if (CrossPlatformInputManager.GetButtonUp ("Fire1")) {
			_selfAnimator.SetBool ("SwingSword", false);
		}

		_reachResetCooldownCounter += Time.deltaTime;
		_reachSlashCooldownCounter += Time.deltaTime;
		if (_reachResetCooldownCounter >= ResetCooldown) {
			Debug.Log ("Reset");
			_swordStep = 1;
			_selfAnimator.SetFloat ("SwordStep", _swordStep);
			_reachResetCooldownCounter = 0;
		}
	}
}
