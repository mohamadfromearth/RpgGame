using System;
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
        [SerializeField] private float weaponRange = 2f;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }


        private void Update()
        {
            if (target == null) return;

            if (!GetInRange())
            {
                actionScheduler.StartAction(this);
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
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

        public void Cancel()
        {
            target = null;
        }
    }
}