using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int hp;
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}