﻿using System.Collections;
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

        public float MaximumSwordStep = 2.0f;
        public bool AttackEnable = false;

        private Animator _selfAnimator;

        private float _swordStep;
        private float _reachResetCooldownCounter = 0.0f;
        private float _reachSlashCooldownCounter = 0.0f;
        // Use this for initialization
        void Start()
        {
            _selfAnimator = GetComponent<Animator>();
            _swordStep = 1;
            _reachResetCooldownCounter = 0.0f;
            _reachSlashCooldownCounter = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (_selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                SlashingFX.enabled = false;
                HitScript.enabled = false;
                if (AttackEnable)
                {
                    _reachResetCooldownCounter = 0;
                    SlashingFX.enabled = true;
                    HitScript.enabled = true;

                    /*Ray tmp_cameraRay = new Ray(
                        Camera.main.transform.position, Camera.main.transform.forward
                    );
                    transform.LookAt(new Vector3(tmp_cameraRay.GetPoint(100).x, transform.position.y, tmp_cameraRay.GetPoint(100).z));
                    */
                    if (_reachSlashCooldownCounter >= SlashCooldown)
                    {
                        Debug.Log("Slash");
                        _reachSlashCooldownCounter = 0;
                        _selfAnimator.SetBool("SwingSword", true);
                        _selfAnimator.SetFloat("SwordStep", _swordStep);
                        _swordStep += 1;
                        if (_swordStep > MaximumSwordStep)
                        {
                            _swordStep = 1;
                        }
                    }
                }
            }
            if (_selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack"))
            {
                SlashingFX.enabled = true;
                HitScript.enabled = true;
            }

            if (!AttackEnable)
            {
                _selfAnimator.SetBool("SwingSword", false);
            }

            _reachResetCooldownCounter += Time.deltaTime;
            _reachSlashCooldownCounter += Time.deltaTime;
            if (_reachResetCooldownCounter >= ResetCooldown)
            {
                Debug.Log("Reset");
                _swordStep = 1;
                _selfAnimator.SetFloat("SwordStep", _swordStep);
                _reachResetCooldownCounter = 0;
                //AttackEnable = false;
            }
        }
    }
}