using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingToPlayer : MonoBehaviour
{ 
    private Transform playerTransform;
    [SerializeField] private AudioSource zombieWalking, zombieHitting;
    [SerializeField] private float playerDist, hittingDist, playHp;
    private NavMeshAgent agent;
    private Animator anim;
    private int hp;
    private ZombieCraetion zC;
    private void Start()
    {
        hp = PlayerPrefs.GetInt("hp");
        playHp = PlayerPrefs.GetFloat("playHp");
        zC = FindObjectOfType<ZombieCraetion>();
        playerTransform = FindObjectOfType<WeaponTouch>().transform;
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
        if (hp == 0)
        {
            zC.zombieAmount = zC.zombieAmount - 1;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet"))
        {
            hp = hp - 1;
            PlayerPrefs.SetInt("hp", hp);
        } 
    }
}
