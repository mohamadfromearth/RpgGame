using Rpg.Core;
using Rpg.Movement;
using UnityEngine;

namespace Rpg.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Health target;
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
            if (target.IsDead) return;
            

                if (!GetInRange())
            {
                actionScheduler.StartAction(this);
                mover.MoveTo(target.transform.position,1f);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform.position);
            if (timeSinceLastAttack > timeBetweenAttack)
            {
                timeSinceLastAttack = 0;
                animator.SetTrigger("Attack");    
                
                
            }
        }

        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;  
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && ! targetToTest.IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        // Animation Event
        void Hit()
        {
            if (target == null)
            {
                return;
            }
            target.TakeDamage(weaponDamage);
        }

        public void Cancel()
        {
            target = null;
            Debug.Log("StopAttack");
            animator.SetTrigger("stopAttack");
            
        }
    }
}