using System.Collections;
using UnityEngine;
public class MagicZombie : MonoBehaviour
{
    [SerializeField] private Vector3 player;
    private float minDist = 25;
    private bool isReloading;
    private void Start() { player = FindAnyObjectByType<WeaponTouch>().transform.position; }
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player);
        if (dist < minDist && !isReloading) { StartCoroutine(Fog()); }
    }
    private IEnumerator Fog()
    {
        isReloading = true;
        
        yield return new WaitForSeconds(10);

        yield return new WaitForSeconds(10);
        isReloading = false;
    }
}
