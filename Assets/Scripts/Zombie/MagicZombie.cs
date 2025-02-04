using System.Collections;
using UnityEngine;
public class MagicZombie : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float minDist = 25;
    private bool isReloading;
    private void Start() { player = FindAnyObjectByType<WeaponTouch>().transform; }
    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < minDist && !isReloading) { StartCoroutine(Fog()); }
    }
    private IEnumerator Fog()
    {
        isReloading = true;
        yield return new WaitForSeconds(10);
        RenderSettings.fogDensity = 0.1f;
        yield return new WaitForSeconds(10);
        RenderSettings.fogDensity = 0.004f;
        isReloading = false;
    }
}
