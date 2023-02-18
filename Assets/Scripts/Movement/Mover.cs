using Rpg.Core;
using UnityEngine;
using UnityEngine.AI;


namespace Rpg.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        Transform target;

        NavMeshAgent navmeshAgent;

        Animator animator;

        private ActionScheduler actionScheduler;


        void Start()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }


        void Update()
        {
            UpdateAnimator();
        }


        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navmeshAgent.destination = destination;
            navmeshAgent.isStopped = false;
        }
        

        void UpdateAnimator()
        {
            Vector3 velocity = navmeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navmeshAgent.isStopped = true;
        }
    }
}