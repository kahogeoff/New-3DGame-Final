using System;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;

        // target to aim for
        public Quaternion targetRotation;
        public float strength = 0.5f;
        public float str;
        public bool attacked = false;
        public float forward = 1;
        public float runAwayTime = 2.0f;

		private float _runAwayTimeCounter = 0.0f;
		private Animator _selfAnimator;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )

            /*GameObject go = new GameObject("Target");
            Vector3 sourcePostion = new Vector3(100, 20, 100);//The position you want to place your agent
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(sourcePostion, out closestHit, 500, 1))
            {
                go.transform.position = closestHit.position;
                go.AddComponent<NavMeshAgent>();
            }
            */
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = true;
            agent.updatePosition = true;

            
			_selfAnimator = GetComponent<Animator> ();
        }


        private void Update()
        {
            if (target != null)
            {
                if (forward > 0)
                    agent.SetDestination(target.position);
                else
                {
                    agent.SetDestination(GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>().position);
                }
            }
                //AI movement

			if (agent.remainingDistance > agent.stoppingDistance) {
				character.Move (agent.desiredVelocity, false, false);
				this.SendMessage ("StopAttack");

			} else if (agent.remainingDistance <= agent.stoppingDistance) {

				if (this.gameObject.name.Contains ("Faster_new")) {
					if (!attacked) {
						character.Move (Vector3.zero, false, false);
						transform.LookAt (new Vector3 (target.position.x, transform.position.y, target.position.z));
						//close enough to attack player
						this.SendMessage ("StartAttack");
						this.SendMessage ("AttackPlayer");
					} else {
						this.SendMessage ("StopAttack");
					}
				} else {
					character.Move (Vector3.zero, false, false);
					transform.LookAt (new Vector3 (target.position.x, transform.position.y, target.position.z));
					//close enough to attack player
					this.SendMessage ("StartAttack");
					this.SendMessage ("AttackPlayer");
				}
			}

            //calculate run away time if forward = -1
            if (forward == -1)
				_runAwayTimeCounter += Time.deltaTime;

            //stop runaway when runawaytime >= 2s
			if (_runAwayTimeCounter >= runAwayTime)
            {
                forward = 1;
				_runAwayTimeCounter = 0.0f;
            }
        }

        public void FixedUpdate()
        {
            //AI is attack by player
            if (attacked)
            {
                Debug.Log(this.gameObject.name + " be Hitten!");
                if (this.gameObject.name.Contains("Faster_new"))
                {
                    //start run away
                    forward = -1;
                    Debug.Log("Run away!");
					_selfAnimator.StopPlayback ();
					_selfAnimator.SetBool ("SwingSword", false);
					_selfAnimator.Play("Grounded");
                    
					_runAwayTimeCounter = 0.0f;
                }
            }
			UnsetAttacked ();
        }

        public void SetAttacked()
        {
            attacked = true;

        }

        public void UnsetAttacked()
        {
            attacked = false;

        }

        public void SetTarget()
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
         
           
        }
    }
}
