using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class WalkingToPlayer : MonoBehaviour
{ 
    private Transform carTransform, targetTransform;
    [SerializeField] private AudioSource zombieWalking, zombieHitting, isDamaged;
    [SerializeField] private float playerDist, hittingDist;
    [SerializeField] private int damage;
    public NavMeshAgent agent;
    public Animator anim;
    private CarController carController;
    private WeaponTouch weaponTouchPlayer;
    private bool isHitting;
    private GoIntoCar goIntoCar;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        weaponTouchPlayer = FindAnyObjectByType<WeaponTouch>();
        targetTransform = weaponTouchPlayer.transform;
        isHitting = false;
        goIntoCar = FindAnyObjectByType<GoIntoCar>();
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
        
        isDamaged.Play();
        yield return new WaitForSeconds(1f);
        anim.SetBool("isHitting", false);
        isHitting = false;
    }
    
}
