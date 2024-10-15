using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Damageable : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;
    public int hp, zombiesKilled;
    public List<Rigidbody> rigidList;
    private Slider zombieHp;
    private ZombieCreation zC;

    private void Start()
    {
        zC = FindAnyObjectByType<ZombieCreation>();
        zombieHp = GetComponentInChildren<Slider>();
        zombieHp.maxValue = hp;
        zombieHp.value = hp;
        damageText.gameObject.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        zombieHp.value = hp;
        StartCoroutine(DamageText(damage));
        if (hp <= 0)
        {
            zC.zombiesKilled += 1;
            zC.zombiesKilledText.text = zC.zombiesKilled.ToString();
            zombieHp.gameObject.SetActive(false);
            zombiesKilled += 1;
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

    private IEnumerator DamageText(int damage)
    {
        damageText.gameObject.SetActive(true);
        damageText.text = damage.ToString();
        yield return new WaitForSeconds(0.5f);
        damageText.gameObject.SetActive(false);
    }
}