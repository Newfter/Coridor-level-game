using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WalkingToPlayer : MonoBehaviour
{ 
    public Transform playerTransform, carTransform;
    [SerializeField] private AudioSource zombieWalking, zombieHitting, isDamaged;
    [SerializeField] private float playerDist, hittingDist;
    public NavMeshAgent agent;
    public Animator anim;
    private CarController cC;
    private WeaponTouch wT;
    private bool isHitting;
    private void Start()
    {
        carTransform = FindAnyObjectByType<CarController>().transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        wT = FindAnyObjectByType<WeaponTouch>();
        playerTransform = wT.transform;
        isHitting = false;
    }

    private void Update()
    {
        if (wT.enabled&&cC is not null) {playerTransform = cC.playerTransform;}
        if (!wT.enabled&& playerTransform != carTransform) { playerTransform = carTransform;}
        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist < hittingDist&& !isHitting) { 
            StartCoroutine(Damage());
            agent.ResetPath();
        }
        if (dist < playerDist&& dist > hittingDist) {
            anim.SetBool("isWalking", true);
            agent.SetDestination(playerTransform.position);
        }
        else {
            anim.SetBool("isWalking", false);
            agent.ResetPath();
        }
    }

    private IEnumerator Damage()
    {
        isHitting = true;
        anim.SetBool("isHitting", true);
        yield return new WaitForSeconds(0.5f);
        playerTransform.gameObject.GetComponent<PlayerController>().Damage();
        
        isDamaged.Play();
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isHitting", false);
        isHitting = false;
    }
}
