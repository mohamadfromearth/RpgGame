using System;
using UnityEngine;

namespace Rpg.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        private Animator animator;
        private bool isDead = false;
        public bool IsDead => isDead;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            Die();
        }

        private void Die()
        {
            if (isDead) return;
            if (health == 0)
            {
                isDead = true;
                animator.SetTrigger("die");
            }
        }
    }
}