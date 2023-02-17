using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    Transform target;

    NavMeshAgent navmeshAgent;

    Animator animator;

    // Update is called once per frame


    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
      
        UpdateAnimator();
    }




    public void MoveTo(Vector3 destination)
    {
        navmeshAgent.destination = destination;
    }

    void UpdateAnimator()
    {
        Vector3 velocity = navmeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed",speed);
    }
}