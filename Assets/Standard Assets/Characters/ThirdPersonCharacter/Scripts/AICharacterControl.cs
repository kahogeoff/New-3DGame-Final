using System;
using UnityEngine;
using System.Collections;

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
        public float runAwayTime = 0.0f;

		private Animator _selfAnimator;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = true;
            agent.updatePosition = true;

			_selfAnimator = GetComponent<Animator> ();
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(forward * target.position);
            //AI movement

			if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
				this.SendMessage("StopAttack");

			}else if (agent.remainingDistance <= agent.stoppingDistance) {

				if (this.gameObject.name.Contains ("Faster_new")) {
					if (!attacked) {
						character.Move (Vector3.zero, false, false);
						transform.LookAt (new Vector3 (target.position.x, transform.position.y, target.position.z));
						//close enough to attack player
						this.SendMessage ("StartAttack");
						this.SendMessage ("AttackPlayer");
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
                runAwayTime += Time.deltaTime;

            //stop runaway when runawaytime >= 2s
            if (runAwayTime >= 2.0f)
            {
                forward = 1;
                runAwayTime = 0.0f;
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
					Debug.Log("Run away!");
					_selfAnimator.StopPlayback ();
					_selfAnimator.SetBool ("SwingSword", false);
					_selfAnimator.Play("Grounded");
                    forward = -1;
                    runAwayTime = 0.0f;
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

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
