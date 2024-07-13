using System;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class RcketScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    private Damageable damageable;
    public WeaponSO wS;
    private void OnCollisionEnter(Collision other)
    {
        explosion.transform.parent = null;
        explosion.Play();

        var colliderList =Physics.OverlapSphere(transform.position, 4f);
        foreach (var collider in colliderList)
        {
            Damage(collider.gameObject, wS.damageAmount);
        }
        Destroy(explosion.gameObject, 5f);
        Destroy(gameObject);
    }

    public void Damage(GameObject gm,int damage)
    {
        if (gm.TryGetComponent(out damageable)) damageable.TakeDamage(damage);
        else if( gm.TryGetComponent(out LinkToGm LinkToGM))
        {
            if (LinkToGM.GameObject.TryGetComponent(out damageable)) damageable.TakeDamage(damage);
        }
    }
}
