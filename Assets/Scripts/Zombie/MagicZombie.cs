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
        if (dist < minDist && !isReloading && !gameObject.GetComponent<Damageable>()._isKilled) { StartCoroutine(Fog()); }
        if (gameObject.GetComponent<Damageable>()._isKilled && RenderSettings.fogDensity > 0.04f) { RenderSettings.fogDensity = 0.4f; }
    }
    private IEnumerator Fog()
    {
        isReloading = true;
        for (float i = 0; i < 60; i++) { RenderSettings.fogDensity += 0.001f; }
        yield return new WaitForSeconds(10);
        for (float i = 0; i < 60; i++) { RenderSettings.fogDensity -= 0.001f; }
        yield return new WaitForSeconds(10);
        isReloading = false;
    }
}
