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

        private Health health;

        [SerializeField] private float maxSpeed = 5f;


        void Start()
        {
            navmeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }


        void Update()
        {
            navmeshAgent.enabled = !health.IsDead;
            UpdateAnimator();
        }


        public void StartMoveAction(Vector3 destination,float speedFraction)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination,speedFraction);
        }

        public void MoveTo(Vector3 destination,float speedFraction)
        {
            navmeshAgent.destination = destination;
            navmeshAgent.speed = maxSpeed * speedFraction;
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