using System.Collections;
using UnityEngine;
public class ExplosiveBarrelController : MonoBehaviour
{
    [SerializeField] private ParticleSystem pS;
    private void OnCollisionEnter(Collision other) { if (other.gameObject.CompareTag("Bullet")) { StartCoroutine(Explousion()); } }
    private IEnumerator Explousion()
    {
        pS.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
