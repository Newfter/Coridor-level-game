using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WalkingToPlayer : MonoBehaviour
{ 
    private Transform carTransform, targetTransform;
    [SerializeField] private AudioResource[] death, walk, hit;
    [SerializeField] private AudioSource zombieWalking, zombieDeath, damaging, brains;
    [SerializeField] private float playerDist, hittingDist;
    [SerializeField] private int damage;
    public NavMeshAgent agent;
    public Animator anim;
    private CarController carController;
    private WeaponTouch weaponTouchPlayer;
    private bool isHitting;
    private GoIntoCar goIntoCar;
    private WalkingSound wS;
    
    private IEnumerator Start()
    {
        wS = FindAnyObjectByType<WalkingSound>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        weaponTouchPlayer = FindAnyObjectByType<WeaponTouch>();
        targetTransform = weaponTouchPlayer.transform;
        isHitting = false;
        goIntoCar = FindAnyObjectByType<GoIntoCar>();
        yield return new WaitForSeconds(Random.Range(0f, 20f));
        StartCoroutine(Brains());
    }

    private void Update()
    {
        if (!goIntoCar.inCar)
        {
            targetTransform = weaponTouchPlayer.transform;
            hittingDist = 2;
        }
        else
        { 
            carTransform = goIntoCar.currentCar.gameObject.transform; 
            targetTransform = carTransform;
            hittingDist = 3;
        }
        
        float dist = Vector3.Distance(transform.position, targetTransform.position);
        if(dist > playerDist)return;
        if (dist < hittingDist&& !isHitting) { 
            StartCoroutine(Damage());
            agent.ResetPath();
        }
        if (dist < playerDist&& dist > hittingDist) {
            anim.SetBool("isWalking", true);
            agent.SetDestination(targetTransform.position);
        }
        else {
            anim.SetBool("isWalking", false);
            agent.ResetPath();
        }
    }

    private IEnumerator Brains()
    {
        yield return new WaitForSeconds(Random.Range(10, 20));
        var i = Random.Range(0, 100);
        if (i < 30)
        {
            brains.pitch = Random.Range(0.5f, 0.9f);
            brains.Play();
        }
        StartCoroutine(Brains());
    }
    private IEnumerator Damage()
    {
        isHitting = true;
        anim.SetBool("isHitting", true);
        if(targetTransform.gameObject.TryGetComponent(out PlayerController playerController)) { playerController.Damage(damage); }
        else
        {
            carController = FindAnyObjectByType<CarController>();
            carController.Damage(damage);
        }

        wS.SetSound(hit, damaging);
        damaging.Play();
        yield return new WaitForSeconds(1f);
        anim.SetBool("isHitting", false);
        isHitting = false;
    }
}
