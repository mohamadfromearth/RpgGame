using System;
using Rpg.Combat;
using Rpg.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        private GameObject player;
        private Fighter fighter;
        private Health health;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                print(gameObject.name + " Should chase");
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }
    }
}