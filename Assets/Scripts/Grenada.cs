using System.Collections;
using UnityEngine;
public class Grenada : MonoBehaviour
{
    [SerializeField] private ParticleSystem pS;
    [SerializeField] private int damage1;
    [SerializeField] private float radius;
    [SerializeField] private AudioSource explousion;
    private WeaponTouch wp;
    private Damageable damageable;

    private void Start()
    {
        wp = FindFirstObjectByType<WeaponTouch>();
        StartCoroutine(GrenadaThrou());
    }
    private IEnumerator GrenadaThrou()
    {
        yield return new WaitForSeconds(3);
        pS.transform.parent = null;
        var colliderList = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider i in colliderList) { Damage(i.gameObject, damage1); }
        pS.Play();
        explousion.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(pS.gameObject);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void Damage(GameObject gm, int damage)
    {
        if (gm.TryGetComponent(out damageable)) damageable.TakeDamage(damage);
        else if( gm.TryGetComponent(out LinkToGm LinkToGM))
        {
            if (LinkToGM.GameObject.TryGetComponent(out damageable)) damageable.TakeDamage(damage);
        }
    }
}
