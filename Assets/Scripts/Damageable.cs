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
    private Transform player;
    private CoinController _coinController;

    private void Start()
    {
        _coinController = FindFirstObjectByType<CoinController>();
        player = FindAnyObjectByType<WeaponTouch>().gameObject.transform;
        zC = FindAnyObjectByType<ZombieCreation>();
        zombieHp = GetComponentInChildren<Slider>();
        zombieHp.maxValue = hp;
        zombieHp.value = hp;
        damageText.gameObject.SetActive(false);
        //Quaternion.Euler()  from vec3 to quat
        //transform.rotation.euler from quat to vec3
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        zombieHp.value = hp;
        StartCoroutine(DamageText(damage));
        if (hp <= 0)
        {
            _coinController.CoinSpawn(gameObject.transform.position);
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
            enabled = false;
        }
    }

    private IEnumerator DamageText(int damage)
    {
        
        damageText.gameObject.SetActive(true);
        damageText.transform.LookAt(player);
        var rot = damageText.transform.rotation.eulerAngles;
        rot.y = 180;
        
        damageText.transform.rotation = Quaternion.Euler(rot);
        
        damageText.text = damage.ToString();
        
        
        yield return new WaitForSeconds(0.5f);
        damageText.gameObject.SetActive(false);
    }
}