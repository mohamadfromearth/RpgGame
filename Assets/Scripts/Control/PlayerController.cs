using Rpg.Combat;
using UnityEngine;
using Rpg.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter fighter;
    
    
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }
    
        void Update()
        {
            if (IntractWithCombat()) return;
            if (IntractWithMovement()) return;
            print("Nothing to do");
        }
    
        private bool IntractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                var target = hit.collider.transform.GetComponent<CombatTarget>();
                if (!fighter.CanAttack(target)) continue;
    
                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(target);
                }
    
                return true;
            }
    
            return false;
        }
    
        private bool IntractWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
    
                return true;
            }
    
            return false;
        }
    
    
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
