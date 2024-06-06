using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingToPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource zombieWalking, zombieHitting;
    [SerializeField] private float playerDist, hittingDist;
    private NavMeshAgent agent;
    private Animator anim;
    private int hp = 2;
    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist < playerDist&& dist > hittingDist) {
            anim.SetBool("isWalking", true);
            agent.SetDestination(playerTransform.position);
        }
        else {
            anim.SetBool("isWalking", false);
            agent.ResetPath();
        }
        if(hp == 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) { if(other.gameObject.CompareTag("Bullet")){hp = hp - 1;} }
}
