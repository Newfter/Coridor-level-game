using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public GameObject damagePanel, deadPanel, potionPanel;
    public int hp;
    private void Start() { StartCoroutine(Healing()); }
    private IEnumerator Healing()
    {
        if (hp < 10) hp += 1;
        yield return new WaitForSeconds(60);
        StartCoroutine(Healing());
    }
    public void Damage(int damageAmount) { StartCoroutine(PlayerDamage(damageAmount));}
    private IEnumerator PlayerDamage(int damageAmount)
    {
        hp -= damageAmount;
        damagePanel.SetActive(true);
        if (hp <= 0)
        {
            deadPanel.SetActive(true);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        damagePanel.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other) { if (other.gameObject.CompareTag("Potion")) { StartCoroutine(HealingByPotion(other.gameObject)); } }
    private IEnumerator HealingByPotion(GameObject toDestroy)
    {
        hp += 2;
        Destroy(toDestroy);
        potionPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        potionPanel.SetActive(false);
    }
}
