using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int hp;
    public List<Rigidbody> rigidList;
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            var wtp = GetComponent<WalkingToPlayer>();
            wtp.agent.enabled = false;
            wtp.anim.enabled = false;
            wtp.enabled = false;
            foreach (var i in rigidList) {
                i.isKinematic = false;
                i.useGravity = true;
                i.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
            Destroy(gameObject, 100);
        }
    }
}