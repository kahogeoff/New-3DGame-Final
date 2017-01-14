using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
namespace AIAttackSpace
{
    public class AIAttackMeleeControl : MonoBehaviour
    {
        public TrailRenderer SlashingFX;
        public SwordHitScript HitScript;
        public float ResetCooldown = 1.0f;
        public float SlashCooldown = 0.2f;
		public float AnimationMultiplier = 1.0f;

        public float MaximumSwordStep = 2.0f;

		public bool ShouldAttack = false;


        private Animator _selfAnimator;

        private float _swordStep;
        private float _reachResetCooldownCounter = 0.0f;
        private float _reachSlashCooldownCounter = 0.0f;
        // Use this for initialization
        void Start()
        {
            _selfAnimator = GetComponent<Animator>();
            _swordStep = 0;
            _reachResetCooldownCounter = 0.0f;
            _reachSlashCooldownCounter = 0.0f;
			_selfAnimator.SetFloat ("MeleeAttackSpeed", AnimationMultiplier);
        }

        // Update is called once per frame
        void Update()
        {
            if (_selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                //SlashingFX.enabled = false;
                HitScript.enabled = false;
            }

            if (_selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack"))
            {
				//SlashingFX.enabled = true;
				HitScript.enabled = true;

				_selfAnimator.SetBool ("SwingSword", false);
            }

            _reachResetCooldownCounter += Time.deltaTime;
            _reachSlashCooldownCounter += Time.deltaTime;
            if (_reachResetCooldownCounter >= ResetCooldown)
            {
				_swordStep = 0;
                _selfAnimator.SetFloat("SwordStep", _swordStep);
                _reachResetCooldownCounter = 0;
                //AttackEnable = false;
            }
        }

		void FixedUpdate(){
		}

		void AttackPlayer(){
			if (!ShouldAttack) {
				Debug.Log ("Should not attack.");
				return;
			}

			if (_selfAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Grounded")) {
				Debug.Log ("Cooling Down");
				_reachResetCooldownCounter = 0;
				//SlashingFX.enabled = true;
				HitScript.enabled = true;

				if (_reachSlashCooldownCounter >= SlashCooldown) {
					Debug.Log ("Slash");
					_reachSlashCooldownCounter = 0;
					_selfAnimator.SetBool ("SwingSword", true);
					_selfAnimator.SetFloat ("SwordStep", _swordStep);
					_swordStep += 1;
					if (_swordStep > MaximumSwordStep) {
						_swordStep = 0;
					}
				}
			}
		}


		void StartAttack(){
			ShouldAttack = true;
		}

		void StopAttack(){
			ShouldAttack = false;
		}


    }
}