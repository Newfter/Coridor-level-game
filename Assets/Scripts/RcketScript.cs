using System;
using Cinemachine;
using UnityEngine;

public class RcketScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    private Damageable damageable;
    private void OnCollisionEnter(Collision other)
    {
        explosion.transform.parent = null;
        explosion.Play();

        var colliderList =Physics.OverlapSphere(transform.position, 4f);
        foreach (var collider in colliderList)
        {
            if (collider.TryGetComponent(out damageable))
            {
                damageable.TakeDamage(10);
            }
            else if( collider.TryGetComponent(out LinkToGM LinkToGM))
            {
                if (LinkToGM.GameObject.TryGetComponent(out damageable))
                {
                    damageable.TakeDamage(10);
                }
            }
        }
        Destroy(explosion.gameObject, 5f);
        Destroy(gameObject);
    }
}
