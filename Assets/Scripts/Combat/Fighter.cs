using Rpg.Core;
using Rpg.Movement;
using UnityEngine;

namespace Rpg.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Transform target;
        private Mover mover;
        private ActionScheduler actionScheduler;
        private Animator animator;
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttack = 1f;
        [SerializeField] private float weaponDamage = 5f;

        private float timeSinceLastAttack;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!GetInRange())
            {
                actionScheduler.StartAction(this);
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                timeSinceLastAttack = 0;
                animator.SetTrigger("Attack"); 
            }
            
            
        }

        private bool GetInRange() 
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        // Animation Event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        public void Cancel()
        {
            target = null;
        }
    }
}