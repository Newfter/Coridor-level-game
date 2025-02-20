using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI timeText;
    public GameObject damagePanel, deadPanel, potionPanel, newRecord;
    public int hp;

    private void Start()
    {
        StartCoroutine(Healing());
        newRecord.SetActive(false);
        deadPanel.SetActive(false);
        damagePanel.SetActive(false);
        potionPanel.SetActive(false);
    }
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
        healthBar.value = hp;
        damagePanel.SetActive(true);
        if (hp <= 0) { Death(); }
        yield return new WaitForSeconds(0.2f);
        damagePanel.SetActive(false);
        
    }
    private void Death()
    {
        int seconds = FindAnyObjectByType<Clock>().StopClock();
        var record = PlayerPrefs.GetInt("RecordTime", 0);
        if (record < seconds)
        {
            PlayerPrefs.SetInt("RecordTime", seconds);
            timeText.text = seconds.ToString();
            newRecord.SetActive(true);
        }
        else { timeText.text = record.ToString(); }
        deadPanel.SetActive(true);
        Destroy(gameObject);
    }
        
    private void OnTriggerEnter(Collider other) { if (other.gameObject.CompareTag("Potion")) { StartCoroutine(HealingByPotion(other.gameObject)); } }
    private IEnumerator HealingByPotion(GameObject toDestroy)
    {
        hp += 2;
        healthBar.value = hp;
        Destroy(toDestroy);
        potionPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        potionPanel.SetActive(false);
    }
}
