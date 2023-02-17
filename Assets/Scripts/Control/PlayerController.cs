using Rpg.Combat;
using UnityEngine;
using Rpg.Movement;

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
        IntractWithCombat();
        IntractWithMovement();
    }

    private void IntractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (var hit in hits )
        {
            var target = hit.collider.transform.GetComponent<CombatTarget>();
            if (target == null) continue;

            if (Input.GetMouseButtonDown(0))
            {
                fighter.Attack(target);
            }
            
        }
    }

    private void IntractWithMovement()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }


    void MoveToCursor()
    {
        Ray ray = GetMouseRay();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            mover.MoveTo(hit.point);
        }
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}