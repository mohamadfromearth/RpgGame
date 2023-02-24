using Rpg.Combat;
using Rpg.Core;
using Rpg.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float suspicionsTime = 5f;
        [SerializeField] private float chaseDistance = 5f;
        private float timeSinceLastSawPlayer;
        private GameObject player;
        private Vector3 guardPosition;
        private Fighter fighter;
        private Health health;
        private ActionScheduler actionScheduler;
        private Mover mover;
        [SerializeField] private PathPatrol pathPatrol;
        [SerializeField] private float wayPointTolerance = 1f;
        private int currentWaypointIndex = 0;


        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            guardPosition = transform.position;
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (health.IsDead) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (suspicionsTime > timeSinceLastSawPlayer)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (pathPatrol != null)
            {
                if (AtWayPoint())
                {
                    Debug.Log("cycle");
                    CycleWayPoint();
                }

                Debug.Log("next pos");
                nextPosition = GetCurrentWayPoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return pathPatrol.GetWayPoint(currentWaypointIndex);
        }

        private void CycleWayPoint()
        {
            currentWaypointIndex = pathPatrol.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position,GetCurrentWayPoint());
            Debug.Log("distance" + distanceToWayPoint);
            return distanceToWayPoint < wayPointTolerance;
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }
    }
}