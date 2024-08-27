using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelController : MonoBehaviour
{
    [SerializeField] private ParticleSystem pS;
    [SerializeField] private int damage1;
    [SerializeField] private float radius;
    private Damageable damageable;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            pS.transform.parent = null;
            pS.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            
        }
    }
}
