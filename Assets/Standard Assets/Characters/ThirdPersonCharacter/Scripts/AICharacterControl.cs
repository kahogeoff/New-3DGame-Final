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
        public Transform tmpTarget;
        public Quaternion targetRotation;
        public float strength = 0.5f;
        public float str;
        public bool attacked = false;
        public float forward = 1;
        public float runAwayTime = 0.0f;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = true;
            agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(forward * target.position);
            //AI movement
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {

                character.Move(Vector3.zero, false, false);
                targetRotation = Quaternion.LookRotation(target.position - transform.position);
                str = Mathf.Min(strength * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
                //close enough to attack player
                this.GetComponent<AIAttackSpace.AIAttackMeleeControl>().AttackEnable = true;

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
                Debug.Log(this.gameObject.name + "be Hitten!");
                if (this.gameObject.name.Contains("Faster_new"))
                {
                    //start sun away
                    forward = -1;
                    runAwayTime = 0.0f;
                }
                UnsetAttacked();
            }
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
