using System;
using Cinemachine;
using UnityEngine;

public class RcketScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    private void OnCollisionEnter(Collision other)
    {
        explosion.Play();
        Destroy(gameObject);
        print(other.gameObject.name);
    }
}
